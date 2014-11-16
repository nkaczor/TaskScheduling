using System;

namespace Genetic_Algorithm_Scheduling
{
    public class Interval : IComparable<Interval> , ICloneable
    {
        public int Length { get; set; }
        public TypeOfInterval Type{ get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
        public Interval(TypeOfInterval type, int length, int startTime)
        {
            Type = type;
            Length = length;
            StartTime = startTime;
            EndTime = startTime + length;
        }
        public enum TypeOfInterval
        {
         Task,
         Break,
         Free
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
