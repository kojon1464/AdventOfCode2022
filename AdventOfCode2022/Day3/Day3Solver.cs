namespace AdventOfCode2022.Day3
{
    internal class Day3Solver
    {
        public static void SolveTask1()
        {
            using (StreamReader reader = new StreamReader(@"Day3\input1.txt"))
            {
                var line = reader.ReadLine();
                var sum = 0;
                var hashSet = new HashSet<int>();

                while(line != null)
                {
                    var itemsInCompartment = line.Length / 2;
                    var firstCompartemnt = line.Substring(0, itemsInCompartment);
                    var secondCompartment = line.Substring(itemsInCompartment);

                    foreach(var item in firstCompartemnt)
                    {
                        var priority = GetItemPriority(item);
                        hashSet.Add(priority);
                    }

                    foreach(var item in secondCompartment)
                    {
                        var priority = GetItemPriority(item);
                        if (hashSet.Contains(priority))
                        {
                            sum += priority;
                            break;
                        }
                    }

                    hashSet.Clear();
                    line = reader.ReadLine();
                }

                Console.WriteLine(sum);
            }
        }

        public static void SolveTask2()
        {
            using(StreamReader reader = new StreamReader(@"Day3\input2.txt"))
            {
                var line = reader.ReadLine();
                var sum = 0;
                var elfNumber = 0;
                var dictionary = new Dictionary<int, int>();
                var elfItems = new HashSet<int>();

                while (line != null)
                {
                    foreach (var item in line)
                    {
                        var priority = GetItemPriority(item);

                        if (elfItems.Contains(item))
                        {
                            continue;
                        }

                        elfItems.Add(item);
                        if (dictionary.ContainsKey(priority))
                        {
                            dictionary[priority] = dictionary[priority] + 1;
                        }
                        else
                        {
                            dictionary.Add(priority, 1);
                        }
                    }

                    if(elfNumber >= 2)
                    {
                        foreach (var keyValue in dictionary)
                        {
                            if (keyValue.Value >= 3)
                            {
                                sum += keyValue.Key;
                            }
                        }

                        dictionary.Clear();
                    }

                    elfItems.Clear();
                    elfNumber = (elfNumber + 1) % 3;
                    line = reader.ReadLine();
                }

                Console.WriteLine(sum);
            }
        }

        private static int GetItemPriority(char item)
        {
            if (char.IsLower(item))
            {
                return item - 96;
            }
            else
            {
                return item - 64 + 26;
            }
        }
    }
}
