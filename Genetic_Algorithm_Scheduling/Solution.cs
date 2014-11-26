using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_Scheduling
{
    public class Solution 
    {
        public SortedSet<Interval> ProcessorOne { get; set; }
        public SortedSet<Interval> ProcessorTwo { get; set; }
        public List<int> TaskOrder { get; set; }
        private readonly List<Job> _jobs;
        private SortedSet<Interval> _breaks; 
        public Solution()
        {
            
        }
         public Solution(SortedSet<Interval> breaks, List<Job> jobs)
        {
           ProcessorOne=new SortedSet<Interval>();
           ProcessorTwo=new SortedSet<Interval>();
           TaskOrder= new List<int>();
             _breaks = breaks;
             _jobs = jobs;
            foreach (var interval in breaks)
            {
                ProcessorOne.Add(interval);
                ProcessorTwo.Add(interval);
            }

        }

     

        public void AddJob(Job job)
        {

            

            SortedSet<Interval> first;
            SortedSet<Interval> second;
            if (job.FirstProcessor == Job.Processor.One)
            {
                first = ProcessorOne;
                second = ProcessorTwo;
            }
            else
            {
                 first = ProcessorTwo;
                second = ProcessorOne;
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
                Interval taskInterval = new Interval(Interval.TypeOfInterval.Task, job.FirstTime, candidate.StartTime);
                first.Add(taskInterval);
                endTime = candidate.StartTime + job.FirstTime;
                if (candidate.EndTime > candidate.StartTime + job.FirstTime)
                    first.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.Length - job.FirstTime, endTime)
                        );
            }
            return endTime;
        }



        public Solution CloneOnlyOrderList()
        {
            Solution newSolution=new Solution(_breaks,_jobs);
            foreach (var task in TaskOrder)
            {
                newSolution.TaskOrder.Add(task);
            }
            return newSolution;
        }

        public void GenerateProcessorsTimeline()
        {

            foreach (int task in TaskOrder)
            {
                AddJob(_jobs[task]);
            }
        }

        public void AddJobAndId(Job job)
        {
            TaskOrder.Add(job.Id);
            AddJob(job);
        }
    }
}
