using System;
using System.Collections.Generic;
using System.Linq;

namespace ACO
{
    public class Solution
    {
        private readonly SortedSet<Interval> _breaks;
        private readonly List<Job> _jobs;
        private int[] _firstEndsAt; 
        public Solution()
        {
        }

        public Solution(SortedSet<Interval> breaks, List<Job> jobs)
        {
            ProcessorOne = 0;
            ProcessorTwo = 0;
            
            _breaks = breaks;
            _jobs = jobs;
             _firstEndsAt=new int[jobs.Count];
           
        }
        public List<int> TaskOrder { get; set; }
        public int ProcessorOne { get; set; }
        public int ProcessorTwo { get; set; }
        
        public int EndTime { get; set; }

        private void addSecondOperation(Job job, ref int second, int startTimeForSecond)
        {
            int startTime = (second>startTimeForSecond)?second:startTimeForSecond;
          
            Interval nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            while (nearestBreak != null && nearestBreak.StartTime - startTime < job.SecondTime)
            {



                startTime = nearestBreak.EndTime;
                nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            }
           second = startTime + job.SecondTime;


            
        }
        

        private int addFirstOperation(Job job, ref int first)
        {
            int startTime = first;
            Interval nearestBreak= _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            while (nearestBreak != null && nearestBreak.StartTime - startTime < job.FirstTime)
            {



                startTime = nearestBreak.EndTime;
                nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            }
            first = startTime + job.FirstTime;

            
            return first;
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
            EndTime = (ProcessorOne > ProcessorTwo) ? ProcessorOne : ProcessorTwo;
        }

        public void AddFirst(Job job)
        {
            //TaskOrder.Add(job.Id);

            int first;

            if (job.FirstProcessor == Job.Processor.One)
            {
                first = ProcessorOne;
                job.FirstEndsAt = addFirstOperation(job, ref first);
                ProcessorOne = first;
            }
            else
            {
                first = ProcessorTwo;
                job.FirstEndsAt = addFirstOperation(job, ref first);
                ProcessorTwo = first;
            }

           
        }

        public void AddSecond(Job job)
        {
            //TaskOrder.Add(-job.Id);
            int second;
            if (job.FirstProcessor == Job.Processor.One)
            {
                second = ProcessorTwo;
                addSecondOperation(job, ref second, job.FirstEndsAt);
                ProcessorTwo = second;
            }
            else
            {
                second = ProcessorOne;
                addSecondOperation(job, ref second, job.FirstEndsAt);
                ProcessorOne = second;
            }


            
        }
    }
}