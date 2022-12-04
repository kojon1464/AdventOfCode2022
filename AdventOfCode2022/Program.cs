using AdventOfCode2022.Day1;
using AdventOfCode2022.Day2;
using AdventOfCode2022.Day3;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2022
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SolveDay1();
            //SolveDay2();
            SolveDay3();
        }

        public static void SolveDay1()
        {
            Day1Solver.SolveTask1();
            Day1Solver.SolveTask2();
        }

        public static void SolveDay2()
        {
            Day2Solver.SolveTask1();
            Day2Solver.SolveTask2();
        }

        public static void SolveDay3()
        {
            Day3Solver.SolveTask1();
            Day3Solver.SolveTask2();
        }
    }
}