namespace AdventOfCode2022.Day2
{
    internal class Day2Solver
    {
        public static void SolveTask1()
        {
            using (StreamReader reader = new StreamReader(@"Day2\input1.txt"))
            {
                var text = reader.ReadToEnd();

                var result = text.Split(Environment.NewLine).Select(x => CalculateRoundResultTask1(x)).Sum();

                Console.WriteLine(result);
            }
        }

        public static void SolveTask2()
        {
            using (StreamReader reader = new StreamReader(@"Day2\input2.txt"))
            {
                var text = reader.ReadToEnd();

                var result = text.Split(Environment.NewLine).Select(x => CalculateRoundResultTask2(x)).Sum();

                Console.WriteLine(result);
            }
        }

        private static int CalculateRoundResultTask1(string roundInput)
        {
            var enemyScore = (int)roundInput[0] - 64;
            var myScore = (int)roundInput[2] - 87;
            var difference = myScore - enemyScore;

            int point = difference switch
            {
                1 or -2 => 6,
                0 => 3,
                -1 or 2 => 0,
                _ => throw new Exception()
            };

            return myScore + point;
        }

        private static int CalculateRoundResultTask2(string roundInput)
        {
            var enemyMove = (int)roundInput[0] - 64;
            var expectedResult = (int)roundInput[2] - 87;

            var myMove = expectedResult switch {
                1 => enemyMove - 1,
                2 => enemyMove,
                3 => enemyMove + 1,
                _ => throw new Exception()
            };

            if(myMove < 1)
            {
                myMove = 3;
            }

            if(myMove > 3)
            {
                myMove = 1;
            }

            return myMove + (expectedResult - 1) * 3;
        }
    }
}
