using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Day14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] inputs = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day14/input.txt");

            string template = inputs[0];
            Dictionary<string, char> insertionRules = new Dictionary<string, char>();
            for (int i = 2; i < inputs.Length; i++)
            {
                string[] rule = inputs[i].Split(new string[] { " -> " }, StringSplitOptions.RemoveEmptyEntries);
                insertionRules.Add(rule[0], rule[1][0]);
            }

            Console.WriteLine("Part 1: " + Part2(template, insertionRules, 40));
        }

        static long Part1(string template, Dictionary<string,char> insertionRules, int totalSteps)
        {
            StringBuilder newTemplate = new StringBuilder();
            newTemplate.Append(template[0]);
            for (int step = 0; step < totalSteps; step++)
            {
                for (int i = 0; i < template.Length - 1; i++)
                {
                    string pair = "";
                    pair += template[i];
                    pair += template[i + 1];

                    if (insertionRules.ContainsKey(pair))
                    {
                        newTemplate.Append(insertionRules[pair]);
                        newTemplate.Append(pair[1]);
                    }
                }
                template = newTemplate.ToString();
                newTemplate.Clear();
                newTemplate.Append(template[0]);
            }

            Dictionary<char, int> commonLetters = template.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
            long mostCommonElement = commonLetters.Values.Max();
            long leastCommonElement = commonLetters.Values.Min();

            return mostCommonElement - leastCommonElement;
        }

        static long Part2(string template, Dictionary<string, char> insertionRules, int totalSteps)
        {
            Dictionary<char, long> commonLetters = new Dictionary<char, long>();
            Dictionary<string, long> pairCounts = new Dictionary<string, long>();
            
            for (int i = 0; i < template.Length - 1; i++)
            {
                string pair = "" + template[i] + template[i + 1];
                if (pairCounts.ContainsKey(pair)) pairCounts[pair]++;
                else pairCounts[pair] = 1;
            }

            Dictionary<string, long> currentPairs = pairCounts;

            foreach (char letter in template)
            {
                if (commonLetters.ContainsKey(letter)) commonLetters[letter]++;
                else commonLetters[letter] = 1;
            }

            for (int step = 0; step < totalSteps; step++)
            {
                Dictionary<string, long> result = new Dictionary<string, long>();
                foreach (KeyValuePair<string, long> kvp in currentPairs)
                {
                    char newLetter = insertionRules[kvp.Key];
                    if (commonLetters.ContainsKey(newLetter)) commonLetters[newLetter] += kvp.Value;
                    else commonLetters[newLetter] = kvp.Value;

                    string pair1 = "" + kvp.Key[0] + insertionRules[kvp.Key];
                    string pair2 = "" + insertionRules[kvp.Key] + kvp.Key[1];
                    if (result.ContainsKey(pair1)) result[pair1] += kvp.Value;
                    else result[pair1] = kvp.Value;

                    if (result.ContainsKey(pair2)) result[pair2] += kvp.Value;
                    else result[pair2] = kvp.Value;
                }
                currentPairs = result;
            }

            long mostCommonElement = commonLetters.Values.Max();
            long leastCommonElement = commonLetters.Values.Min();

            return mostCommonElement - leastCommonElement;
        }
    }
}
