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
           
            int numberOfJobs = Jobs.Count*2+1;
            NumberOfAgents = 50;
            PheremoneLevel= new double[numberOfJobs,numberOfJobs];
        }

        public double [,] PheremoneLevel { get; set; }
        public List<Job> Jobs { get; set; }
        public SortedSet<Interval> Breaks { get; set; }
        public int NumberOfAgents { get; set; }
        private Ant[] _ants; 
        public void Run()
        {
            initialize();

            for (int i = 0; i <100; i++)
            {



                Solution bestSolution = null;
                foreach (var ant in _ants)
                {
                    ant.Go(PheremoneLevel);
                    Solution antSolution = new Solution(Breaks, Jobs);
                    antSolution.TaskOrder = ant.Path;
                    antSolution.GenerateProcessorsTimeline();
                    ant.SolutionEndTime = antSolution.EndTime;
                    if (bestSolution == null || antSolution.EndTime < bestSolution.EndTime) bestSolution = antSolution;
                }
                Console.WriteLine(bestSolution.EndTime);

                
                vaporization(0.9);
                foreach (var ant in _ants)
                {
                    ant.CalculatePheromone();
                }
            }
        }

        private void initialize()
        {
            for (int i = 0; i < PheremoneLevel.GetUpperBound(0); i++)
            {
                for (int j = 0; j < PheremoneLevel.GetUpperBound(1); j++)
                {
                    PheremoneLevel[i, j] = 0.1;
                }
            }


            _ants = new Ant[NumberOfAgents];
            Random rnd = new Random();
            for (int i = 0; i < _ants.Length; i++)

            {
                _ants[i] = new Ant(Jobs.Count, rnd);
            }
        }


        private void vaporization(double p)
        {
            for (int i = 0; i < PheremoneLevel.GetUpperBound(0); i++)
            {
                for (int j = 0; j < PheremoneLevel.GetUpperBound(1); j++)
                {
                    PheremoneLevel[i, j] *= p;
                    if (PheremoneLevel[i, j] < 0.0001) PheremoneLevel[i, j] = 0.0001;
                }
            }
        }
    }
    
}
