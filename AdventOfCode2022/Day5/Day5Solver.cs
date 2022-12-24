using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day5
{
    internal class Day5Solver
    {
        public static void SolveTask1()
        {
            Solve(false);
        }

        private static void Solve(bool canMoveMultipleCrates)
        {
            using (StreamReader reader = new StreamReader(@"Day5\input1.txt"))
            {
                var stacks = ReadAndCreateStacks(reader);

                var line = reader.ReadLine();

                while (line != null)
                {
                    var words = line.Split(' ');

                    int numberOfCrates;
                    int fromStack;
                    int toStack;

                    int.TryParse(words[1], out numberOfCrates);
                    int.TryParse(words[3], out fromStack);
                    int.TryParse(words[5], out toStack);

                    var queue = new Queue<char>();

                    for (int i = 0; i < numberOfCrates; i++)
                    {
                        queue.Enqueue(stacks[fromStack - 1].Pop());
                    }

                    if (canMoveMultipleCrates)
                    {
                        queue = new Queue<char>(queue.Reverse());
                    }

                    for (int i = 0; i < numberOfCrates; i++)
                    {
                       stacks[toStack - 1].Push(queue.Dequeue());
                    }

                    line = reader.ReadLine();
                }

                var result = "";

                foreach (var stack in stacks)
                {
                    result += stack.Pop();
                }

                Console.WriteLine(result);
            }
        }

        private static List<Stack<char>> ReadAndCreateStacks(StreamReader reader)
        {
            var stacks = new List<Stack<char>>();

            var lines = new List<string>();
            var line = reader.ReadLine();

            while(line != null && line.Trim().Length > 0)
            {
                lines.Add(line);
                line = reader.ReadLine();
            }

            int numberOfStacks = lines.Last().Replace(" ", "").Length;

            for(int i = 0; i < numberOfStacks; i++)
            {
                var stack = new Stack<char>();
                for(int j = lines.Count - 2; j >= 0; j--)
                {
                    var container = lines[j][1 + (4 * i)];
                    if (container != ' ')
                    {
                        stack.Push(container);
                    }
                }
                stacks.Add(stack);
            }

            return stacks;
        }

        public static void SolveTask2() 
        {
            Solve(true);
        }
    }
}
