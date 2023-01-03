namespace AdventOfCode2022.Day9
{
    internal class Day9Solver
    {
        public static void SolveTask1() 
        { 
            using(StreamReader reader = new StreamReader(@"Day9/input1.txt"))
            {
                Solve(reader, 2);
            }
        }

        private static void Solve(StreamReader reader, int numberOfKnots)
        {
            var line = reader.ReadLine();

            (int x, int y)[] knotPosition = new (int x, int y)[numberOfKnots];
            
            for(int i = 0; i < numberOfKnots; i++)
            {
                knotPosition[i] = (0, 0);
            }

            var tailPositionSet = new HashSet<(int, int)>();
            tailPositionSet.Add(knotPosition[numberOfKnots - 1]);

            while (line != null)
            {
                ((int x, int y), int numberOfMoves) = GetDirectionAndNumberOfMoves(line);

                for (int i = 0; i < numberOfMoves; i++)
                {
                    knotPosition[0] = (knotPosition[0].x + x, knotPosition[0].y + y);

                    for (int j = 1; j < numberOfKnots; j++)
                    {


                        var positionDiffrenceX = knotPosition[j - 1].x - knotPosition[j].x;
                        var positionDiffrenceY = knotPosition[j - 1].y - knotPosition[j].y;

                        if (Math.Abs(positionDiffrenceX) > 1 ||
                            Math.Abs(positionDiffrenceY) > 1)
                        {
                            knotPosition[j] = (knotPosition[j].x + Math.Sign(positionDiffrenceX), knotPosition[j].y + Math.Sign(positionDiffrenceY));

                            if (j >= numberOfKnots - 1)
                            {
                                tailPositionSet.Add(knotPosition[numberOfKnots - 1]);
                            }
                        }
                    }
                }

                line = reader.ReadLine();
            }

            Console.WriteLine(tailPositionSet.Count);
        }

        private static ((int, int), int) GetDirectionAndNumberOfMoves(string text)
        {
            var characters = text.Split(' ');

            var direction = characters[0] switch
            {
                "R" => (1, 0),
                "L" => (-1, 0),
                "D" => (0, -1),
                "U" => (0, 1)
            };

            int numberOfMoves;

            int.TryParse(characters[1], out numberOfMoves);

            return (direction, numberOfMoves);
        }

        public static void SolveTask2() 
        {
            using (StreamReader reader = new StreamReader(@"Day9/input1.txt"))
            {
                Solve(reader, 10);
            }
        }
    }
}
