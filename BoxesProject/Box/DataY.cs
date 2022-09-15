using BSTHW;
using LinkedLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public class DataY : IComparable<DataY>
    {
        public double Height { get; set; }
        public int Amount { get ; set ; }
        public LogicList<TimeData>.Node MyNode { get; set; }
        public TimeData timeData { get; set; }

        public DataY(double height, int amount)
        {
            Height = height;
            Amount = amount;
        }
        public int CompareTo(DataY other)
        {
            return Height.CompareTo(other.Height);
        }
        public override string ToString()
        {
            return $"height {Height} amount {Amount} {timeData}\n";
        }
    }
}
