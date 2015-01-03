using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_Scheduling
{
    public class SolutionsGenerator
    {
        private readonly List<Job> _jobs;
        private readonly SortedSet<Interval> _breaks;
        private Random _rnd;
        public SolutionsGenerator(List<Job> jobs, SortedSet<Interval> breaks)
        {
            _jobs = jobs;
            _breaks = breaks;
            _rnd= new Random();
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
            
            Solution newSolution= new Solution(_breaks,_jobs);
            List<int> unassignedJobs= new List<int>();
            unassignedJobs.AddRange(Enumerable.Range(1, _jobs.Count));   //dodatnie liczby symbolizuja pierwsze zadania, a ujemne drugie
            
            while (unassignedJobs.Count > 0)
            {
                

                int randomJob = unassignedJobs[_rnd.Next(unassignedJobs.Count)];

                if (randomJob > 0)
                {
                    newSolution.TaskOrder.Add(randomJob);
                    unassignedJobs.Remove(randomJob);
                    unassignedJobs.Add(-randomJob);
                    
                }
                else
                {
                    
                    newSolution.TaskOrder.Add(randomJob);
                    unassignedJobs.Remove(randomJob);
                    
                }
            }

            return newSolution;
        }
    }
}
