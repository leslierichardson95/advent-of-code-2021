using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] list = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day1/input.txt");
            List<int> depths = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                depths.Add(int.Parse(list[i]));
            }

            Console.WriteLine("Part 1: " + Part1(depths));
            Console.WriteLine("Part 2: " + Part2(depths));
        }

        static int Part1(List<int> depths)
        {
            int increases = 0;
            for (int i = 1; i < depths.Count; i++)
            {
                if (depths[i] > depths[i - 1]) increases++;
            }
            return increases;
        }

        static int Part2(List<int> depths)
        {
            int increases = 0;
            for (int i = 2; i < depths.Count - 1; i++)
            {
                if ((depths[i + 1] + depths[i] + depths[i - 1]) > (depths[i] + depths[i - 1] + depths[i - 2])) increases++;
            }
            return increases;
        }
    }
}
