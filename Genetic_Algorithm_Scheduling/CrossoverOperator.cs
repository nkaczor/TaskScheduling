using System;

namespace Genetic_Algorithm_Scheduling
{
    public class CrossoverOperator
    {
        private readonly Random rnd;
        private bool reverseCycle;
        private bool[] state;

        public CrossoverOperator()
        {
            rnd = new Random();
        }

        public Solution Child1 { get; set; }
        public Solution Child2 { get; set; }

        public void Initialize(Solution parent1, Solution parent2)
        {
            Child1 = parent1.Clone();
            Child2 = parent2.Clone();
        }

        public void DoCrossover(out Solution child1, out Solution child2)
        {
            //var numberOfOperation = Child1.TaskOrder.Count;
            //var start = rnd.Next(numberOfOperation);
            //var index = Child1.TaskOrder.FindIndex(x => x == Child2.TaskOrder[start]);
            //int end, length;
            //if (index > start)
            //{
            //    end = rnd.Next(start, numberOfOperation - (index - start));
            //}

            //else
            //{
            //    end = rnd.Next(start, numberOfOperation);
            //}
            //length = end - start + 1;
            //var listToAdd = new int[length];
            //Child2.TaskOrder.CopyTo(start, listToAdd, 0, length);
            //Child2.TaskOrder.RemoveRange(start, length);
            //Child2.TaskOrder.InsertRange(index, listToAdd);

            int numberOfOperation = Child1.TaskOrder.Count;
            state = new bool[numberOfOperation];
            state.Initialize();
            reverseCycle = true;
            for (int i = 0; i < numberOfOperation; i++)
            {
                if (state[i] == false) findCycle(i);

            }
            Child1.CheckAndFix();
            Child2.CheckAndFix();
            child1 = Child1;
            child2 = Child2;
        }

        private void findCycle(int start)
        {
            var old_i = start;
            var value = Child2.TaskOrder[old_i];
            var i = Child1.TaskOrder.FindIndex(x => x == value);
            if (reverseCycle) swapNum(old_i);
            state[start] = true;
            while (i != -1 && i != start) // -1 dla cyklow z przekrecaniem (nigdy nie osiagnie startu)
            {
                state[i] = true;
                old_i = i;
                value = Child2.TaskOrder[old_i];
                i = Child1.TaskOrder.FindIndex(x => x == value);
                if (reverseCycle) swapNum(old_i);
            }
            
            reverseCycle = !reverseCycle;
        }

        private void swapNum(int i)
        {
            var tempswap = Child1.TaskOrder[i];
            Child1.TaskOrder[i] = Child2.TaskOrder[i];
            Child2.TaskOrder[i] = tempswap;
        }
    }
}