using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleAppDay_01
{
    class Program
    {
        private static readonly HashSet<int> reachedFrequencies = new HashSet<int>();
        static void Main(string[] args)
        {
            PuzzleOne();
            PuzzeTwo();
        }

        private static void PuzzleOne()
        {
            Console.WriteLine("==== Puzzle 1 ==== ");
            Console.Write("Starting frequency: ");
            var startingFrequency = new List<string> { Console.ReadLine() };
            var sumOfChanges =   startingFrequency
                .Concat(File.ReadLines("input.txt"))
                .Select(int.Parse)
                .Sum();
            Console.WriteLine("resulting frequency: {0}", sumOfChanges);
            Console.ReadKey();
        }

        private static void PuzzeTwo()
        {
            Console.WriteLine("==== Puzzle 2 ==== ");
            Console.Write("Starting frequency: ");
            var startingFrequency = new List<string> { Console.ReadLine() };
            try
            {
                var aggregateOfChanges = startingFrequency
                    .Concat(ReadInputIndefinitely())
                    .Select(int.Parse)
                    .Aggregate(AggregateFrequencChangeAndCheckForDuplicateResultungFreq);
                Console.WriteLine("resulting frequency: {0}", aggregateOfChanges);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static int AggregateFrequencChangeAndCheckForDuplicateResultungFreq(int currentFrequency, int frequencyChange)
        {
            var resultingFrequency = currentFrequency + frequencyChange;
            if (reachedFrequencies.Contains(resultingFrequency))
            {
                throw new Exception("First frequency reached twice is: " + resultingFrequency);
            }

            reachedFrequencies.Add(resultingFrequency);
            return resultingFrequency;
        }

        private static IEnumerable<string> ReadInputIndefinitely()
        {
            while (true)
            {
                foreach (var line in File.ReadLines("input.txt"))
                {
                    yield return line;
                }
            }
        }
    }
}
