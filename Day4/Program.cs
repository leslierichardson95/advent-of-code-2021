using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("C:/Users/lerich/OneDrive - Microsoft/source/advent-of-code-2021/Day4/input.txt");
            List<string> inputList = input.Split(new string[] { "\n\n" },
                               StringSplitOptions.RemoveEmptyEntries).ToList();

            List<int> calledNumbers = inputList[0].Split(',').Select(int.Parse).ToList();
            List<BingoCard> cards = new List<BingoCard>();

            for (int i = 1; i < inputList.Count; i++)
            {
                List<string> numbers = inputList[i].Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                cards.Add(new BingoCard(numbers));
            }

            Console.WriteLine("Part 1: " + Part1(calledNumbers, cards));
            Console.WriteLine("Part 2: " + Part2(calledNumbers, cards));
        }

        static int Part1(List<int> calledNumbers, List<BingoCard> cards)
        {
            for (int number = 0; number < calledNumbers.Count; number++)
            {
                foreach (BingoCard card in cards)
                {
                    card.MarkCard(calledNumbers[number]);
                    if (number >= 4) 
                    {
                        if (card.CheckForBingo() == true) return card.GetFinalScore(calledNumbers[number]);
                    } 
                }
            }
            return -1;
        }

        static int Part2(List<int> calledNumbers, List<BingoCard> cards)
        {
            HashSet<BingoCard> winningSet = new HashSet<BingoCard>();
            BingoCard lastWinner = null;
            int lastNumber = 0;

            foreach (int calledNumber in calledNumbers)
            {
                foreach (BingoCard card in cards)
                {
                    if (!winningSet.Contains(card)) card.MarkCard(calledNumber);

                    if (card.CheckForBingo() == true) 
                    {
                        if (!winningSet.Contains(card))
                        {
                            winningSet.Add(card);
                            lastWinner = card;
                            lastNumber = calledNumber;
                        }       
                    }
                }
            }
            return lastWinner.GetFinalScore(lastNumber); ;
        }
    }

    public class BingoCard
    {
        public int[,] card;
        public bool[,] isMarked;

        public BingoCard(List<string> numbers)
        {
            card = new int[5,5];
            isMarked = new bool[5,5];

            for (int i = 0; i < numbers.Count; i++)
            {
                card[i / 5, i % 5] = int.Parse(numbers[i]);
                isMarked[i / 5, i % 5] = false;
            }         
        }
        
        public void MarkCard(int number)
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (card[row, col] == number)
                    {
                        isMarked[row, col] = true;
                    }
                }
            }
        }

        public bool CheckForBingo()
        {
            for (int i = 0; i < 5; i++)
            {
                if (isMarked[i, 0] && isMarked[i, 1] && isMarked[i, 2] && isMarked[i, 3] && isMarked[i, 4])
                    return true;
                else if (isMarked[0, i] && isMarked[1, i] && isMarked[2, i] && isMarked[3, i] && isMarked[4, i])
                    return true;               
            }
            return false;
        }

        public int GetFinalScore(int lastNumber)
        {
            int score = 0;
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 5; col++)
                {
                    if (!isMarked[row, col]) score += card[row, col];
                }
            }
            return score * lastNumber;
        }
    }
}
