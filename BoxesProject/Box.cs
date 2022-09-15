using _1060DS;
using LinkedListAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public class Box
    {
        public class BoxBase : IComparable<BoxBase>
        {
            public BST<BoxHeight> BstHeight { get; set; }

            public double Width { get; set; }

           // LinkedList<BoxHeight> heightCollection { get; set; }


            public BoxBase(double buttomSize)
            {
                Width = buttomSize;
                BstHeight = new BST<BoxHeight>();
            }

            public int CompareTo(BoxBase other) => Width.CompareTo(other.Width);

            public override string ToString() => $"Width: {Width}";
        }


        public class BoxHeight : IComparable<BoxHeight>
        {
            public double Height { get; set; }
            public int Amount { get; set; }

            private const int maxAmount = 30;
            public int MaxAmount => maxAmount;

            public LogicList<TimeData>.Node MyNode { get; set; }

            DateTime _lastPurchaseDate;

            public BoxHeight(double height, int amount)
            {
                Height = height;
                Amount = amount;
                _lastPurchaseDate = DateTime.Now;
            }

            public int CompareTo(BoxHeight other) => Height.CompareTo(other.Height);

            public override string ToString() => $"\theight:  {Height}\tamount: {Amount}\n";
        }
    }
}
