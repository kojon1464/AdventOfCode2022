using System.Runtime.CompilerServices;

namespace AdventOfCode2022.Day4
{
    internal class Day4Solver
    {
        public static void SolverTask1()
        {
            Solve(true);
        } 

        private static (int, int) CreateAssigmentTuple(string text)
        {
            var stringNumbers = text.Split("-");
            int resultStart;
            int resultEnd;

            int.TryParse(stringNumbers[0], out resultStart);
            int.TryParse(stringNumbers[1], out resultEnd);

            return (resultStart, resultEnd);
        }

        public static void SolverTask2() 
        {
            Solve(false);
        }

        private static void Solve(bool wholeOverlap)
        {
            using (StreamReader reader = new StreamReader(@"Day4\input1.txt"))
            {
                int count = 0;
                string? line = reader.ReadLine();

                while (line != null)
                {
                    var elvesAssigments = line.Split(',');

                    var evlesAssigmentSortedTuples = elvesAssigments.Select(i => CreateAssigmentTuple(i)).OrderBy(t => t.Item1).ThenByDescending(t => t.Item2).ToArray();

                    if (evlesAssigmentSortedTuples[1].Item1 <= evlesAssigmentSortedTuples[0].Item2 &&
                        (!wholeOverlap || evlesAssigmentSortedTuples[1].Item2 <= evlesAssigmentSortedTuples[0].Item2))
                    {
                        count++;
                    }

                    line = reader.ReadLine();
                }

                Console.WriteLine(count);
            }
        }
    }
}
