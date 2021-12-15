using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Day12
{
    internal class Program
    {
        static Queue<Stack<string>> queue = new Queue<Stack<string>>();
        static void Main(string[] args)
        {
            List<string> lines = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day12/input.txt").ToList();

            Dictionary<string, List<string>> nodes = new Dictionary<string, List<string>>();
            foreach (string line in lines)
            {
                string[] caves = line.Split('-');
                if (!nodes.ContainsKey(caves[0])) nodes[caves[0]] = new List<string>();
                if (!nodes.ContainsKey(caves[1])) nodes[caves[1]] = new List<string>();

                if (caves[0] != "start") nodes[caves[1]].Add(caves[0]);
                if (caves[1] != "start") nodes[caves[0]].Add(caves[1]);
            }

            //Console.WriteLine("Part 1: " + Part1(nodes));
            Console.WriteLine("Part 2: " + Part2(nodes));
        }

        static int Part1(Dictionary<string, List<string>> nodes)
        {
            foreach (string cave in nodes["start"])
            {
                GetPaths1(cave, nodes, new Stack<string>());
            }

            return queue.Count();
        }

        static int Part2(Dictionary<string, List<string>> nodes)
        {
            queue = new Queue<Stack<string>>();
            foreach (string cave in nodes["start"])
            {
                GetPaths2(cave, nodes, new Stack<string>());
            }

            return queue.Count();
        }

        static void GetPaths1(string cave, Dictionary<string, List<string>> nodes, Stack<string> path)
        {
            path.Push(cave);
            if (nodes[cave].Contains("end"))
            {
                path.Push("end");
                queue.Enqueue(new Stack<string>(path));
                path.Pop();
            }

            foreach (string item in nodes[cave])
            {
                if (item != "end" && (!path.Contains(item)) || char.IsUpper(item[0])) GetPaths1(item, nodes, new Stack<string>(path));
            }
        }

        static void GetPaths2(string cave, Dictionary<string, List<string>> nodes, Stack<string> path)
        {
            path.Push(cave);

            int timesVisited = 0;
            try { timesVisited = path.Where(x => char.IsLower(x[0])).GroupBy(x => x).Max(x => x.Count()); }
            catch { timesVisited = 0; }

            if (nodes[cave].Contains("end"))
            {
                path.Push("end");
                queue.Enqueue(new Stack<string>(path));
                path.Pop();
            }

            foreach (string item in nodes[cave])
            {
                if (item != "end" && (timesVisited != 2 || !path.Contains(item)) || char.IsUpper(item[0])) GetPaths2(item, nodes, new Stack<string>(path)); ;
            }
        }
    }
}
