using System;
using System.Collections.Generic;
using System.Linq;

namespace Genetic_Algorithm_Scheduling
{
    public class Solution
    {
        private readonly SortedSet<Interval> _breaks;
        private readonly List<Job> _jobs;
        private bool[] isFirst; 
        public Solution()
        {
        }

        public Solution(SortedSet<Interval> breaks, List<Job> jobs)
        {
            ProcessorOne = 0;
            ProcessorTwo = 0;
            IdleTimeProcessorOne = 0;
            IdleTimeProcessorTwo = 0;
            TaskOrder=new List<int>();
            _breaks = breaks;
            _jobs = jobs;
            isFirst= new bool[jobs.Count+1];

        }
        public List<int> TaskOrder { get; set; }
        public int ProcessorOne { get; set; }
        public int ProcessorTwo { get; set; }
        public int IdleTimeProcessorOne { get; set; }
        public int IdleTimeProcessorTwo { get; set; }
        public int EndTime { get; set; }

        private void addSecondOperation(Job job, ref int second,ref int idleTime, int startTimeForSecond)
        {
            
            int startTime = (second > startTimeForSecond) ? second : startTimeForSecond;
            Interval nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);
           
            while (nearestBreak != null && nearestBreak.StartTime - startTime < job.SecondTime)
            {
                idleTime += nearestBreak.StartTime - startTime;


               startTime = nearestBreak.EndTime;
               nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            }
            second = startTime + job.SecondTime;



        }


        private int addFirstOperation(Job job, ref int first, ref int idleTime)
        {
            isFirst[job.Id] = true;
            int startTime = first;
            Interval nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            while (nearestBreak != null && nearestBreak.StartTime - startTime < job.FirstTime)
            {
                idleTime += nearestBreak.StartTime - startTime;


                startTime = nearestBreak.EndTime;
                nearestBreak = _breaks.FirstOrDefault(x => x.StartTime >= startTime);

            }
            first = startTime + job.FirstTime;


            return first;
        }



        public void GenerateProcessorsTimeline()
        {
            ProcessorOne = 0;
            ProcessorTwo = 0;
            for (var i = 0; i < TaskOrder.Count; i++)
            {
                // Console.WriteLine(i+" " + TaskOrder.Count);
                var task = TaskOrder[i];
                if (task > 0) AddFirst(_jobs[task - 1]);
                else AddSecond(_jobs[-task - 1]);
            }
            EndTime = (ProcessorOne > ProcessorTwo) ? ProcessorOne : ProcessorTwo;
            
        }

        public void AddFirst(Job job)
        {
            //TaskOrder.Add(job.Id);

            int first, firstIdle;

            if (job.FirstProcessor == Job.Processor.One)
            {
                first = ProcessorOne;
                firstIdle = IdleTimeProcessorOne;
                job.FirstEndsAt = addFirstOperation(job, ref first, ref firstIdle);
                ProcessorOne = first;
                IdleTimeProcessorOne = firstIdle;
            }
            else
            {
                first = ProcessorTwo;
                firstIdle = IdleTimeProcessorTwo;
                job.FirstEndsAt = addFirstOperation(job, ref first, ref firstIdle);
                ProcessorTwo = first;
                IdleTimeProcessorTwo = firstIdle;
            }


        }

        public void AddSecond(Job job)
        {
            //TaskOrder.Add(-job.Id);
            int second, secondIdle;
            if (job.FirstProcessor == Job.Processor.One)
            {
                second = ProcessorTwo;
                secondIdle = IdleTimeProcessorTwo;
                addSecondOperation(job, ref second, ref secondIdle, job.FirstEndsAt);
                ProcessorTwo = second;
                IdleTimeProcessorTwo = secondIdle;
            }
            else
            {
                second = ProcessorOne;
                secondIdle = IdleTimeProcessorOne;
                addSecondOperation(job, ref second, ref secondIdle,job.FirstEndsAt);
                ProcessorOne = second;
                IdleTimeProcessorOne = secondIdle;

            }



        }
        public Solution Clone()
        {
            var newSolution = new Solution(_breaks, _jobs);
            
           
            return newSolution;
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

    }
}
