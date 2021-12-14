using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day11
{
    internal class Program
    {
        public static int totalFlashes = 0;

        static void Main(string[] args)
        {
            List<string> lines = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day11/input.txt").ToList();

            Octopus[,] octopi = new Octopus[lines.Count, lines[0].Length];
            for (int row = 0; row < lines.Count; row++)
            {
                for (int col = 0; col < lines[0].Length; col++)
                {
                    octopi[row,col] = new Octopus(int.Parse(lines[row].Substring(col,1)));
                }
            }

            // Comment out Part 1 before running Part 2
            //Console.WriteLine("Part 1: " + Part1(octopi, 100));
            Console.WriteLine("Part 2: " + Part2(octopi));
        }

        static int Part1(Octopus[,] octopi, int totalSteps)
        {
            //int totalFlashes = 0;
            for (int step = 0; step < totalSteps; step++)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        octopi[row, col].hasFlashed = false;
                    }
                }
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        CheckForFlash(row, col, octopi);
                    }
                }

            }
            return totalFlashes;
        }

        static int Part2(Octopus[,] octopi)
        {
            int step = 1;

            while (true)
            {
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        octopi[row, col].hasFlashed = false;
                    }
                }

                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        CheckForFlash(row, col, octopi);
                    }
                }

                if (AreAllFlashed(octopi)) break;

                step++;
            }

            return step;
        }

        static void CheckForFlash(int row, int col, Octopus[,] octopi)
        {
            if (octopi[row, col].hasFlashed) return;

            octopi[row, col].energyLevel++;

            if (octopi[row, col].energyLevel > 9) // Flash!
            {
                octopi[row, col].energyLevel = 0;
                octopi[row, col].hasFlashed = true;
                totalFlashes++;
                IncreaseNeighbors(row, col, octopi);                              
            }
        }

        static void IncreaseNeighbors(int row, int col, Octopus[,] octopi)
        {
            int minRow = Math.Max(0, row - 1);
            int maxRow = Math.Min(9, row + 1);
            int minCol = Math.Max(0, col - 1);
            int maxCol = Math.Min(9, col + 1);

            for (int i = minRow; i <= maxRow; i++)
            {
                for (int j = minCol; j <= maxCol; j++)
                {
                    CheckForFlash(i, j, octopi);
                }
            }
        }

        static bool AreAllFlashed(Octopus[,] octopi)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (octopi[i, j].energyLevel != 0) return false;
                }
            }
            return true;
        }
    }

    class Octopus
    {
        public int energyLevel;
        public bool hasFlashed;

        public Octopus(int energyLevel)
        {
            this.energyLevel = energyLevel;
            hasFlashed = false;
        }
    }
}
