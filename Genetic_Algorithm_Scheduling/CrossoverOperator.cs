using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    public class CrossoverOperator
    {
        public Solution Child1 { get; set; }
        public Solution Child2 { get; set; }
        private bool[] state;
        private bool reverseCycle;
        public void Initialize(Solution parent1, Solution parent2)
        {
            
            Child1 = parent1.Clone();
            Child2 = parent2.Clone();
        }

        public CrossoverOperator()
        {
            // TODO: Complete member initialization
        }

        public void DoCrossover(out Solution child1,out Solution child2)
        {
            if(Child1.TaskOrder.Count()==Child2.TaskOrder.Count()) Console.WriteLine("OK");
            int min = Child1.TaskOrder.Min();
            int max = Child1.TaskOrder.Max();
            for (int i = min; i <= max; i++)
            {
                int l= Child1.TaskOrder.Count(x => x == i);
                if(l!=1 ) Console.WriteLine(i+" "+ l);
             }
        
           
            int numberOfOperation = Child1.TaskOrder.Count;
            state= new bool[numberOfOperation];
            state.Initialize();
            reverseCycle = true;
            for (int i = 0; i < numberOfOperation; i++)
            {
                if (state[i] == false) findCycle(i);
                
            }
            Child1.CheckAndFix();
            Child2.CheckAndFix();
            Console.WriteLine("OK!!!");
            child1 = Child1;
            child2 = Child2;

        }

        private void findCycle(int start)
        {

           // Console.WriteLine(start);
            int old_i=start;
            int value = Child2.TaskOrder[old_i];
            int i = Child1.TaskOrder.FindIndex(x => x == value);
            if (reverseCycle) swapNum(old_i);
            state[start] = true;
            while (i!=start)
            {
                
                state[i] = true;
                old_i = i;
                value = Child2.TaskOrder[old_i];
                //Console.WriteLine(value);
                i = Child1.TaskOrder.FindIndex(x => x == value);
                if (i == -1)
                {
                    Console.WriteLine("X"+value);
                    Console.WriteLine("K"+Child2.TaskOrder.FindIndex(x => x == value));

                }
                if(reverseCycle) swapNum(old_i);
               
            }
            
            //Console.WriteLine();
            reverseCycle = !reverseCycle;
        }
        private void swapNum(int i)
        {

            int tempswap = Child1.TaskOrder[i];
            Child1.TaskOrder[i] = Child2.TaskOrder[i];
            Child2.TaskOrder[i] = tempswap;
        } 
    }
}
