using BSTHW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public class BoxData
    {
        public DataX BoxBase { get; set; }
        public DataY BoxHeight { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        internal BST<DataY> BoxHeights { get; private set; }

        internal bool Flag { get; private set; }

        public int Amount { get; set; }

        internal BoxData(DataX boxBase, DataY boxHeight, bool flag, int amount)
        {
            BoxBase = boxBase;
            BoxHeight = boxHeight;
            Flag = flag;
            BoxHeights = boxBase.HeightCollections;
            X = boxBase.Width;
            Y = boxHeight.Height;
            Amount = amount;
        }
    }
}
