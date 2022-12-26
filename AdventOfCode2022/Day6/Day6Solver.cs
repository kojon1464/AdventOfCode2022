namespace AdventOfCode2022.Day6
{
    public static class Day6Solver
    {
        public static void SolveTask1()
        {
            Solve(4);
        }

        public static void Solve(int numberOfDistinctCharacters)
        {
            using (StreamReader reader = new StreamReader(@"Day6\input1.txt"))
            {
                char[] signal = reader.ReadToEnd().ToCharArray();
                int index = 0;
                Dictionary<char, int> dictionary = new Dictionary<char, int>();

                while (dictionary.Count < numberOfDistinctCharacters)
                {
                    dictionary.AddSignalElement(signal[index]);

                    if (index - numberOfDistinctCharacters >= 0)
                    {
                        dictionary.RemoveSignalElement(signal[index - numberOfDistinctCharacters]);
                    }

                    index++;
                }

                Console.WriteLine(index);
            }
        }

        public static void AddSignalElement(this Dictionary<char, int> dictionary, char element)
        {
            if(dictionary.ContainsKey(element))
            {
                dictionary[element] = dictionary[element] + 1;
            }
            else
            {
                dictionary.Add(element, 1);
            }
        }

        public static void RemoveSignalElement(this Dictionary<char, int> dictionary, char element)
        {
            if (dictionary.ContainsKey(element))
            {
                if (dictionary[element] > 1)
                {
                    dictionary[element]--;
                }
                else
                {
                    dictionary.Remove(element);
                }
            }
        }

        public static void SolveTask2() 
        {
            Solve(14);
        }
    }
}
