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
                Solutions = roulette.SpinTheWheel(30);
                var geneticOperator = new GeneticOperator(Solutions);
               
                Solutions = geneticOperator.CreateNewPopulation(200);
                int min = Solutions.Min(sol => sol.EndTime);
                Console.WriteLine(min);

               // List<int> L = new List<int>();
               // foreach (var k in Solutions.First(x => x.EndTime == min).TaskOrder) L.Add(k);
                
               // foreach (var b in L) Console.Write(b+ " ");
                Console.WriteLine();

            }

            
           
        }
    }
}