using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] list = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day3/input.txt");
            List<string> binaryNumbers = list.ToList();


            //Console.WriteLine("Part 1: " + Part1(binaryNumbers));
            Console.WriteLine("Part 2: " + Part2(binaryNumbers));
        }

        static double Part1(List<string> binaryNumbers)
        {
            string gammaRate = "";
            string epsilonRate = "";

            List<int> ones = new List<int>();
            for (int i = 0; i < binaryNumbers[0].Length; i++) ones.Add(0);

            foreach (string binaryNumber in binaryNumbers)
            {
                for (int i = 0; i < binaryNumber.Length; i++)
                {
                    if (binaryNumber[i] == '1') ones[i]++;

                }
            }
            for (int i = 0; i < ones.Count; i++)
            {
                if (ones[i] > (binaryNumbers.Count - ones[i]) )
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
                else
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
            }

            return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        }

        static double Part2(List<string> binaryNumbers)
        {
            List<string> o2Numbers = new List<string>();
            List<string> co2Numbers = new List<string>();
            for (int i = 0; i < binaryNumbers.Count; i++)
            {
                o2Numbers.Add(binaryNumbers[i]);
                co2Numbers.Add(binaryNumbers[i]);
            }

            int bitSize = binaryNumbers[0].Length;
            for (int i = 0; i < bitSize; i++)
            {

                //// O2 Rating
                List<int> o2Ones = CalculateOnes(o2Numbers);
                if (o2Ones != null)
                {
                    if (o2Ones[i] >= (o2Numbers.Count - o2Ones[i])) // one is most common, zero is least common
                    {
                        int j = 0;
                        while (j < o2Numbers.Count) // remove numbers not matching bit criteria (1)
                        {
                            if (o2Numbers[j][i] != '1')
                            {
                                o2Numbers.RemoveAt(j);
                            }
                            else j++;
                        }
                    }
                    else // zero is most common
                    {
                        int j = 0;
                        while (j < o2Numbers.Count) // remove numbers not matching bit criteria (0)
                        {
                            if (o2Numbers[j][i] != '0')
                            {
                                o2Numbers.RemoveAt(j);
                            }
                            else j++;
                        }
                    }
                }

                // CO2 Rating
                List<int> co2Ones = CalculateOnes(co2Numbers);
                if (co2Ones != null)
                {
                    if (co2Ones[i] > (co2Numbers.Count - co2Ones[i])) // one is most common, zero is least common
                    {
                        int j = 0;
                        while (j < co2Numbers.Count) // remove numbers not matching bit criteria (0)
                        {
                            if (co2Numbers[j][i] != '0')
                            {
                                co2Numbers.RemoveAt(j);
                            }
                            else j++;
                        }
                    }
                    else if (co2Ones[i] == (co2Numbers.Count - co2Ones[i]))
                    {
                        int j = 0;
                        while (j < co2Numbers.Count) // remove numbers not matching bit criteria (0)
                        {
                            if (co2Numbers[j][i] != '0')
                            {
                                co2Numbers.RemoveAt(j);
                            }
                            else j++;
                        }
                    }
                    else // zero is most common
                    {
                        int j = 0;
                        while (j < co2Numbers.Count) // remove numbers not matching bit criteria (0)
                        {
                            if (co2Numbers[j][i] != '1')
                            {
                                co2Numbers.RemoveAt(j);
                            }
                            else j++;
                        }
                    }
                }
                

                if (o2Numbers.Count == 1 && co2Numbers.Count == 1) break;
            }

            double o2Rating = Convert.ToInt32(o2Numbers[0], 2);
            double co2Rating = Convert.ToInt32(co2Numbers[0], 2);
            return o2Rating * co2Rating;
        }

        static List<int> CalculateOnes(List<string> binaryNumbers)
        {
            if (binaryNumbers.Count > 1)
            {
                List<int> ones = new List<int>();
                for (int i = 0; i < binaryNumbers[0].Length; i++) ones.Add(0);

                foreach (string binaryNumber in binaryNumbers)
                {
                    for (int i = 0; i < binaryNumber.Length; i++)
                    {
                        if (binaryNumber[i] == '1') ones[i]++;

                    }
                }
                return ones;
            }
            return null;
        }
    }
}
