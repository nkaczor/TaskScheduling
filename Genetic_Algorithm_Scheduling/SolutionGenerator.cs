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
            
            Solution newSolution= new Solution(_breaks,_jobs);
            List<int> unassignedJobs= new List<int>();
            Console.WriteLine(_jobs.Count);
            unassignedJobs.AddRange(Enumerable.Range(1, _jobs.Count));   //dodatnie liczby symbolizuja pierwsze zadania, a ujemne drugie
            Random rnd= new Random();
            while (unassignedJobs.Count > 0)
            {
                

                int randomJob = unassignedJobs[rnd.Next(unassignedJobs.Count)];

                if (randomJob > 0)
                {
                    newSolution.TaskOrder.Add(randomJob);
                    unassignedJobs.Remove(randomJob);
                    unassignedJobs.Add(-randomJob);
                    newSolution.AddFirst(_jobs[randomJob - 1]);
                    
                }
                else
                {
                    
                    newSolution.TaskOrder.Add(randomJob);
                    unassignedJobs.Remove(randomJob);
                    randomJob = -randomJob;
                    newSolution.AddSecond(_jobs[randomJob - 1]);
                }
            }

            return newSolution;
        }
    }
}
