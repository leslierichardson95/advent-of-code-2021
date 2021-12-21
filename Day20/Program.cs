using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day20
{
    internal class Program
    {
        static string Algorithm;
        static List<(int Row, int Col)> neighbors;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day20/input.txt");

            Algorithm = input[0];
            Dictionary<(int,int), char> inputImage = new Dictionary<(int,int), char>();

            int ogRowLength = input.Length - 2;
            int ogColLength = input[2].Length;

            for (int row = 0; row < ogRowLength; row++)
            {
                for (int col = 0; col < ogColLength; col++)
                {
                    inputImage.Add((row, col), input[row + 2][col]);
                }
            }
            neighbors = new List<(int Row, int Col)>() { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 0), (0, 1), (1, -1), (1, 0), (1, 1) };

            Console.WriteLine($"Part 1: {Solve(inputImage, 2)}");
            Console.WriteLine($"Part 2: {Solve(inputImage, 50)}");
        }

        static int Solve(Dictionary<(int,int), char> inputImage, int totalIterations)
        {
            Dictionary<(int, int), char> outputImage = new Dictionary<(int, int), char>();

            for (int iteration = 0; iteration < totalIterations; iteration++)
            {
                int xMin = inputImage.Min(pixel => pixel.Key.Item1);
                int yMin = inputImage.Min(pixel => pixel.Key.Item2);
                int xMax = inputImage.Max(pixel => pixel.Key.Item1);
                int yMax = inputImage.Max(pixel => pixel.Key.Item2);

                for (int row = xMin - 1; row <= xMax + 1; row++)
                {
                    for (int col = yMin - 1; col <= yMax + 1; col++)
                    {
                        GetOutputPixel(inputImage, outputImage, row, col, yMin);
                    }
                }

                inputImage = new Dictionary<(int, int), char>(outputImage);
                outputImage.Clear();
            }

            int litPixels = inputImage.Values.Where(c => c == '#').Count();
            return litPixels;
        }

        static void GetOutputPixel(Dictionary<(int, int), char> inputImage, Dictionary<(int, int), char> outputImage, int centerRow, int centerCol, int yMin)
        {
            string binaryString = "";
            List<(int,int)> miniGrid = neighbors.Select(p => (p.Row + centerRow, p.Col + centerCol)).ToList();
            foreach ((int,int) pixel in miniGrid)
            {
                if (!inputImage.ContainsKey(pixel) && (yMin + 1) % 2 == 0 && Algorithm[0] == '#') // check for infinity point
                {
                    binaryString += '#';
                }
                else if (!inputImage.ContainsKey(pixel)) binaryString += '.';
                else binaryString += inputImage[pixel];
            }

            int decimalIndex = GetDecimalNumber(binaryString);
            outputImage[(centerRow, centerCol)] = Algorithm[decimalIndex];
        }

        static int GetDecimalNumber (string binaryString)
        {
            string binary = "";
            foreach (char c in binaryString)
            {
                if (c == '.') binary += '0';
                else binary += '1';
            }

            return Convert.ToInt32(binary, 2);
        }
    }
}
