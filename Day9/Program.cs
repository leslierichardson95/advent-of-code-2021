using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] file = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day9/input.txt");
            int[,] heightMap = new int[file.Length,file[0].Length];
            for (int i = 0; i < file.Length; i++)
            {
                string row = file[i];
                for (int j = 0; j < file[0].Length; j++)
                {
                    heightMap[i, j] = int.Parse(row.Substring(j,1));
                }
            }

            List<Point> lowPoints = new List<Point>();
            Console.WriteLine("Part 1: " + Part1(heightMap, lowPoints));
            Console.WriteLine("Part 2: " + Part2(heightMap, lowPoints));
        }

        static int Part2(int[,] heightMap, List<Point> lowPoints)
        {
            List<int> basins = new List<int>();
            foreach (Point lowPoint in lowPoints)
            {
                List<Point> basinPoints = new List<Point>();
                basinPoints.Add(lowPoint);
                GetBasinPoints(basinPoints, heightMap);
                basins.Add(basinPoints.Count);
            }
            basins.Sort();
            return basins[basins.Count - 1] * basins[basins.Count - 2] * basins[basins.Count - 3];
        }

        static void GetBasinPoints(List<Point> basinPoints, int[,] heightMap)
        {
            for (int i = 0; i < basinPoints.Count; i++)
            {
                CheckForValidPoint(basinPoints[i].x - 1, basinPoints[i].y, basinPoints, heightMap);
                CheckForValidPoint(basinPoints[i].x + 1, basinPoints[i].y, basinPoints, heightMap);
                CheckForValidPoint(basinPoints[i].x, basinPoints[i].y - 1, basinPoints, heightMap);
                CheckForValidPoint(basinPoints[i].x, basinPoints[i].y + 1, basinPoints, heightMap);
            }
        }

        static void CheckForValidPoint(int col, int row, List<Point> basinPoints, int[,] heightMap)
        {
            if ((row >= 0 && row < heightMap.GetLength(0)) && (col >= 0 && col < heightMap.GetLength(1)))
            {
                if (heightMap[row, col] != 9)
                {
                    foreach (Point basinPoint in basinPoints)
                    {
                        if (basinPoint.x == col && basinPoint.y == row) return;
                    }
                    basinPoints.Add(new Point(col, row, heightMap[row, col]));
                }
            }         
        }

        static int Part1(int[,] heightMap, List<Point> lowPoints)
        {
            int sum = 0;
            for (int row = 0; row < heightMap.GetLength(0); row++)
            {
                for (int col = 0; col < heightMap.GetLength(1); col++)
                {
                    int point = heightMap[row, col];
                    if (row == 0)
                    {
                        if (col == 0)
                        {
                            if (point < heightMap[row + 1, col] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else if (col == heightMap.GetLength(1) - 1)
                        {
                            if (point < heightMap[row + 1, col] && point < heightMap[row, col - 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else
                        {
                            if (point < heightMap[row + 1, col] && point < heightMap[row, col - 1] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                    }
                    else if (row == heightMap.GetLength(0) - 1)
                    {
                        if (col == 0)
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else if (col == heightMap.GetLength(1) - 1)
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row, col - 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row, col - 1] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                    }
                    else
                    {
                        if (col == 0)
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row + 1, col] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else if (col == heightMap.GetLength(1) - 1)
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row + 1, col] && point < heightMap[row, col - 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                        else
                        {
                            if (point < heightMap[row - 1, col] && point < heightMap[row + 1, col] && point < heightMap[row, col - 1] && point < heightMap[row, col + 1])
                            {
                                sum += 1 + point;
                                lowPoints.Add(new Point(col, row, point));
                            }
                        }
                    }
                }
            }

            return sum;
        }
    }

    class Point 
    {
        public int x;
        public int y;
        public int value;

        public Point(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            this.value = value;
        }
    }
}
