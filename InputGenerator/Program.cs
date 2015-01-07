using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
           Generator gen = new Generator();
            gen.Generate(200,250,"test.in");
            Console.Write("OK");
            Console.ReadLine();

        }
    }
}
