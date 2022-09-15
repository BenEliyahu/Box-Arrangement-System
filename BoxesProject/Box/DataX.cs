using BSTHW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxesProject
{
    public class DataX : IComparable<DataX>
    {
        public double Width { get; set; }
        public BST<DataY> HeightCollections { get; set; }
        public DataX(double x)
        {
            this.Width = x;
            HeightCollections = new BST<DataY>();
        }
        public int CompareTo(DataX other) => Width.CompareTo(other.Width);
        public override string ToString()
        {
            return $"\nwidth {Width} \n";
        }
    }
}
