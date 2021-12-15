using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day13/input.txt");

            List<(int,int)> dots = new List<(int,int)> ();
            int i = 0;
            while (!inputs[i].Equals(""))
            {
                string[] coordinates = inputs[i].Split(',');
                dots.Add((int.Parse(coordinates[1]), int.Parse(coordinates[0])));
                i++;
            }

            List<(char, int)> foldInstructions = new List<(char, int)>();
            for (i = i + 1; i < inputs.Length; i++)
            {
                string[] values = inputs[i].Split('=');
                foldInstructions.Add((values[0][values[0].Length - 1], int.Parse(values[1])));
            }

            Console.WriteLine("Part 1: " + Part1(dots, foldInstructions));
            Part2(dots, foldInstructions);
        }

        static int Part1(List<(int,int)> dots, List<(char,int)> foldInstructions)
        {
            (char axis, int foldLine) = foldInstructions[0];
            HashSet<(int, int)> foldedDots = new HashSet<(int, int)>();

            if (axis == 'x') FoldLeft(dots, foldLine, foldedDots);
            else if (axis == 'y') FoldUp(dots, foldLine, foldedDots);

            return foldedDots.Count;
        }

        static void Part2(List<(int,int)> dots, List<(char,int)> foldInstructions)
        {
            HashSet<(int, int)> foldedDots = new HashSet<(int, int)>();

            foreach ((char,int) foldInstruction in foldInstructions)
            {
                int axis = foldInstruction.Item1;
                int foldLine = foldInstruction.Item2;

                if (axis == 'x') FoldLeft(dots, foldLine, foldedDots);
                else if (axis == 'y') FoldUp(dots, foldLine, foldedDots);

                dots = new List<(int,int)>(foldedDots);
                foldedDots.Clear();
            }
            PrintCode(dots);
        }

        static void FoldUp(List<(int,int)> dots, int foldLine, HashSet<(int,int)> foldedDots)
        {
            foreach ((int,int) dot in dots)
            {
                int row = dot.Item1;
                int col = dot.Item2;

                if (row < foldLine) foldedDots.Add((row, col));
                else
                {
                    int newRow = Math.Abs(foldLine - (row - foldLine));
                    foldedDots.Add((newRow, col));
                }               
            }            
        }

        static void FoldLeft(List<(int,int)> dots, int foldLine, HashSet<(int,int)> foldedDots)
        {
            foreach ((int,int) dot in dots)
            {
                int row = dot.Item1;
                int col = dot.Item2;

                if (col < foldLine) foldedDots.Add((row, col));
                else
                {
                    int newCol = Math.Abs(foldLine - (Math.Abs(foldLine - col)));
                    foldedDots.Add((row, newCol));
                }
            }
        }

        static void PrintCode(List<(int,int)> dots) 
        {
            int maxRow = dots.Max(x => x.Item1) + 1;
            int maxCol = dots.Max(x => x.Item2) + 1;

            for (int row = 0; row < maxRow; row++)
            {
                string line = "";
                for (int col = 0; col < maxCol; col++)
                {
                    if (dots.Contains((row, col))) line += '#';
                    else line += '.';
                }
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}
