using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day7
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day7/input.txt");
            List<int> numbers = input.Split(',').Select(int.Parse).ToList();

            Console.WriteLine("Part 1: " + Part1(numbers));
            Console.WriteLine("Part 2: " + Part2(numbers));
        }

        static int Part1(List<int> numbers)
        {
            int totalFuel = 0;
            numbers.Sort();

            int medianIndex = numbers.Count() / 2;
            if (numbers.Count() % 2 != 0)
            {
                int horizontal = numbers[medianIndex];
                for (int i = 0; i < numbers.Count(); i++) totalFuel += Math.Abs(horizontal - numbers[i]);
            }           
            else
            {
                int horizontal1 = numbers[medianIndex];
                int horizontal2 = numbers[medianIndex - 1];
                int totalFuel1 = 0;
                int totalFuel2 = 0;
                for (int i = 0; i < numbers.Count(); i++) totalFuel1 += Math.Abs(horizontal1 - numbers[i]);
                for (int i = 0; i < numbers.Count(); i++) totalFuel2 += Math.Abs(horizontal2 - numbers[i]);
                totalFuel = Math.Min(totalFuel1, totalFuel2);
            }

            return totalFuel;
        }

        static int Part2(List<int> numbers)
        {
            int leastFuel = int.MaxValue;
            for (int i = numbers.Min(); i <= numbers.Max(); i++)
            {
                int fuel = 0;
                foreach (int number in numbers)
                {
                    int increment = Math.Abs(number - i);
                    fuel += (increment * (increment + 1)) / 2;
                }
                if (fuel < leastFuel) leastFuel = fuel;
            }
            return leastFuel;
        }
    }
}
