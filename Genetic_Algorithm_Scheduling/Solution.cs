using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Genetic_Algorithm_Scheduling
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
            ProcessorOne = new SortedSet<inte;rvalnterval>();
            ProcessorTwo = new SortedSet<Interval>();
            TaskOrder = new List<int>();
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
            var candidate =
                second.FirstOrDefault(
                    x =>
                        x.StartTime > startTimeForSecond && x.Type == Interval.TypeOfInterval.Free &&
                        x.Length >= job.FirstTime);

            if (candidate == null) //dodajemy na koniec
            {
                var startTime = second.Last().EndTime;
                var length = job.SecondTime;
                second.Add(new Interval(Interval.TypeOfInterval.Task, length, startTime));
            }
            else
            {
                second.Remove(candidate);
                second.Add(new Interval(Interval.TypeOfInterval.Task, job.SecondTime, candidate.StartTime));

                if (candidate.EndTime > candidate.StartTime + job.SecondTime)
                    second.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.Length - job.SecondTime,
                            candidate.StartTime + job.SecondTime)
                        );
            }
        }

        private int addFirstOperation(Job job, SortedSet<Interval> first)
        {
            int endTime;
            var candidate =
                first.FirstOrDefault(x => x.Type == Interval.TypeOfInterval.Free && x.Length >= job.FirstTime);

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
                var taskInterval = new Interval(Interval.TypeOfInterval.Task, job.FirstTime, candidate.StartTime);
                first.Add(taskInterval);
                endTime = candidate.StartTime + job.FirstTime;
                if (candidate.EndTime > candidate.StartTime + job.FirstTime)
                    first.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.Length - job.FirstTime, endTime)
                        );
            }
            return endTime;
        }

        public Solution Clone()
        {
            var newSolution = new Solution(_breaks, _jobs);
            newSolution.TaskOrder = new List<int>();

            return newSolution;
        }

        public void GenerateProcessorsTimeline()
        {
            for (var i = 0; i < TaskOrder.Count; i++)
            {
                // Console.WriteLine(i+" " + TaskOrder.Count);
                var task = TaskOrder[i];
                if (task > 0) AddFirst(_jobs[task - 1]);
                else AddSecond(_jobs[-task - 1]);
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

        public void CheckAndFix()
        {
            var first = new bool[_jobs.Count + 1];
            first.Initialize();
            for (var i = 0; i < TaskOrder.Count; i++)
            {
                var it = TaskOrder[i];
                if (it > 0 && first[it] == false) first[it] = true;
                else if (it > 0) TaskOrder[i] = -it;
                else if (first[-it] == false)
                {
                    first[-it] = true;
                    TaskOrder[i] = -it;
                }
            }
}
        //    int amount=TaskOrder.Count;
        //    var first = new bool[_jobs.Count + 1];
        //    int i=0;
        //    while(i<TaskOrder.Count)
        //    {
        //        var it = TaskOrder[i];
        //        if (it > 0 && first[it] == false) {first[it] = true;
        //            i++;
        //        }

        //        else if (it < 0 && first[-it] == false)
        //        {
        //            TaskOrder.Remove(it);
        //            first[-it] = true;

        //        }
        //        else if (it > 0)
        //        {
        //            TaskOrder.Insert(i + 1, -it);
        //            i++;
        //            first[it] = true;
        //        }
        //        else i++;
        //}
           
        
    
}