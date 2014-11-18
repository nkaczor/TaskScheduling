using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetic_Algorithm_Scheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            GeneticAlgorithm ga= new GeneticAlgorithm("test.in");
            ga.Run();
            Console.ReadLine();
        }
    }
}
