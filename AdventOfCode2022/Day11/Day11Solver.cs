using System.Numerics;

namespace AdventOfCode2022.Day11
{
    internal class Day11Solver
    {
        public static void SolveTask1()
        {
            var monkies = CreateMonkies(@"Day11\input1.txt");

            for(int i = 0; i < 20; i++)
            {
                foreach(var monkey in monkies)
                {
                    monkey.InspectItems(monkies);
                }
            }

            var result = monkies.Select(m => m.InspectCount).OrderByDescending(m => m).Take(2).Aggregate((x, y) => x * y);

            Console.WriteLine(result);
        }

        private static List<Monkey> CreateMonkies(string filePath)
        {
            var monkies = new List<Monkey>();

            using(StreamReader reader = new StreamReader(filePath))
            {
                var line = reader.ReadLine();
                var singleMonkeyLines = new List<string>();

                while (line != null)
                {
                    if(line.Trim() != "")
                    {
                        singleMonkeyLines.Add(line);
                    }
                    else
                    {
                        monkies.Add(CreateMonkey(singleMonkeyLines));
                        singleMonkeyLines.Clear();
                    }
                    
                    line= reader.ReadLine();
                }

                monkies.Add(CreateMonkey(singleMonkeyLines));
            }

            return monkies;
        }

        private static Monkey CreateMonkey(List<string> singleMonkeyLines)
        {
            var monkey = new Monkey();

            monkey.Items = CreateStartingItems(singleMonkeyLines[1]);
            monkey.WorryIndexChangeFunction = CreateMonkeyOperation(singleMonkeyLines[2]);
            monkey.DivisibleByTestNumber = ParseWordToInt(singleMonkeyLines[3], 3);
            monkey.MonkeyIndexIfTestTrue = ParseWordToInt(singleMonkeyLines[4], 5);
            monkey.MonkeyIndexIfTestFalse = ParseWordToInt(singleMonkeyLines[5], 5);

            return monkey;
        }

        private static LinkedList<BigInteger> CreateStartingItems(string text)
        {
            var items = new LinkedList<BigInteger>();

            var words = text.Trim().Replace(",", "").Split(' ');

            for(int i = 2; i < words.Length; i++)
            {
                int number = 0;

                int.TryParse(words[i], out number);
                items.AddLast(number);
            }

            return items;
        } 

        private static Func<BigInteger, BigInteger> CreateMonkeyOperation(string text)
        {
            var words = text.Trim().Split(" ");

            int number = 0;
            int.TryParse(words[5], out number);

            return words[4] switch
            {
                "+" => (x) => x + number,
                "*" when number == 0 => (x) => x * x,
                "*" => (x) => x * number
            };
        }

        private static int ParseWordToInt(string text, int wordIndex)
        {
            var words = text.Trim().Split(' ');

            int number = 0;
            int.TryParse(words[wordIndex], out number);

            return number;
        }

        public static void SolveTask2() 
        {
            var monkies = CreateMonkies(@"Day11\input1.txt");

            var modulo = 1;

            foreach (var monkey in monkies)
            {
                modulo *= monkey.DivisibleByTestNumber;
            }

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkies)
                {
                    monkey.InspectItems(monkies, false, modulo);
                }
            }

            var result = monkies.Select(m => m.InspectCount).OrderByDescending(m => m).Take(2).ToList();

            Console.WriteLine(result[0] * (BigInteger)result[1]);
        }
    }
}
