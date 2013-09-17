using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShamosHoey
{
    public enum Orientation { Colinear, Clockwise, Counterclockwise };

    class LineSegment2D : IComparable<LineSegment2D>
    {
        // p1 is the leftmost point, for vertical segments it is the bottom point.
        private Point2D p1;
        private Point2D p2;

        public double X1 { get { return p1.X; } }
        public double Y1 { get { return p1.Y; } }
        public double X2 { get { return p2.X; } }
        public double Y2 { get { return p2.Y; } }

        public LineSegment2D(Point2D p1, Point2D p2)
        {
            if (p1.CompareTo(p2) > 0)
            {
                this.p1 = p2;
                this.p2 = p1;
            }
            else
            {
                this.p1 = p1;
                this.p2 = p2;
            }
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", p1, p2);
        }

        private static Orientation GetOrientation(Point2D p, Point2D q, Point2D r)
        {
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
            double val = (q.Y - p.Y) * (r.X - q.X) -
                      (q.X - p.X) * (r.Y - q.Y);

            // TODO < epsilon?
            
            if (val == 0) return Orientation.Colinear;

            return (val > 0) ? Orientation.Clockwise : Orientation.Counterclockwise;
        }

        public bool IntersectsWith(LineSegment2D other)
        {
            Orientation o1 = GetOrientation(p1, p2, other.p1);
            Orientation o2 = GetOrientation(p1, p2, other.p2);
            Orientation o3 = GetOrientation(other.p1, other.p2, p1);
            Orientation o4 = GetOrientation(other.p1, other.p2, p2);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // other.p1 is colinear with this segment and other.p1 lies on segment p1p2
            if (o1 == Orientation.Colinear && IntersectsWithColinearPoint(other.p1)) return true;

            // other.p1 is colinear with this segment and other.p2 lies on segment p1p2
            if (o2 == Orientation.Colinear && IntersectsWithColinearPoint(other.p2)) return true;

            // p1 is colinear with other segment and p1 lies on other segment
            if (o3 == Orientation.Colinear && other.IntersectsWithColinearPoint(p1)) return true;

            // p2 is colinear with other segment and p2 lies on other segment
            if (o4 == Orientation.Colinear && other.IntersectsWithColinearPoint(p2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        // Given three colinear point q, checks if q lies on this line segment
        private bool IntersectsWithColinearPoint(Point2D q)
        {
            if (q.X <= p2.X && q.X >= p1.X &&
                q.Y <= Math.Max(p1.Y, p2.Y) && q.Y >= Math.Min(p1.Y, p2.Y))
                return true;

            return false;
        }

        /**
         * Comparison of LineSegments relative to the current vertical sweepline.
         */
        public int CompareTo(LineSegment2D other)
        {
            if( this == other ) 
            {
                return 0;
            }
            if (X1 < other.X1)
            {
                return -other.CompareTo(this);
            }
            if (X1 == other.X1)
            {
                int result = p1.CompareTo(other.p1);
                if (result != 0)
                {
                    return result;
                }
                else
                {
                    // same start points, compare end points
                    switch (GetOrientation(other.p1, other.p2, p2))
                    {
                        case Orientation.Clockwise:
                            return -1;
                        case Orientation.Counterclockwise:
                            return 1;
                        default:
                            return p2.CompareTo(other.p2);
                    }
                }
            }
            // TODO: check if other is entirely above or below us

            switch (GetOrientation(other.p1, other.p2, p1))
            {
                case Orientation.Clockwise:
                    return -1;
                case Orientation.Counterclockwise:
                    return 1;
                default:
                    //TODO: colinear line segments, starting in same point?!?
                    return 0;
            }
        }
    }
}
