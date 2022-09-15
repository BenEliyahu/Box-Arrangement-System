using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    internal class BoxHeight : IComparable<BoxHeight>
    {
        public double Y { get; set; }
        public DateTime lastPurchaseDate { get; set; }
        public int Amount { get; set; }
        public const int MaxBoxes = 1000; 
        public int CompareTo(BoxHeight other)
        {
            if (this.Y > other.Y) return 1;
            if (this.Y == other.Y) return 0;
            return -1;
        }
    }
}
