using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ACO
{
    class AcoAlgorithm
    {
           public AcoAlgorithm(string inputFile)
        {
            var ir = new InputReader(inputFile);
            Jobs = ir.FindJobs();
            Breaks = ir.FindBreaks();
            NumberOfAgents = 30;
            int numberOfJobs = Jobs.Count*2+1;
            PheremoneLevel= new double[numberOfJobs,numberOfJobs];
        }

        public double [,] PheremoneLevel { get; set; }
        public List<Job> Jobs { get; set; }
        public SortedSet<Interval> Breaks { get; set; }
        public int NumberOfAgents { get; set; }

        public void Run()
        {

            Console.WriteLine("Podaj liczbe sekund");
            var seconds = Int32.Parse(Console.ReadLine());

            PheremoneLevel.Initialize();
            for (int i = 0; i < PheremoneLevel.GetUpperBound(0); i++)
            {
                for (int j = 0; j < PheremoneLevel.GetUpperBound(1); j++)
                {
                    PheremoneLevel[i, j] = 0.1;
                }
            }


            Ant[] ants = new Ant[NumberOfAgents];
            Random rnd = new Random();
            for (int i = 0; i < ants.Length; i++)

            {
                ants[i] = new Ant(Jobs.Count, rnd);
            }

            for (int i = 0; i < 1; i++)
            {



                Solution bestSolution = null;
                foreach (var ant in ants)
                {
                    ant.Go(PheremoneLevel);
                    Solution antSolution = new Solution(Breaks, Jobs);
                    antSolution.TaskOrder = ant.Path;
                    antSolution.GenerateProcessorsTimeline();
                    ant.SolutionEndTime = antSolution.EndTime;
                    if (bestSolution == null || antSolution.EndTime < bestSolution.EndTime) bestSolution = antSolution;
                }
                Console.WriteLine(bestSolution.EndTime);

                foreach (var b in bestSolution.ProcessorTwo) Console.WriteLine(b.StartTime+" " + b.Length+" "+ b.Type+" "+ b.EndTime);
                foreach (var b in bestSolution.ProcessorOne) Console.WriteLine(b.StartTime + " " + b.Length + " " + b.Type + " " + b.EndTime);
                vaporization(0.5);
                foreach (var ant in ants)
                {
                    ant.CalculatePheromone(bestSolution.EndTime);
                }
            }

        }
        





    

        private void vaporization(double p)
        {
            for (int i = 0; i < PheremoneLevel.GetUpperBound(0); i++)
            {
                for (int j = 0; j < PheremoneLevel.GetUpperBound(1); j++)
                {
                    PheremoneLevel[i, j] *= p;
                }
            }
        }
    }
    
}
