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
            var numberOfOperation = Parent1.TaskOrder.Count;
            int interceptPoint = rnd.Next((int)0.1 * numberOfOperation,(int) 0.9 * numberOfOperation );

            child1 = Parent1.Clone();
            child2 = Parent2.Clone();
            child1.TaskOrder.InsertRange(0, Parent1.TaskOrder.GetRange(0, interceptPoint));

            SortedDictionary<int, int> child1Order = new SortedDictionary<int, int>();
            SortedDictionary<int, int> child2Order = new SortedDictionary<int, int>();
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
            //var numberOfOperation = Parent1.TaskOrder.Count;
            //int interceptPoint1 = rnd.Next((int)(0.6 * numberOfOperation));
            //int interceptPoint2 = rnd.Next(interceptPoint1, numberOfOperation);
            //child1 = Parent1.Clone();
            //child2 = Parent2.Clone();


            //SortedDictionary<int, int> child1Order = new SortedDictionary<int, int>();
            //SortedDictionary<int, int> child2Order = new SortedDictionary<int, int>();
            //child1.TaskOrder.InsertRange(0, Parent1.TaskOrder.GetRange(0, interceptPoint1));
            //foreach (var task in Parent1.TaskOrder.GetRange(interceptPoint1, interceptPoint2 - interceptPoint1))
            //{
            //    child1Order.Add(Parent2.TaskOrder.FindIndex(x => x == task), task);
            //}
            //foreach (var t in child1Order)
            //{
            //    child1.TaskOrder.Add(t.Value);
            //}
            //child1.TaskOrder.InsertRange(0, Parent1.TaskOrder.GetRange(interceptPoint2, numberOfOperation - interceptPoint2));



            //foreach (var task in Parent2.TaskOrder.GetRange(0, interceptPoint1))
            //{
            //    child2Order.Add(Parent1.TaskOrder.FindIndex(x => x == task), task);
            //}
            //foreach (var t in child2Order)
            //{
            //    child2.TaskOrder.Add(t.Value);
            //}
            //child2.TaskOrder.InsertRange(child2.TaskOrder.Count,
            //Parent2.TaskOrder.GetRange(interceptPoint1, interceptPoint2 - interceptPoint1));

            //child2Order.Clear();
            //foreach (var task in Parent2.TaskOrder.GetRange(interceptPoint2, numberOfOperation - interceptPoint2))
            //{
            //    child2Order.Add(Parent1.TaskOrder.FindIndex(x => x == task), task);
            //}
            //foreach (var t in child2Order)
            //{
            //    child2.TaskOrder.Add(t.Value);
            //}
           

        }

       
    }
}