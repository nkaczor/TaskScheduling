using System;
using System.Collections.Generic;

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

        public Solution Parent1 { get; set; }
        public Solution Parent2 { get; set; }

        public void Initialize(Solution parent1, Solution parent2)
        {
            Parent1 = parent1;
            Parent2 = parent2;
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

            ////int numberOfOperation = Child1.TaskOrder.Count;
            ////state = new bool[numberOfOperation];
            ////state.Initialize();
            ////reverseCycle = true;
            ////for (int i = 0; i < numberOfOperation; i++)
            ////{
            ////    if (state[i] == false) findCycle(i);

            ////}
            ////Child1.CheckAndFix();
            ////Child2.CheckAndFix();
            /// 
            var numberOfOperation = Parent1.TaskOrder.Count;
            int interceptPoint = rnd.Next((int) (0.1*numberOfOperation), (int)( 0.9*numberOfOperation));

            child1 = Parent1.Clone();
            child2 = Parent2.Clone();
            child1.TaskOrder.InsertRange(0,Parent1.TaskOrder.GetRange(0, interceptPoint));
           
            SortedDictionary<int, int> child1Order=new SortedDictionary<int, int>();
            SortedDictionary<int, int> child2Order= new SortedDictionary<int, int>();
            foreach (var task in Parent1.TaskOrder.GetRange(interceptPoint, numberOfOperation - interceptPoint))
            {
                child1Order.Add(Parent2.TaskOrder.FindIndex(x => x == task), task);
            }
            foreach (var t in child1Order)
            {
                child1.TaskOrder.Add(t.Value);                
            }
            foreach (var task in Parent2.TaskOrder.GetRange(0, interceptPoint))
            {
                child2Order.Add(Parent1.TaskOrder.FindIndex(x => x == task), task);
            }
            foreach (var t in child2Order)
            {
                child2.TaskOrder.Add(t.Value);
            }
            child2.TaskOrder.InsertRange(child2.TaskOrder.Count,
               Parent2.TaskOrder.GetRange(interceptPoint, numberOfOperation - interceptPoint));



        }

        //private void findCycle(int start)
        //{
        //    var old_i = start;
        //    var value = Child2.TaskOrder[old_i];
        //    var i = Child1.TaskOrder.FindIndex(x => x == value);
        //    if (reverseCycle) swapNum(old_i);
        //    state[start] = true;
        //    while (i != -1 && i != start) // -1 dla cyklow z przekrecaniem (nigdy nie osiagnie startu)
        //    {
        //        state[i] = true;
        //        old_i = i;
        //        value = Child2.TaskOrder[old_i];
        //        i = Child1.TaskOrder.FindIndex(x => x == value);
        //        if (reverseCycle) swapNum(old_i);
        //    }
            
        //    reverseCycle = !reverseCycle;
        //}

        //private void swapNum(int i)
        //{
        //    var tempswap = Child1.TaskOrder[i];
        //    Child1.TaskOrder[i] = Child2.TaskOrder[i];
        //    Child2.TaskOrder[i] = tempswap;
        //}
    }
}