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
            ////Random rnd= new Random();
            ////List<int> list = new List<int> {1, 2, 3, 4, 5, 6,7,8,9,10,11,12};
            ////int interceptPoint = rnd.Next((int)(0.3 *list.Count), (int)(0.7 * list.Count));
            
            ////foreach( var a in list.GetRange(0, interceptPoint)) Console.WriteLine(a);
            ////Console.WriteLine("fdsfdsf"+ interceptPoint);
            ////foreach (var a in list.GetRange(interceptPoint, list.Count - interceptPoint)) Console.WriteLine(a);
            

            GeneticAlgorithm ga= new GeneticAlgorithm("test.in");
            ga.Run();
            Console.ReadLine();
        }
    }
}
