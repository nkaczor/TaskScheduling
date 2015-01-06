using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACO
{
    class Program
    {
        static void Main(string[] args)
        {
            AcoAlgorithm ga = new AcoAlgorithm("test.in");
            ga.Run();
            Console.ReadLine();
        }
    }
}
