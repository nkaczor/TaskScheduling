using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    public class Mutator
    {
        private Solution _mutatedSolution;
        private int _numberOfTask;
        public int ChangeFactor { get; set; }
        public Solution Mutate(Solution solutionToMutate)
        {
            _mutatedSolution = solutionToMutate.CloneOnlyOrderList();
            _numberOfTask = solutionToMutate.TaskOrder.Count();
            int changes = (_numberOfTask*ChangeFactor)/100;
           
            for (int i = 0; i < changes; i++)
            {
                moveOneItem();
            }
            _mutatedSolution.GenerateProcessorsTimeline();
            return _mutatedSolution;
        }

        private void moveOneItem()
        {
            Random rnd = new Random();
            int index = rnd.Next(_numberOfTask);
            _mutatedSolution.TaskOrder.Remove(index);
            _mutatedSolution.TaskOrder.Add(index);
        }

        public Mutator(int changeFactor=4)
        {
            ChangeFactor = changeFactor;

        }
    }
}
