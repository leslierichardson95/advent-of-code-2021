using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = File.ReadAllLines("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day10/input.txt").ToList();

            Console.WriteLine("Part 1: " + Part1(lines));
            Console.WriteLine("Part 2: " + Part2(lines));
        }

        static int Part1(List<string> lines)
        {
            int score = 0;
            List<string> corruptedLines = new List<string>();
            foreach (string line in lines)
            {
                Stack<char> stack = new Stack<char>();
                foreach (char c in line)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<') stack.Push(c);
                    else if (c == ')' || c == ']' || c == '}' || c == '>')
                    {
                        if (stack.Peek() == '(' && c == ')' ||
                            stack.Peek() == '[' && c == ']' ||
                            stack.Peek() == '{' && c == '}' ||
                            stack.Peek() == '<' && c == '>') stack.Pop();
                        else
                        {
                            if (c == ')') score += 3;
                            else if (c == ']') score += 57;
                            else if (c == '}') score += 1197;
                            else if (c == '>') score += 25137;

                            corruptedLines.Add(line);
                            break;
                        }
                    }
                }
            }
            foreach (string corruptedLine in corruptedLines) lines.Remove(corruptedLine);

            return score;
        }

        static long Part2 (List<string> lines)
        {
            List<long> scores = new List<long>();
            foreach (string line in lines)
            {
                Stack<char> stack = new Stack<char>();
                foreach (char c in line)
                {
                    if (c == '(' || c == '[' || c == '{' || c == '<') stack.Push(c);
                    else if (c == ')' || c == ']' || c == '}' || c == '>')
                    {
                        if (stack.Peek() == '(' && c == ')' ||
                            stack.Peek() == '[' && c == ']' ||
                            stack.Peek() == '{' && c == '}' ||
                            stack.Peek() == '<' && c == '>') stack.Pop();
                    }
                }

                string closingChars = "";
                int count = stack.Count;
                for (int i = 0; i < count; i++)
                {
                    char c = stack.Pop();
                    if (c == '(') closingChars += ')';
                    else if (c == '[') closingChars += ']';
                    else if (c == '{') closingChars += '}';
                    else if (c == '<') closingChars += '>';
                }

                long score = 0;
                for (int i = 0; i < closingChars.Length; i++)
                {
                    score *= 5;
                    if (closingChars[i] == ')') score += 1;
                    else if (closingChars[i] == ']') score += 2;
                    else if (closingChars[i] == '}') score += 3;
                    else if (closingChars[i] == '>') score += 4;
                }
                scores.Add(score);
            }

            scores.Sort();
            long middleScore = scores[scores.Count/2];
            return middleScore;
        }
    }
}
