using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ACO
{
    public class Ant
    {
        public List<int> Path { get; set; }         // przebyta sciezka     [wykonane zadania]
        private List<int> _freeTasks;     // gdzie mozna sie udac [niewykonane zadania]
        public int NumberOfTask{ get; set; }
        private double[,] _pheremoneLevels;
        private Random _rnd;
        public int SolutionEndTime  { get; set; }
        public Ant(int numberOfTask,Random rnd)
        {
            NumberOfTask = numberOfTask;
            _freeTasks=new List<int>();
            Path= new List<int>();
            _rnd = rnd;

        }

        public void Go(double [,] pheremoneLevels)
        {
            _pheremoneLevels = pheremoneLevels;
            taskInitialize();
            int currentTask = 0;
            while(_freeTasks.Count>0)
            currentTask=step(currentTask);

        }

        private int step(int taskFrom)
        {
            double[] pheremoneList = new double[_freeTasks.Count];
            double currentValue = 0;
            int i = 0;
            foreach (var task in _freeTasks)
            {
                currentValue += _pheremoneLevels[taskFrom, task];
                pheremoneList[i++]=currentValue;
            }
            int taskTo = _freeTasks[roulette(pheremoneList)];
            Path.Add(taskTo);
            _freeTasks.Remove(taskTo);
            if(taskTo<=NumberOfTask) _freeTasks.Add(taskTo+NumberOfTask);
            return taskTo;
        }

        private int roulette(double [] pheremoneList)
        {
            double range = pheremoneList.Last();
            range=_rnd.NextDouble()*range;
            return binarySearch(pheremoneList,range );
        }

        private static int binarySearch(double[] tab, double value)
        {

            int left = 0, right = tab.Count() - 1;
            int mid = (left + right) / 2;
            while (left < right)
            {
                mid = (left + right) / 2;
                if (tab[mid] > value) right = mid;
                else left = mid + 1;


            }

            return left;



        }
        private void taskInitialize()
        {
            Path.Clear();
            _freeTasks.AddRange(Enumerable.Range(1, NumberOfTask)); 
        }



        public void CalculatePheromone(int bestSolutionEndTime)
        {
            double amount = 1/(double) SolutionEndTime;
            for (int i = 0; i < Path.Count-1; i++)
            {
                _pheremoneLevels[Path[i], Path[i + 1]] += amount;
            }
        }
    }
}
