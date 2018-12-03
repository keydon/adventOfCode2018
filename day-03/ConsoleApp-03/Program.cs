using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp_03
{
    class Program
    {
        static readonly Dictionary<Point, int> map = new Dictionary<Point, int>();

        static void Main(string[] args)
        {
            var fabrics = File.ReadLines("input.txt")
                .Select(Fabric.Parse).ToList();

            foreach (var fabric in fabrics)
            {
                for (int x = 0; x < fabric.Width; x++)
                {
                    for (int y = 0; y < fabric.Height; y++)
                    {
                        var point = new Point(
                            fabric.Left + x,
                            fabric.Top + y);

                        var count = map.ContainsKey(point)
                            ? map[point]
                            : 0;

                        map[point] = ++count;
                    }
                }
            }

            var overlapCount = map.Values.Count(c => c > 1);
            Console.WriteLine("Overlap-Count: " + overlapCount);

            foreach (var fabric in fabrics)
            {
                var isFreeOfOverlaps = true;
                for (int x = 0; x < fabric.Width; x++)
                {
                    for (int y = 0; y < fabric.Height; y++)
                    {
                        var point = new Point(
                            fabric.Left + x,
                            fabric.Top + y);

                        var count = map[point];
                        if (count > 1)
                            isFreeOfOverlaps = false;
                    }
                }

                if (isFreeOfOverlaps)
                {
                    Console.WriteLine("Id free of overlaps: " + fabric.Id);
                }
            }
            Console.ReadKey();
        }
    }

    internal class Fabric
    {
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Left)}: {Left}, {nameof(Top)}: {Top}, {nameof(Width)}: {Width}, {nameof(Height)}: {Height}, {nameof(Original)}: {Original}";
        }

        public static Fabric Parse(string fabric)
        {
            var match = Regex.Match(fabric, @"^#(\d+) @ (\d+),(\d+): (\d+)x(\d+)$");

            return new Fabric()
            {
                Original = fabric,
                Id = match.Groups[1].Value,
                Left = int.Parse(match.Groups[2].Value),
                Top = int.Parse(match.Groups[3].Value),
                Width = int.Parse(match.Groups[4].Value),
                Height = int.Parse(match.Groups[5].Value),
            };
        }

        public string Id { get; set; }

        public string Original { get; set; }

        public int Top { get; set; }

        public int Left { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }
    }
}
