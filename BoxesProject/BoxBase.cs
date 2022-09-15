using DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    internal class BoxBase : IComparable<BoxBase>
    {
        public double X { get; set; }
        public BST<BoxHeight> YTree { get; set; }

        public int CompareTo(BoxBase other)
        {
            if (this.X > other.X) return 1;
            if (this.X == other.X) return 0;
            return -1;
        }
    }
}
