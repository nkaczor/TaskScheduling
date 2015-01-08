using System;

namespace ACO
{
    public class Interval : IComparable<Interval> , ICloneable
    {
        public int Length { get; set; }
       
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public Interval( int length, int startTime)
        {
            
            Length = length;
            StartTime = startTime;
            EndTime = startTime + length;
        }
      

        public int CompareTo(Interval other)
        {
            if (StartTime < other.StartTime) return -1;
            else if (StartTime == other.StartTime) return 0;
            else return 1;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
