using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShamosHoey
{
    public enum EventType{ SegmentStart, SegmentEnd };

    class Event: IComparable<Event>
    {
        private LineSegment2D segment;
        private EventType type;
        private Point2D point;

        /// <summary>
        /// The type of the event
        /// </summary>
        public EventType Type { get { return type; } }

        /// <summary>
        /// The segment that this event pertains to
        /// </summary>
        public LineSegment2D Segment { get { return segment; } }
        
        /// <summary>
        /// The point where the event occurs
        /// </summary>
        public Point2D Point
        {
            get
            {
                switch (type)
                {
                    case EventType.SegmentStart:
                        return segment.P1;
                    case EventType.SegmentEnd:
                        return segment.P2;
                    default:
                        throw new System.InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Creates a new event
        /// </summary>
        /// <param name="segment">the segement that this event pertains to</param>
        /// <param name="type">the type of the event</param>
        public Event(LineSegment2D segment, EventType type)
        {
            this.segment = segment;
            this.type = type;
        }
        
        public int CompareTo(Event other)
        {
            return Point.CompareTo(other.Point);
        }

        public override string ToString()
        {
            return String.Format("Event: {0} {1}", type, segment);
        }
    }
}
