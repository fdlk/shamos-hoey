using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShamosHoey
{
    class Point2D: IComparable<Point2D>
    {
        private double x;
        private double y;

        public double X { get { return x; } }
        public double Y { get { return y; } }

        public Point2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        // left before right, on vertical lines bottom before top
        public int CompareTo(Point2D other)
        {
            int result = Math.Sign(x - other.x);
            if (result == 0)
            {
                result = Math.Sign(y - other.y);
            }
            return result;
        }

        public override string ToString()
        {
            return String.Format("({0},{1})", x, y);
        }
    }
}
