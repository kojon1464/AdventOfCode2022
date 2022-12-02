using System.Reflection.PortableExecutable;

namespace AdventOfCode2022.Day1
{
    internal class Day1Solver
    {
        public static void SolveTask1()
        {
            using (StreamReader reader = new StreamReader(@"Day1\input1.txt"))
            {
                Console.WriteLine(CalculateTopElvesCaloriesSum(reader, 1));
            }
        }

        public static void SolveTask2()
        {
            using (StreamReader reader = new StreamReader(@"Day1\input2.txt"))
            {
                Console.WriteLine(CalculateTopElvesCaloriesSum(reader, 3));
            }
        }

        private static int CalculateTopElvesCaloriesSum(StreamReader reader, int numberOfElvesToSum)
        {
            int parsedCalories;
            int calories = 0;
            var priorityQueue = new PriorityQueue<int, int>( Comparer<int>.Create((int a, int b) => { return b-a; }));
            var line = reader.ReadLine();

            while (line != null)
            {
                if (int.TryParse(line, out parsedCalories))
                {
                    calories += parsedCalories;
                }
                else
                {
                    priorityQueue.Enqueue(calories, calories);
                    calories = 0;
                }

                line = reader.ReadLine();
            }

            priorityQueue.Enqueue(calories, calories);

            int caloriesSum = 0;
            
            for(int i = 0; i < numberOfElvesToSum; i++)
            {
                caloriesSum += priorityQueue.Dequeue();
            }

            return caloriesSum;
        }
    }
}
