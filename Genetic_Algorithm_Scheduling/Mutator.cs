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
            _mutatedSolution = solutionToMutate.Clone();
            foreach(var task in solutionToMutate.TaskOrder)
            _mutatedSolution.TaskOrder.Add(task);
            _numberOfTask = solutionToMutate.TaskOrder.Count();
            int changes = (_numberOfTask*ChangeFactor)/100;
           
            for (int i = 0; i < changes; i++)
            {
                moveOneItem();
            }
            _mutatedSolution.CheckAndFix();
            return _mutatedSolution;
        }

        private void moveOneItem()
        {
            Random rnd = new Random();
            int index = rnd.Next(_numberOfTask);
            int item = _mutatedSolution.TaskOrder[index];
            _mutatedSolution.TaskOrder.Remove(item);
            index = rnd.Next(_numberOfTask-1);
            _mutatedSolution.TaskOrder.Insert(index, item);
            
            
        }

        public Mutator(int changeFactor=8)
        {
            ChangeFactor = changeFactor;

        }
    }
}
