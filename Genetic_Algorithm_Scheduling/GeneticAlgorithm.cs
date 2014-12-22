using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_Scheduling
{
    public class GeneticAlgorithm
    {
        public List<Job> Jobs { get; set; }
        
        public SortedSet<Interval> Breaks { get; set; }
        public List<Solution> Solutions  { get; set; }
        public GeneticAlgorithm(string inputFile)
        {
            var ir= new InputReader(inputFile);
            Jobs = ir.FindJobs();  
            Breaks = ir.FindBreaks();

        }

        public void Run()
        {
            
           var solutionsGenerator= new SolutionsGenerator(Jobs, Breaks);
           Solutions = solutionsGenerator.Generate(50);       
           GeneticOperator geneticOperator= new GeneticOperator(Solutions);
           List<Solution> newSolutions=geneticOperator.CreateNewPopulation(200);
            Console.WriteLine("Bye");
           
        }
    }
}
