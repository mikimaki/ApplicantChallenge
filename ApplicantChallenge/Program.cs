using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ApplicantChallenge
{
    class Program
    {
        static KeyValuePair<int, List<string>> SumPathPair;
        static int[][] Tree;

        static void Main(string[] args)
        {
            SumPathPair = new KeyValuePair<int, List<string>>(0, new List<string>());
            IEnumerable<string> fileEnumerable = File.ReadLines(@"..\..\Data.txt");
            Tree = new int[fileEnumerable.Count()][];
            int i = 0;

            foreach (string s in fileEnumerable)
            {
                Tree[i] = Array.ConvertAll(s.Split(' '), str => int.Parse(str));
                i++;
            }

            FindPathWithLargestSum(0, 0, 0, "", !NumberIsEven(Tree[0][0]));

            int maxSum = SumPathPair.Key;

            Console.WriteLine($"Max sum: {maxSum}");
            Console.WriteLine("Path(s):");

            foreach (string s in SumPathPair.Value)
            {
                Console.WriteLine($"{s}\n");
            }

            Console.ReadLine();
        }

        static void FindPathWithLargestSum(int i, int j, int sum, string path, bool nextIntIsEven)
        {
            path = $"{path}{Tree[i][j]}";
            sum = sum + Tree[i][j];
            if (i != (Tree.Length - 1))
            {
                path = $"{path}, ";
                int leftNode = Tree[i + 1][j];
                int rightNode = Tree[i + 1][j + 1];
                if (nextIntIsEven == NumberIsEven(leftNode))
                    FindPathWithLargestSum(i + 1, j, sum, path, !nextIntIsEven);
                if (nextIntIsEven == NumberIsEven(rightNode))
                    FindPathWithLargestSum(i + 1, j + 1, sum, path, !nextIntIsEven);
            }
            else
            {
                if (sum > SumPathPair.Key)
                {
                    SumPathPair = new KeyValuePair<int, List<string>>(sum, new List<string>());
                    SumPathPair.Value.Add(path);
                }
                else if (sum == SumPathPair.Key)
                    SumPathPair.Value.Add(path);
            }
        }

        static bool NumberIsEven(int number)
        {
            return ((number % 2) == 0);
        }
    }
}
