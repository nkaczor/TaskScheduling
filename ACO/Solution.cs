using System.Collections.Generic;
using System.Linq;

namespace ACO
{
    public class Solution
    {
        private readonly SortedSet<Interval> _breaks;
        private readonly List<Job> _jobs;

        public Solution()
        {
        }

        public Solution(SortedSet<Interval> breaks, List<Job> jobs)
        {
            ProcessorOne = new SortedSet<Interval>();
            ProcessorTwo = new SortedSet<Interval>();

            _breaks = breaks;
            _jobs = jobs;
            foreach (var interval in breaks)
            {
                ProcessorOne.Add(interval);
                ProcessorTwo.Add(interval);
            }
        }

        public SortedSet<Interval> ProcessorOne { get; set; }
        public SortedSet<Interval> ProcessorTwo { get; set; }
        public List<int> TaskOrder { get; set; }
        public int EndTime { get; set; }

        private void addSecondOperation(Job job, SortedSet<Interval> second, int startTimeForSecond)
        {
            var last = second.LastOrDefault(x => x.Type == Interval.TypeOfInterval.Task);
            var lastTime = (last == null) ? 0 : last.EndTime;
            lastTime = (lastTime > startTimeForSecond) ? lastTime : startTimeForSecond;
            var candidate =
                second.FirstOrDefault(
                    x => x.EndTime >= lastTime &&
                         x.Type == Interval.TypeOfInterval.Free &&
                         (x.StartTime > lastTime && x.EndTime - x.StartTime >= job.SecondTime ||
                          x.StartTime <= lastTime && x.EndTime - lastTime >= job.SecondTime));

            if (candidate == null) //dodajemy na koniec
            {
                var startTime = second.Last().EndTime;
                var length = job.SecondTime;
                second.Add(new Interval(Interval.TypeOfInterval.Task, length, startTime));
            }
            else
            {
                int endTime;
                second.Remove(candidate);
                Interval taskInterval;
                if (lastTime > candidate.StartTime)
                {
                    //ewentualnie dodac free
                    endTime = lastTime + job.FirstTime;
                    taskInterval = new Interval(Interval.TypeOfInterval.Task, job.SecondTime, lastTime);
                }
                else
                {
                    endTime = candidate.StartTime + job.FirstTime;
                    taskInterval = new Interval(Interval.TypeOfInterval.Task, job.SecondTime, candidate.StartTime);
                }


                second.Add(taskInterval);

                if (candidate.EndTime > endTime)
                    second.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.EndTime - endTime, endTime)
                        );

            }
        }
        

        private int addFirstOperation(Job job, SortedSet<Interval> first)
        {
            int endTime;
            var last = first.LastOrDefault(x => x.Type == Interval.TypeOfInterval.Task);
            var lastTime = (last == null) ? 0 : last.EndTime;

            var candidate =
                first.FirstOrDefault(x =>
                    x.EndTime >= lastTime &&
                    x.Type == Interval.TypeOfInterval.Free &&
                    (x.StartTime > lastTime && x.EndTime - x.StartTime >= job.FirstTime ||
                     x.StartTime <= lastTime && x.EndTime - lastTime >= job.FirstTime));

            if (candidate == null)
            {
                var startTime = first.Last().EndTime;
                var length = job.FirstTime;
                first.Add(new Interval(Interval.TypeOfInterval.Task, length, startTime));
                endTime = startTime + length;
            }
            else
            {
                first.Remove(candidate);
                Interval taskInterval;
                if (lastTime > candidate.StartTime)
                {
                    //ewentualnie dodac free
                    endTime = lastTime + job.FirstTime;
                    taskInterval = new Interval(Interval.TypeOfInterval.Task, job.FirstTime, lastTime);
                }
                else
                {
                    endTime = candidate.StartTime + job.FirstTime;
                    taskInterval = new Interval(Interval.TypeOfInterval.Task, job.FirstTime, candidate.StartTime);
                }


                first.Add(taskInterval);

                if (candidate.EndTime > endTime)
                    first.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.EndTime - endTime, endTime)
                        );
            }
            return endTime;
        }

        public Solution Clone()
        {
            var newSolution = new Solution(_breaks, _jobs);
            newSolution.TaskOrder = new List<int>();
            foreach (var task in TaskOrder)
            {
                newSolution.TaskOrder.Add(task);
            }
            return newSolution;
        }

        public void GenerateProcessorsTimeline()
        {
            for (var i = 0; i < TaskOrder.Count; i++)
            {
                // Console.WriteLine(i+" " + TaskOrder.Count);
                var task = TaskOrder[i];
                if (task <= _jobs.Count) AddFirst(_jobs[task - 1]);
                else AddSecond(_jobs[task - _jobs.Count - 1]);
            }
            var endTime1 = ProcessorOne.Last(x => x.Type == Interval.TypeOfInterval.Task).EndTime;
            var endTime2 = ProcessorTwo.Last(x => x.Type == Interval.TypeOfInterval.Task).EndTime;
            EndTime = (endTime1 > endTime2) ? endTime1 : endTime2;
        }

        public void AddFirst(Job job)
        {
            //TaskOrder.Add(job.Id);

            SortedSet<Interval> first;

            if (job.FirstProcessor == Job.Processor.One)
            {
                first = ProcessorOne;
            }
            else
            {
                first = ProcessorTwo;
            }

            job.FirstEndsAt = addFirstOperation(job, first);
        }

        public void AddSecond(Job job)
        {
            //TaskOrder.Add(-job.Id);
            SortedSet<Interval> second;
            if (job.FirstProcessor == Job.Processor.One)
            {
                second = ProcessorTwo;
            }
            else
            {
                second = ProcessorOne;
            }


            addSecondOperation(job, second, job.FirstEndsAt);
        }
    }
}