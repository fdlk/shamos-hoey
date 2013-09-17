using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C5;
using System.Diagnostics;

namespace ShamosHoey
{
    class ShamosHoey
    {
        private IPriorityQueue<Event> events;
        private TreeSet<LineSegment2D> sweepline;

        public ShamosHoey(List<LineSegment2D> segments)
        {
            sweepline = new TreeSet<LineSegment2D>();
            events = new IntervalHeap<Event>();
            foreach (var segment in segments)
            {
                events.Add(new Event(segment, EventType.SegmentStart));
                events.Add(new Event(segment, EventType.SegmentEnd));
            }
        }

        private bool HasIntersections()
        {
            while (!events.IsEmpty)
            {
                var currentEvent = events.DeleteMin();
                Console.WriteLine(currentEvent);
                switch (currentEvent.Type)
                {
                    case EventType.SegmentStart:
                        //Console.WriteLine("Adding {0}", currentEvent.Segment);
                        sweepline.Add(currentEvent.Segment);
                        int i = sweepline.IndexOf(currentEvent.Segment);
                        if (i > 0 && currentEvent.Segment.IntersectsWith(sweepline[0]))
                        {
                            return true;
                        }
                        if (i < sweepline.Count - 1 && currentEvent.Segment.IntersectsWith(sweepline[i + 1]))
                        {
                            return true;
                        }
                        //Console.WriteLine(sweepline.Count);
                        break;
                    case EventType.SegmentEnd:
                        //Console.WriteLine("Removing {0}", currentEvent.Segment);
                        int j = sweepline.IndexOf(currentEvent.Segment);
                        if (j > 0 && j < sweepline.Count - 1 && sweepline[j - 1].IntersectsWith(sweepline[j + 1]))
                        {
                            return true;
                        }
                        sweepline.Remove(currentEvent.Segment);
                        break;
                }
                //LogSweeplineContents();
            }
            return false;
        }



        private void LogSweeplineContents()
        {
            Console.WriteLine("Sweepline contents:");
            foreach (var segment in sweepline)
            {
                Console.WriteLine(segment);
            }
        }

        static public int Main(string[] args)
        {
            int n = 6;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<LineSegment2D> segments = new List<LineSegment2D>();
            for (int i = 0; i < n / 2; i++)
            {
                segments.Add(new LineSegment2D(new Point2D(i / (double)n, i),
                    new Point2D(i / (double)n + 100, 100 + i)));
                segments.Add(new LineSegment2D(new Point2D(i / (double)n, i + n),
                    new Point2D(i / (double)n + 100, 100 + i + n)));
            }
            ShamosHoey shamosHoey = new ShamosHoey(segments);
            if (shamosHoey.HasIntersections())
            {
                Console.WriteLine("Found intersections");
            }
            else
            {
                Console.WriteLine("Found no intersections");
            }
            stopwatch.Stop();
            Console.WriteLine("{0} segments took: {1}",n, stopwatch.ElapsedMilliseconds);
            Console.ReadKey();
            return 0;
        }


        
    }
}
