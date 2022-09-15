using System;
using System.Collections;
using System.Collections.Generic;

namespace BoxesProject
{
    public class TimeData : IComparable<TimeData>, IEnumerable<TimeData>
    {
        private double _x;
        private double _y;
        public double Y { get => _y; set => _y = value; }
        public double X { get => _x; set => _x = value; }
        public DateTime LastPurchase { get; set; }
        public DataY BoxHeightProp { get; set; }
        public DataX BoxBaseProp { get; set; }

        public TimeData(double x, double y)
        {
            _x = x;
            _y = y;
            LastPurchase = DateTime.Now;
        }
        public int CompareTo(TimeData other)
        {
            return this._x.CompareTo(other._x) & this._y.CompareTo(other._y);
        }
        public IEnumerator<TimeData> GetEnumerator()
        {
            yield return this;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override string ToString()
        {
            return $"expiration {LastPurchase}";
        }
    }
}
