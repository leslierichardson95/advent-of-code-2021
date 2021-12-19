using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = File.ReadAllText("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day17/input.txt");
            string[] inputs = file.Split(new string[] { "target area: x=", "..", ", y=" }, StringSplitOptions.RemoveEmptyEntries);

            int minX = int.Parse(inputs[0]);
            int maxX = int.Parse(inputs[1]);
            int minY = int.Parse(inputs[2]);
            int maxY = int.Parse(inputs[3]);

            Console.WriteLine("Part 1: " + Part1(maxY));
            Console.WriteLine("Part 2: " + Part2(minX, maxX, minY, maxY));
        }

        static int Part1(int minY)
        {          
            int maxHeightY = -minY - 1;
            return maxHeightY * (maxHeightY + 1) / 2;
        }

        static int Part2(int minX, int maxX, int minY, int maxY)
        {
            int velocity = 0;
            int minVelocityX = 0;

            int distance = 0;
            while (distance < maxX)
            {
                velocity++;

                // get max distance for x
                distance = velocity * (velocity + 1) / 2;
                if (distance > minX)
                {
                    minVelocityX = velocity;
                    break;
                }
            }

            int validVelocities = 0;
            for (int i = minVelocityX; i <= maxX; i++)
            {
                for (int j = minY; j <= (-minY - 1); j++)
                {
                    int xPosition = 0;
                    int yPosition = 0;
                    int xVelocity = i;
                    int yVelocity = j;


                    while (xPosition <= maxX && yPosition >= minY)
                    {
                        xPosition += xVelocity;
                        yPosition += yVelocity;
                        if (xVelocity > 0) xVelocity--;
                        yVelocity--;

                        if (xPosition >= minX && xPosition <= maxX && yPosition >= minY && yPosition <= maxY)
                        {
                            validVelocities++;
                            break;
                        }
                    }
                }
            }
            return validVelocities;
        }
    }
}