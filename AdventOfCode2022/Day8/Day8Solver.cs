namespace AdventOfCode2022.Day8
{
    internal class Day8Solver
    {
        public static void SolveTask1()
        {
            var map = GetTreeMap();

            var left = GenerateTreeMaxHeightMap(map, 0, -1);
            var right = GenerateTreeMaxHeightMap(map, 0, 1);
            var down = GenerateTreeMaxHeightMap(map, 1, 0);
            var up = GenerateTreeMaxHeightMap(map, -1, 0);

            var listOfBlockingMaps = new List<int[][]> { left, right, up, down };

            int sum = 0;

            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {

                    foreach(var blockingMap in listOfBlockingMaps)
                    {
                        if (blockingMap[i][j] < map[i][j])
                        {
                            sum++;
                            break;
                        }
                    }
                }
            }

            Console.WriteLine(sum);
        }

        private static int[][] GenerateTreeMaxHeightMap(int[][] treeHeight,int yDirection, int xDirection)
        {
            var map = new int[treeHeight.Length][];

            for(int i = 0; i < treeHeight.Length; i++)
            {
                map[i] = new int[treeHeight[i].Length];
            }

            int yStartIndex = yDirection > 0 ? map.Length - 1 : 0;
            int xStartIndex = xDirection > 0 ? map[0].Length - 1 : 0;
            int yIncrement = yDirection > 0 ? -1 : 1;
            int xIncrement = xDirection > 0 ? -1 : 1;
            Func<int, bool> yCondition = yDirection > 0 ? (y) => y >= 0 : (y) => y < map.Length;
            Func<int, bool> xCondition = xDirection > 0 ? (x) => x >= 0 : (x) => x < map[0].Length;

            for (int i = yStartIndex; yCondition(i); i += yIncrement)
            {
                for(int j = xStartIndex; xCondition(j); j += xIncrement)
                {
                    int neighbour = -1;
                    int heightBlockingNeighbour = -1;

                    if (i + yDirection >= 0 && i + yDirection < map.Length &&
                        j + xDirection >= 0 && j + xDirection < map[i].Length)
                    {
                        neighbour = treeHeight[i + yDirection][j + xDirection];
                        heightBlockingNeighbour = map[i + yDirection][j + xDirection];
                    }

                    map[i][j] = Math.Max(neighbour, heightBlockingNeighbour);
                }
            }

            return map;
        }

        private static int[][] GetTreeMap()
        {
            List<int[]> map = new List<int[]>();

            using (StreamReader reader = new StreamReader(@"Day8/input1.txt"))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    var row = line.Select(x => (int)char.GetNumericValue(x)).ToArray();
                    map.Add(row);

                    line= reader.ReadLine();
                }
            }

            return map.ToArray();
        }

        public static void SolveTask2()
        {
            var map = GetTreeMap();
            int maxScore = 0;

            for (int i = 1; i < map.Length - 1; i++)
            {
                for (int j = 1; j < map[i].Length - 1; j++)
                {
                    int currentScore = 1;

                    currentScore *= GetViewingDistance(map, j, i, 0, 1);
                    currentScore *= GetViewingDistance(map, j, i, 0, -1);
                    currentScore *= GetViewingDistance(map, j, i, 1, 0);
                    currentScore *= GetViewingDistance(map, j, i, -1, 0);

                    if (maxScore < currentScore)
                    {
                        maxScore = currentScore;
                    }
                }
            }

            Console.WriteLine(maxScore);
        }

        private static int GetViewingDistance(int[][] treeHeight, int x, int y, int xDirection, int yDirection)
        {
            int distance = 1;
            int offsetY = yDirection;
            int offsetX = xDirection;

            while (y + offsetY > 0 && y + offsetY < treeHeight.Length - 1 &&
                   x + offsetX > 0 && x + offsetX < treeHeight[y].Length - 1 &&
                   treeHeight[y + offsetY][x + offsetX] < treeHeight[y][x])
            {
                distance++;
                offsetY += yDirection;
                offsetX += xDirection;
            }

            return distance;
        }
    }
}
