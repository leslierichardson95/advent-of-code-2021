using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day5/input.txt");
            Console.WriteLine("Part 1: " + Solution(1, input));
            Console.WriteLine("Part 2: " + Solution(2, input));
        }
        
        static int Solution(int part, string[] input)
        {
            Dictionary<(int, int), int> map = new Dictionary<(int, int), int>();

            foreach (string line in input)
            {
                int[] points = line.Split(new string[] { "->", "," }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
                (int x1, int y1, int x2, int y2) = (points[0], points[1], points[2], points[3]);

                int xStep = 0;
                int yStep = 0;

                if (x1 == x2) xStep = 0;
                else if (x1 > x2) xStep = -1;
                else xStep = 1;

                if (y1 == y2) yStep = 0;
                else if (y1 > y2) yStep = -1;
                else yStep = 1;

                // check for diagnonals if part 2, else consider only horizontals and verticals
                if (part == 1 && xStep != 0 && yStep != 0) continue;

                (int mapX, int mapY) = (x1, y1);
                while (true)
                {
                    if (!map.ContainsKey((mapX, mapY))) map[(mapX, mapY)] = 0;
                    map[(mapX, mapY)]++;
                    if ((mapX, mapY) == (x2, y2)) break;
                    mapX += xStep;
                    mapY += yStep;
                }
            }

            return map.Count(point => point.Value > 1);
        }
    }
}
