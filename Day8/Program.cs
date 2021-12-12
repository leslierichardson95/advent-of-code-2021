using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string file = File.ReadAllText("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day8/input.txt");
            //string[] inputs = file.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            string[] inputs = file.Split('\n');

            //List<string> signals = new List<string>();
            //List<string> outputs = new List<string>();
            //foreach (string input in inputs)
            //{
            //    string[] values = input.Split('|');
            //    signals.Add(values[0]);
            //    outputs.Add(values[1]);
            //}

            int total = 0;
            foreach(string input in inputs)
            {
                string[] values = input.Split('|');
                List<string> signal = values[0].Trim().Split(' ').ToList();
                List<string> output = values[1].Trim().Split(' ').ToList();

                total += Part2(signal, output);
            }

            //Console.WriteLine("Part 1: " + Part1(outputs));
            Console.WriteLine("Part 2: " + total);
        }

        static int Part1(List<string> outputs)
        {
            int uniqueSegments = 0;
            foreach (string output in outputs)
            {
                string[] outputValues = output.Split(' ');
                foreach (string outputValue in outputValues)
                {
                    if (outputValue.Length == 2 || outputValue.Length == 4 || 
                        outputValue.Length == 3 || outputValue.Length == 7) uniqueSegments++;
                }
            }
            return uniqueSegments;
        }

        static int Part2(List<string> signals, List<string> outputs)
        {
            int sum = 0;

            string one = signals.Where(s => s.Length == 2).First();

            string seven = signals.Where(s => s.Length == 3).First();
            char aWire = seven.Where(c => !one.Contains(c)).First();

            string four = signals.Where(s => s.Length == 4).First();
            string bd = new string(four.Where(c => !one.Contains(c)).ToArray());

            char dWire = '\0';
            bool dWireFound = false;
            foreach (string option in signals.Where(s => s.Length == 6))
            {
                foreach (char c in bd)
                {
                    if (!option.Contains(c))
                    {
                        dWire = c;
                        dWireFound = true;
                        break;
                    }
                }
                if (dWireFound) break;
            }

            char bWire = bd.Where(c => c != dWire).First();

            string five = signals.Where(s => s.Length == 5 && s.Contains(bWire)).First();
            char fWire = '\0';
            if (five.Contains(one[0])) fWire = one[0];
            else fWire = one[1];

            char cWire = one.Where(c => c != fWire).First();
            char gWire = five.Where(c => c != aWire && c != bWire && c != dWire && c != fWire).First();
            char eWire = signals.Where(s => s.Length == 7).First().Where(c => c != aWire && c != bWire && c != cWire && c != dWire && c != fWire && c != gWire).First();

            Dictionary<char, char> wireDictionary = new Dictionary<char, char>();
            wireDictionary[aWire] = 'a';
            wireDictionary[bWire] = 'b';
            wireDictionary[cWire] = 'c';
            wireDictionary[dWire] = 'd';
            wireDictionary[eWire] = 'e';
            wireDictionary[fWire] = 'f';
            wireDictionary[gWire] = 'g';

            int place = 1000;
            foreach (string output in outputs)
            {
                string translatedNumber = new string(output.Select(c => wireDictionary[c]).ToArray());
                sum += GetValue(translatedNumber) * place;
                place /= 10;
            }

            return sum;
        }

        static int GetValue(string output)
        {
            bool[] bits = new bool[7];
            foreach (char c in output) bits[(int)c - (int)'a'] = true;

            int x = 0;
            int p = 0;
            for (int j = 6; j >= 0; j--)
            {
                if (bits[j]) x += (int)Math.Pow(2, p);
                p++;
            }

            switch (x)
            {
                case 119:
                    return 0;
                case 18:
                    return 1;
                case 93:
                    return 2;
                case 91:
                    return 3;
                case 58:
                    return 4;
                case 107:
                    return 5;
                case 111:
                    return 6;
                case 82:
                    return 7;
                case 127:
                    return 8;
                case 123:
                    return 9;
                default:
                    return -1;

            }
        }
    }
}
