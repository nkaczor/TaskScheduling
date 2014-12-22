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
        private CrossoverOperator _crossoverOperator;
        public GeneticOperator(List<Solution> oldSolutions)
        {
            _oldSolutions = oldSolutions;
            _newSolutions = new List<Solution>();
            _rnd = new Random();
            _mutator = new Mutator(12);
            _crossoverOperator=new CrossoverOperator();
        }
        public List<Solution> CreateNewPopulation(int numberOfChildren)
        {
            
            
            while (numberOfChildren > 0)
            {
                
                if (_rnd.Next(100) < 8)
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
          
            int randomIndex1 = _rnd.Next(_oldSolutions.Count);
            int randomIndex2;
            do
            {
                randomIndex2 = _rnd.Next(_oldSolutions.Count);
             
            } while (randomIndex1 == randomIndex2);
            Solution solution1ToMutate = _oldSolutions[randomIndex1];
            Solution solution2ToMutate = _oldSolutions[randomIndex2];
            _crossoverOperator.Initialize(solution1ToMutate,solution2ToMutate);
            Solution child1, child2;
            
            _crossoverOperator.DoCrossover(out child1, out child2);
            _newSolutions.Add(child1);
            _newSolutions.Add(child2);

        }

        private void mutateSomething()
        {
            
            int randomIndex = _rnd.Next(_oldSolutions.Count);
            Solution solutionToMutate = _oldSolutions[randomIndex];
            
            _newSolutions.Add(_mutator.Mutate(solutionToMutate));
        }
    }
}