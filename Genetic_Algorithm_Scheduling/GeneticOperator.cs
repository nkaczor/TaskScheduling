﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    class GeneticOperator
    {
        private readonly List<Solution> _oldSolutions;
        private List<Solution> _newSolutions;
        private Random _rnd;
        private Mutator _mutator;
        public GeneticOperator(List<Solution> oldSolutions)
        {
            _oldSolutions = oldSolutions;
            _newSolutions = new List<Solution>();
            _rnd = new Random();
            _mutator = new Mutator(12);
        }
        public List<Solution> CreateNewPopulation(int numberOfChildren)
        {
            

            while (numberOfChildren > 0)
            {
                if (_rnd.Next(100) > 8)
                {
                    mutateSomething();
                    numberOfChildren--; // z mutacji mamy jedno dziecko
                }
                else
                {
                    crossoverSomething();
                    numberOfChildren = numberOfChildren - 2; // z krzyżowania mamy 2 potomków
                }

            }

            return _newSolutions;
        }

        private void crossoverSomething()
        {
           // throw new NotImplementedException();
        }

        private void mutateSomething()
        {
            int randomIndex = _rnd.Next(_oldSolutions.Count);
            Solution solutionToMutate = _oldSolutions[randomIndex];
            
            _newSolutions.Add(_mutator.Mutate(solutionToMutate));
        }
    }
}