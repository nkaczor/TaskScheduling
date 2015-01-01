using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithm_Scheduling
{
    public class Job
    {
        private static int idCounter=1; 
        public Job(int firstTime, int secondTime, Processor firstProcessor, Processor secondProcessor)
        {
            Id = idCounter++;
            FirstTime = firstTime;
            SecondTime = secondTime;
            FirstProcessor = firstProcessor;
            SecondProcessor = secondProcessor;
        }

        public int FirstEndsAt { get; set; }
    
        public int Id { get; private set; }
        public int FirstTime { get; set; }
        public Processor FirstProcessor { get; set; }
        public int SecondTime { get; set; }
        public Processor SecondProcessor { get; set; }
        public enum Processor
        {
           One=1,
           Two=2
        }
    }
}
