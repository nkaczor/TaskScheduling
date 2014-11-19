using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    public class RandomSolution : Solution
    {
        public RandomSolution(SortedSet<Interval> breaks)
        {
           _ProcessorOne=new SortedSet<Interval>();
           _ProcessorTwo=new SortedSet<Interval>();
            foreach (var interval in breaks)
            {
                _ProcessorOne.Add(interval);
                _ProcessorTwo.Add(interval);
            }

        }
        public void AddJob(Job job)
        {
            SortedSet<Interval> first;
            SortedSet<Interval> second;
            if (job.FirstProcessor == Job.Processor.One)
            {
                first = _ProcessorOne;
                second = _ProcessorTwo;
            }
            else
            {
                 first = _ProcessorTwo;
                second = _ProcessorOne;
            }

            int endTime = addFirstOperation(job, first);
            addSecondOperation(job, second, endTime);

        }
        private void addSecondOperation(Job job, SortedSet<Interval> second, int startTimeForSecond)
        {
            Interval candidate = second.FirstOrDefault(x => x.StartTime>startTimeForSecond && x.Type == Interval.TypeOfInterval.Free && x.Length >= job.FirstTime);

            if (candidate == null) //dodajemy na koniec
            {
                int startTime = second.Last().EndTime;
                int length = job.SecondTime;
                second.Add(new Interval(Interval.TypeOfInterval.Task, length, startTime));
            
            }
            else
            {
                second.Remove(candidate);
                second.Add(new Interval(Interval.TypeOfInterval.Task, job.SecondTime, candidate.StartTime));
                
                if (candidate.EndTime > candidate.StartTime + job.SecondTime)
                    second.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.Length - job.SecondTime, candidate.StartTime + job.SecondTime)
                        );
            }
        }
        private  int addFirstOperation(Job job, SortedSet<Interval> first)
        {
            int endTime;
            Interval candidate = first.FirstOrDefault(x => x.Type == Interval.TypeOfInterval.Free && x.Length >= job.FirstTime);

            if (candidate == null)
            {
                int startTime = first.Last().EndTime;
                int length = job.FirstTime;
                first.Add(new Interval(Interval.TypeOfInterval.Task, length, startTime));
                endTime = startTime + length;
            }
            else
            {
                first.Remove(candidate);
                first.Add(new Interval(Interval.TypeOfInterval.Task, job.FirstTime, candidate.StartTime));
                endTime = candidate.StartTime + job.FirstTime;
                if (candidate.EndTime > candidate.StartTime + job.FirstTime)
                    first.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.Length - job.FirstTime, endTime)
                        );
            }
            return endTime;
        }
    }
}
