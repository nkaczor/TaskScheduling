using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    public class SolutionsGenerator
    {
        private readonly List<Job> _jobs;
        private readonly SortedSet<Interval> _breaks;

        public SolutionsGenerator(List<Job> jobs, SortedSet<Interval> breaks)
        {
            // TODO: Complete member initialization
            _jobs = jobs;
            _breaks = breaks;
        }

        public List<Solution> Generate(int numberOfSolutions)
        {
           var solutions= new List<Solution>();
            for (int i = 0; i < numberOfSolutions ; i++)
            {
                solutions.Add(generateOne());
            }
            return solutions;
        }

        private Solution generateOne()
        {
            RandomSolution newSolution= new RandomSolution(_breaks);
            List<int> unassignedJobs= new List<int>(); 
            unassignedJobs.AddRange(Enumerable.Range(0, _jobs.Count));
            Random rnd= new Random();
            while (unassignedJobs.Count > 0)
            {

                int randomJob = unassignedJobs[rnd.Next(unassignedJobs.Count)];
                unassignedJobs.Remove(randomJob);
                newSolution.AddJob(_jobs[randomJob]);

            }

            return newSolution;
        }
    }
}
