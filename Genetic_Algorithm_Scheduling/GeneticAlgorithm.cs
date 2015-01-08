using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Genetic_Algorithm_Scheduling
{
    public class GeneticAlgorithm
    {
        public GeneticAlgorithm(string inputFile)
        {
            var ir = new InputReader(inputFile);
            Jobs = ir.FindJobs();
            Breaks = ir.FindBreaks();
        }

        public List<Job> Jobs { get; set; }
        public SortedSet<Interval> Breaks { get; set; }
        public List<Solution> Solutions { get; set; }

        public void Run()
        {
           

            var solutionsGenerator = new SolutionsGenerator(Jobs, Breaks);
            Solutions = solutionsGenerator.Generate(200);

            for (int i = 0; i < 100; i++)
           
            {
                
                var roulette = new Roulette(Solutions);
                Solutions = roulette.SpinTheWheel(50);
                var geneticOperator = new GeneticOperator(Solutions);
               
                Solutions = geneticOperator.CreateNewPopulation(400);
                int min = Solutions.Min(sol => sol.EndTime);
                Console.WriteLine(min);

               
                Console.WriteLine();

            }

            
           
        }
    }
}