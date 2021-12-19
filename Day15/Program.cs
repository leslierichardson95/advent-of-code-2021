using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    internal class Program
    {
        static (int, int)[] adjacent = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };
        static void Main(string[] args)
        {
            string[] inputs = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day15/input.txt");

            Dictionary<(int,int), Vertex> graph = new Dictionary<(int, int), Vertex>();
            for (int row = 0; row < inputs.Length; row++)
            {
                for (int col = 0; col < inputs[0].Length; col++)
                {
                    graph[(row, col)] = new Vertex(row, col, (int)char.GetNumericValue(inputs[row][col]));
                }
            }
            int width = (int) Math.Sqrt(graph.Count);
            Console.WriteLine("Part 1: " + Part1(graph, graph[(width - 1, width - 1)]));
            Console.WriteLine("Part 2: " + Part2(graph));
        }

        static int Part1(Dictionary<(int,int), Vertex> graph, Vertex destination)
        {
            PriorityQueue<Vertex, int> nextVertex = new PriorityQueue<Vertex, int>();

            graph[(0, 0)].Distance = 0;
            nextVertex.Enqueue(graph[(0, 0)], 0);
            while (nextVertex.Count > 0)
            {
                Vertex currentVertex = nextVertex.Dequeue();
                if (currentVertex.WasVisited) continue;

                currentVertex.WasVisited = true;
                if (currentVertex == destination) return destination.Distance;

                foreach (Vertex neighbor in GetNeighbors(graph, currentVertex))
                {
                    int alternateDistance = currentVertex.Distance + neighbor.RiskLevel;
                    if (alternateDistance < neighbor.Distance) neighbor.Distance = alternateDistance;

                    if (neighbor.Distance != int.MaxValue) nextVertex.Enqueue(neighbor, neighbor.Distance);
                }
            }

            return destination.Distance;
        }

        static int Part2(Dictionary<(int, int), Vertex> graph)
        {
            int width = (int) Math.Sqrt(graph.Count()) * 5;
            int height = (int) Math.Sqrt(graph.Count()) * 5;
            int tileWidth = (int)Math.Sqrt(graph.Count());
            int tileHeight = (int)Math.Sqrt(graph.Count());

            foreach (int row in Enumerable.Range(0, height))
            {
                foreach (int col in Enumerable.Range(0, width))
                {
                    int value = graph[((row % tileWidth), (col % tileHeight))].RiskLevel + row / tileWidth + col / tileHeight;
                    while (value > 9) value -= 9;
                    graph[(row, col)] = new Vertex(row, col, value);
                }
            }
            Dictionary<(int, int), Vertex> newGraph = graph;
            return Part1(newGraph, newGraph[(width - 1, width - 1)]);
        }

        static IEnumerable<Vertex> GetNeighbors(Dictionary<(int, int), Vertex> graph, Vertex vertex)
        {
            foreach ((int i, int j) in adjacent)
            {
                (int, int) key = (vertex.Row + i, vertex.Col + j);
                if (graph.ContainsKey(key) && !graph[key].WasVisited) yield return graph[key];
            }
        }
    }

    class Vertex
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int RiskLevel { get; set; }
        public int Distance { get; set; }
        public bool WasVisited { get; set; }

        public Vertex(int row, int col, int riskLevel)
        {
            Row = row;
            Col = col;
            RiskLevel = riskLevel;
            WasVisited = false;
            Distance = int.MaxValue;
        }
    }
}
