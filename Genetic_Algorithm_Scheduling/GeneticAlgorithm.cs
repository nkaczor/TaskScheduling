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
            Console.WriteLine("Podaj liczbe sekund");
            var seconds = Int32.Parse(Console.ReadLine());


            var solutionsGenerator = new SolutionsGenerator(Jobs, Breaks);
            Solutions = solutionsGenerator.Generate(50);


            var s = new Stopwatch();
            s.Start();
            while (s.Elapsed < TimeSpan.FromSeconds(seconds))
            {
                var geneticOperator = new GeneticOperator(Solutions);
                Solutions = geneticOperator.CreateNewPopulation(200);
                
                var roulette = new Roulette(Solutions);
                Solutions = roulette.SpinTheWheel(50);
                int min = Solutions.Min(sol => sol.EndTime);
                Console.WriteLine(min);
            }

            s.Stop();

           
        }
    }
}