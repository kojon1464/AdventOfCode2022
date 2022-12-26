using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2022.Day7
{
    internal class Day7Solver
    {
        public static void SolveTask1()
        {
            using(StreamReader reader = new StreamReader(@"Day7\input1.txt"))
            {
                int directorySizeAtMost100000 = 0;
                List<int> sizes = new List<int>();

                CheckDirectoriesSize(reader, sizes);

                foreach (var size in sizes)
                {
                    if (size <= 100_000)
                    {
                        directorySizeAtMost100000 += size;
                    }
                }

                Console.WriteLine(directorySizeAtMost100000);
            }
        }

        private static int CheckDirectoriesSize(StreamReader reader, List<int> sizes)
        {
            int directorySize = 0;
            bool directoryChecked = false;

            string? line;

            do
            {
                line = reader.ReadLine();
                object outout = line switch
                {
                    null => directoryChecked = true,
                    var str when str.StartsWith("$ cd ..") => directoryChecked = true,
                    var str when str.StartsWith("$ cd ") => directorySize += CheckDirectoriesSize(reader, sizes),
                    var str when Regex.IsMatch(str, "^\\d") => directorySize += GetDirectorySize(str),
                    _ => true
                };
            }
            while (!directoryChecked);

            sizes.Add(directorySize);

            return directorySize;
        }

        private static int GetDirectorySize(string line)
        {
            int size;
            var words = line.Split(' ');

            int.TryParse(words[0], out size);

            return size;
        }

        public static void SolveTask2() 
        {
            using (StreamReader reader = new StreamReader(@"Day7\input1.txt"))
            {
                int diskSpace = 70_000_000;
                int spaceRequired = 30_000_000;

                int smallest = int.MaxValue;
                List<int> sizes = new List<int>();

                CheckDirectoriesSize(reader, sizes);

                int spaceTaken = sizes.Max();
                int spaceToFree = spaceRequired - (diskSpace - spaceTaken);

                foreach (var size in sizes)
                {
                    if (size < smallest && size >= spaceToFree)
                    {
                        smallest = size;
                    }
                }

                Console.WriteLine(smallest);
            }
        }
    }
}
