using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day6/input.txt");
            List<int> fishes = input.Split(',').Select(int.Parse).ToList();

            Console.WriteLine("Part 1: " + Part1(fishes, 80));

            fishes = input.Split(',').Select(int.Parse).ToList();
            Console.WriteLine("Part 2: " + Part2(fishes, 256));
        }

        static int Part1(List<int> fishes, int totalDays)
        {
            for (int day = 0; day < totalDays; day++)
            {
                int newFish = 0;
                for (int i = 0; i < fishes.Count; i++)
                {
                    if (fishes[i] == 0)
                    {
                        fishes[i] = 6;
                        newFish++;
                    }
                    else fishes[i]--;
                }
                if (newFish > 0)
                {
                    for (int i = 0; i < newFish; i++) fishes.Add(8);
                }
            }

            return fishes.Count;
        }

        static long Part2 (List<int> fishes, int totalDays)
        {
            long[] newFish = new long[10];
            foreach (int fish in fishes) newFish[fish]++;

            for (int day = 0; day < totalDays; day++)
            {
                newFish[7] += newFish[0];
                newFish[9] = newFish[0];
                for (int i = 0; i < 9; i++)
                {
                    newFish[i] = newFish[i + 1];
                }                   
                newFish[9] = 0;
            }

            return newFish.Sum();
        }
    }
}
