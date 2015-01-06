using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACO
{
    public class InputReader
    {
        private readonly string _inputFile;

        public InputReader(string inputFile)
        {
            
            _inputFile = inputFile;
        }

        public List<Job> FindJobs()
        {
            var jobs= new List<Job>();
            using (var sr = new StreamReader(_inputFile))
            {
                
                int numberOfAllJobs = Convert.ToInt32(sr.ReadLine());
                for (int i = 0; i < numberOfAllJobs; i++)
                {
                    string tmp = sr.ReadLine();
                   
                    string[] jobInfo = tmp.Split(';');
                    int firstTime = Convert.ToInt32(jobInfo[0]);
                    int secondTime = Convert.ToInt32(jobInfo[1]);
                    var firstProcessor = (Job.Processor) Convert.ToInt32(jobInfo[2]);
                    var secondProcessor = (Job.Processor) Convert.ToInt32((jobInfo[3]));

                    jobs.Add(new Job(firstTime, secondTime,firstProcessor, secondProcessor));
                   
                }
            }
            return jobs;
        }

        public SortedSet<Interval> FindBreaks()
        {
            var intervals = new SortedSet<Interval>();
            using (var sr = new StreamReader(_inputFile))
            {
                moveToIntervals(sr);
                int startFree = 0;
                while (!sr.EndOfStream)
                {
                    string tmp = sr.ReadLine();
                    string[] intervalInfo = tmp.Split(';');
                    int length = Convert.ToInt32(intervalInfo[1]);
                    int startTime = Convert.ToInt32(intervalInfo[2]);
                    if (startFree < startTime)
                        intervals.Add(new Interval(Interval.TypeOfInterval.Free, startTime - startFree, startFree));
                    intervals.Add(new Interval(Interval.TypeOfInterval.Break, length, startTime));
                    startFree = startTime + length;

                }
                
            }
            return intervals;
        }

      

        private void moveToIntervals(StreamReader sr)
        {
            int numberOfAllJobs = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < numberOfAllJobs; i++)
                sr.ReadLine();
        }
    }
}
