using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] list = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day2/input.txt");
            List<string> commands = list.ToList();


            Console.WriteLine("Part 1: " + Part1(commands));
            Console.WriteLine("Part 2: " + Part2(commands));
        }

        static int Part1(List<string> commands)
        {
            int horizontal = 0;
            int depth = 0;

            foreach (string command in commands)
            {
                string[] split = command.Split(' ');
                if (split[0].Equals("forward"))
                {
                    horizontal += int.Parse(split[1]);
                }
                else if (split[0].Equals("up"))
                {
                    depth -= int.Parse(split[1]);
                }
                else // split[0].Equals("down"))
                {
                    depth += int.Parse(split[1]);
                }
            }

            return horizontal * depth;
        }

        static int Part2(List<string> commands)
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;

            foreach (string command in commands)
            {
                string[] split = command.Split(' ');
                if (split[0].Equals("forward"))
                {
                    horizontal += int.Parse(split[1]);
                    depth = depth + (int.Parse(split[1]) * aim);
                }
                else if (split[0].Equals("up"))
                {
                    aim -= int.Parse(split[1]);
                }
                else // split[0].Equals("down"))
                {
                    aim += int.Parse(split[1]);
                }
            }

            return horizontal * depth;
        }
    }
}
