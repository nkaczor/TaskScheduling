using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Genetic_Algorithm_Scheduling
{
    class Roulette
    {
        private int [] _fitnessLevels;
        private bool [] _chosenOnes;
        private List<Solution> _luckySolutions;
        private List<Solution> _solutions;
        private int _range;
        
        public Roulette(List<Solution> solutions)
        {
           
            Int32 maxEndTime = solutions.Max(s=>s.EndTime);
            _solutions = solutions;
            _luckySolutions=new List<Solution>();
            _fitnessLevels=new int[solutions.Count];
            _chosenOnes= new bool[solutions.Count];

            int fitnessLevel=0;
            int i = 0;
            foreach (var solution in solutions)
            {
                int rank = maxEndTime - solution.EndTime;
                
                fitnessLevel += rank;
                _fitnessLevels[i]=fitnessLevel;
                i++;
            }
            _range = fitnessLevel;
           
        }

        public List<Solution> SpinTheWheel(int amount)
        {
            var rnd= new Random();

            while (amount > 0)
            {

                int ball = rnd.Next(_range);
                int index = binarySearch(_fitnessLevels, ball);
                if (index < 0) index = -index;
                if (_chosenOnes[index] == false)
                {
                    _luckySolutions.Add(_solutions[index]);
                    _chosenOnes[index] = true;
                    amount--;
                }

            }
            return _luckySolutions;
        }

         private static int binarySearch(int[] tab, int value)
         {
             
             int left = 0, right = tab.Count()-1;
             int mid = (left + right)/2;
             while (left<right)
             {
                  mid = (left + right)/2;
                 if (tab[mid] > value) right = mid;
                 else left = mid+1;
                

             } 
             
             return left;



         }
    }
}
