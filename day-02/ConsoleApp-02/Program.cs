using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp_02
{
    class Program
    {
        static void Main(string[] args)
        {
            PuzzleOne();
            PuzzleTwo();
        }

        private static void PuzzleOne()
        {
            Console.WriteLine("==== Puzzle 1 ==== ");
            var checksum = File.ReadLines("input.txt")
                .SelectMany(CountLetters)
                .GroupBy(count => count, (count, counts) => counts.Count())
                .Aggregate(1, (result, count) => result * count);
            Console.WriteLine("Checksum: " + checksum);
            Console.ReadKey();
        }

        private static IEnumerable<int> CountLetters(string id)
        {
            return id.GroupBy(
                    character => character,
                    (character, chars) => chars.Count())
                .Where(c => c == 3 || c == 2)
                .Distinct();
        }

        private static void PuzzleTwo()
        {
            Console.WriteLine("==== Puzzle 2 ==== ");
            var ids = File.ReadLines("input.txt").ToList();

            while (ids.Count > 1)
            {
                var candidate = ids.First();
                ids = ids.Skip(1).ToList();

                foreach (var partner in ids)
                {
                    var score = CalculateDifference(candidate, partner);
                    if (score != 1)
                        continue;

                    WriteCommonLetters(candidate, partner);
                    ids.Clear();
                    break;
                }
            }

            Console.ReadKey();
        }

        private static void WriteCommonLetters(string a, string b)
        {
            Console.WriteLine("  id 1: " + a);
            Console.WriteLine("  id 2: " + b);

            var commonCharSequence = a.Zip(b,
                (a1, b1) => a1 == b1
                    ? a1.ToString()
                    : string.Empty
            );
            var commonString = string.Join(string.Empty, commonCharSequence);
            Console.WriteLine("common: " + commonString);
        }

        private static int CalculateDifference(string a, string b)
        {
            return a.Zip(b,
                (a1, b1) => a1 == b1
                    ? 0
                    : 1
                )
                .Sum();
        }
    }
}
