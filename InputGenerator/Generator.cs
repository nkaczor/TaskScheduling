using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InputGenerator
{
    public class Generator
    {
        private Random rnd;
        public int NumberOfTasks { get; private set; }
        public int NumberOfBreaks { get; private set; }
        public int TimeOfAllOperations { get; private set; }
        public SortedSet<Interval> Scheduler { get; private set; }

        public void Generate(int minNumberOfTasks, int maxNumberOfTasks, string file = "test.in")
        {
            rnd = new Random();
            NumberOfTasks = rnd.Next(minNumberOfTasks, maxNumberOfTasks);
            NumberOfBreaks = Convert.ToInt32( 0.15*NumberOfTasks);
            TimeOfAllOperations = 0;
            generateTasks(file);
            generateBreaks(file);
        }

        private void generateTasks(string file)
        {
            using (var sw = new StreamWriter(file,true))
            {
                sw.WriteLine(NumberOfTasks);
                for (int i = 0; i < NumberOfTasks; i++)
                {
                    int firstTime = rnd.Next(20, 101);
                    int secondTime = rnd.Next(20, 101);
                    int firstProcessor = rnd.Next(1, 3);
                    int secondProcessor = firstProcessor == 1 ? 2 : 1;
                    sw.WriteLine("{0};{1};{2};{3}", firstTime, secondTime, firstProcessor, secondProcessor);
                    TimeOfAllOperations += firstTime + secondTime;
                }
            }
        }

        private void generateBreaks(string file)
        {
            int time =Convert.ToInt32(0.45*TimeOfAllOperations);
            Scheduler = new SortedSet<Interval>
            {
                new Interval(Interval.TypeOfInterval.Free, time + 30, 0)
            };
           

            for (int i = 0; i < NumberOfBreaks; i++)
            {
                var rnd = new Random();
                Interval candidate;
                int breakStart, breakLength;
                do
                {
                    breakStart = rnd.Next(time);
                    breakLength = rnd.Next(10, 31);
                    candidate = Scheduler.SingleOrDefault(x => (x.StartTime <= breakStart && x.EndTime >= breakStart+breakLength));
                } while (candidate==null || candidate.Type != Interval.TypeOfInterval.Free);
                
                Scheduler.Remove(candidate);
                
                if (candidate.StartTime < breakStart)
                    Scheduler.Add(
                        new Interval(Interval.TypeOfInterval.Free, breakStart - candidate.StartTime, candidate.StartTime)
                        );

                Scheduler.Add(new Interval(Interval.TypeOfInterval.Break, breakLength, breakStart));

                if (candidate.EndTime > breakStart + breakLength)
                    Scheduler.Add(
                        new Interval(Interval.TypeOfInterval.Free, candidate.EndTime-breakStart - breakLength,breakStart+breakLength  )
                        );
            
            }


            using (var sw = new StreamWriter(file,true))
            {
                int breakNumber = 1;
                foreach (Interval interval in Scheduler)
                {
                   
                    if (interval.Type == Interval.TypeOfInterval.Break)
                    {
                        sw.WriteLine("{0};{1};{2}", breakNumber, interval.Length, interval.StartTime);
                        breakNumber++;
                    }
                }
            }
        }
    }
}