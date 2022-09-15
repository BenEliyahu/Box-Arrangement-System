using BSTHW;
using LinkedLists;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BoxesProject
{
    public class Manager
    {
        BST<DataX> mainTree = new BST<DataX>();
        LogicList<TimeData> timeList = new LogicList<TimeData>();
        Timer timer;
        INotifier notifier;
        TimeSpan _expiration;
        TimeSpan _checkPeriod;
        double _spanExpiration;
        int _maxItem;
        int _minItem;

        public Manager(INotifier notifier, TimeSpan expiration, TimeSpan checkPeriod, double spanExpiration, int maxAmount, int minAmount)
        {
            _expiration = expiration;
            _checkPeriod = checkPeriod;
            _spanExpiration = spanExpiration;
            _maxItem = maxAmount;
            _minItem = minAmount;
            this.notifier = notifier;
            timer = new Timer(CheckExpireredBoxes, timeList, _expiration, _checkPeriod);
        }
        private void CheckExpireredBoxes(object state)
        {
            if (timeList == null)
            {
                notifier.OnInfo($"All boxes in your stock have been deleted !");
                timer.Dispose();
            }
            while (mainTree.RootProp != null && timeList.Last.Value.LastPurchase.AddDays(_spanExpiration) <= DateTime.Now.Date)
            {
                DataX boxBase;
                DataY boxHeight;
                bool searchedBase = mainTree.Search(timeList.Last.Value.BoxBaseProp, out boxBase);
                bool searchedHeight = boxBase.HeightCollections.Search(timeList.Last.Value.BoxHeightProp, out boxHeight);
                notifier.OnInfo($"Expired box: width {timeList.Last.Value.X} Height {timeList.Last.Value.Y}");
                boxBase.HeightCollections.Remove(boxHeight);
                timeList.RemoveLast();
                if (boxBase.HeightCollections.RootProp == null)
                {
                    mainTree.Remove(boxBase);
                }
            }
        }
        public void Supply(double buttomSize, double heightSize, int amount)
        {
            DataX width = new DataX(buttomSize);
            TimeData data = new TimeData(buttomSize, heightSize);
            DataX boxBase;
            if (amount > _maxItem)
            {
                amount = _maxItem;
                notifier.OnError($"Can not exceed the maximum number of boxes!");
            }
            DataY height = new DataY(heightSize, amount);
            if (mainTree.Search(new DataX(buttomSize), out boxBase))
            {
                DataY boxHeight;
                if (boxBase.HeightCollections.Search(height, out boxHeight))
                {
                    if (boxHeight.Amount + amount <= _maxItem)
                        boxHeight.Amount += amount;
                    else
                    {
                        notifier.OnError($"Can not exceed the maximum number of boxes!");
                        int max = _maxItem - boxHeight.Amount;
                        boxHeight.Amount += max;
                    }
                }
                else
                {
                    data = new TimeData(buttomSize, heightSize);
                    timeList.AddFirst(data);
                    height.MyNode = timeList.First;
                    height.timeData = data;
                    data.BoxBaseProp = width;
                    data.BoxHeightProp = height;
                    boxBase.HeightCollections.Add(height);
                }
            }
            else
            {
                DataX box = new DataX(buttomSize);
                box.HeightCollections.Add(height);
                data = new TimeData(buttomSize, heightSize);
                timeList.AddFirst(data);
                height.timeData = data;
                height.MyNode = timeList.First;
                data.BoxBaseProp = width;
                data.BoxHeightProp = height;
                mainTree.Add(box);
            }
        }
        public bool ShowCollection(out string shop)
        {
            if (mainTree.RootProp == null)
            {
                shop = "";
                Console.ForegroundColor = ConsoleColor.Red;
                notifier.OnError($"Sorry friend , we are out of stock!");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
            StringBuilder sb = new StringBuilder();

            foreach (DataX box in mainTree)
            {
                sb.Append(box.ToString());
                foreach (DataY boxheight in box.HeightCollections)
                {
                    sb = sb.Append(boxheight.ToString());
                }
            }
            shop = sb.ToString();
            return true;
        }
        public bool Purchase(double width, double height, int amount)
        {
            const int splits = 3;
            int amountCounter = 0;
            int splitCounter = 0;
            List<BoxData> buyBoxes = new List<BoxData>();
            DataX tmpBase = new DataX(width);
            DataX tmpMatchBase;
            DataY tmpHeight = new DataY(height, amount);
            DataY tmpMatchHeight;
            while (amountCounter < amount && splitCounter < splits)
            {
                bool findMatchBox = mainTree.FindBestMatch(tmpBase, out tmpMatchBase);

                if (!findMatchBox) break;
                else
                {
                    findMatchBox = tmpMatchBase.HeightCollections.FindBestMatch(tmpHeight, out tmpMatchHeight);
                    if (!findMatchBox && amountCounter > 0)
                    {
                        tmpBase = new DataX(tmpMatchBase.Width + 1);
                    }
                    else if (!findMatchBox) break;
                    else
                    {
                        if (tmpMatchHeight.Amount <= (amount - amountCounter))
                        {
                            buyBoxes.Add(new BoxData(tmpMatchBase, tmpMatchHeight, false, tmpMatchHeight.Amount));
                            amountCounter += tmpMatchHeight.Amount;
                            splitCounter++;
                            tmpHeight = new DataY(tmpMatchHeight.Height + 1, amount);
                        }
                        else
                        {
                            buyBoxes.Add(new BoxData(tmpMatchBase, tmpMatchHeight, true, amount - amountCounter));
                            splitCounter = splits;
                        }
                    }
                }
            }
            string list = ShowList(buyBoxes);
            if (buyBoxes.Count == 0)
            {
                notifier.OnInfo($"We did not find a suitable box for what you wanted !");
                return false;
            }
            bool answer = notifier.Purchase($"Would you like to buy {list} ?");
            if (answer)
            {
                foreach (var box in buyBoxes)
                {
                    if (!box.Flag) CheckOutOfStock(box.BoxHeights, box.BoxBase, box.BoxHeight);
                    else
                    {
                        box.BoxHeight.Amount -= box.Amount;
                        if (box.BoxHeight.Amount < _minItem)
                            notifier.OnInfo($"The quantity of boxes has reached a minimum of boxes");
                        timeList.RellocateToStart(box.BoxHeight.MyNode);
                        box.BoxHeight.timeData.LastPurchase = DateTime.Now;
                    }
                }
                return true;
            }
            return false;
        }
        private string ShowList(List<BoxData> boxInfo)
        {
            StringBuilder n = new StringBuilder();
            n.Append($"\n");
            foreach (BoxData box in boxInfo)
            {
                n.Append($"width {box.X} and height {box.Y}");
            }
            return n.ToString();
        }
        private void CheckOutOfStock(BST<DataY> heightTree, DataX width, DataY height)
        {
            heightTree.Remove(height);
            if (width.HeightCollections.RootProp == null) mainTree.Remove(width);
            notifier.OnInfo($"out of stock : width  {width.Width}, height {height.Height}");
            timeList.DeleteNode(height.MyNode.Value);
        }
    }
}





