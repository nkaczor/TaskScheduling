using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            using (var sw = new StreamWriter("wyjscie.out", false))
            {

                for (int i = 0; i < 300; i++)

                {

                    var roulette = new Roulette(Solutions);
                    Solutions = roulette.SpinTheWheel(50);
                    var geneticOperator = new GeneticOperator(Solutions);
                    Solutions = geneticOperator.CreateNewPopulation(200);
                    int min = Solutions.Min(sol => sol.EndTime);
                    var bestSolution = Solutions.First(sol => sol.EndTime == min);
                    sw.WriteLine(min + " " + bestSolution.IdleTimeProcessorOne + " " +
                                      bestSolution.IdleTimeProcessorTwo);


                }

            }
            Console.WriteLine("KONIEC");
        }
    }
}