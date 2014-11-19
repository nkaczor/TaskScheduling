using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    class GeneticOperator
    {
        private readonly List<Solution> _oldsolutions;
        private List<Solution> _newSolutions;

        public GeneticOperator(List<Solution> oldSolutions )
        {
             _oldsolutions = oldSolutions;
            _newSolutions= new List<Solution>();
        }
        public List<Solution> CreateNewPopulation( int numberOfChildren)
        {
          Random rnd= new Random();

            while (numberOfChildren > 0)
            {
                if (rnd.Next(100) > 8) {
                    mutateSomething();
                    numberOfChildren--;
                }
                 else {
                        crossoverSomething();
                        numberOfChildren = numberOfChildren - 2;
                    }

        }

            return _newSolutions;
        }

        private void crossoverSomething()
        {
            throw new NotImplementedException();
        }

        private void mutateSomething()
        {
            throw new NotImplementedException();
        }
    }
}
