using System.Text;

namespace AdventOfCode2022.Day12
{
    internal class Day12Solver
    {
        public static void SolveTask1()
        {
            Solve(true);
        }

        private static void Solve(bool countZeroLevel)
        {
            var map = CreateMap(@"Day12\input1.txt");

            var unseenQueue = new PriorityQueue<(int, int), int>();
            int[][] costMap = new int[map.HeightMap.Length][];
            bool[][] visitedMap = new bool[map.HeightMap.Length][];

            for (int i = 0; i < map.HeightMap.Length; i++)
            {
                costMap[i] = new int[map.HeightMap[i].Length];
                visitedMap[i] = new bool[map.HeightMap[i].Length];

                for (int j = 0; j < costMap[i].Length; j++)
                {
                    costMap[i][j] = int.MaxValue;
                }
            }

            costMap[map.Start.y][map.Start.x] = 0;
            unseenQueue.Enqueue(map.Start, 0);

            (int x, int y)[] moveDIrections = { (0, 1), (0, -1), (1, 0), (-1, 0) };

            while (unseenQueue.Count > 0)
            {
                (int x, int y) current = unseenQueue.Dequeue();

                if (visitedMap[current.y][current.x] || costMap[current.y][current.x] >= costMap[map.Finish.y][map.Finish.x])
                {
                    continue;
                }

                visitedMap[current.y][current.x] = true;

                foreach (var direction in moveDIrections)
                {
                    (int x, int y) neighbour = (current.x + direction.x, current.y + direction.y);

                    if (neighbour.x < 0 || neighbour.x > costMap[0].Length - 1 ||
                        neighbour.y < 0 || neighbour.y > costMap.Length - 1)
                    {
                        continue;
                    }

                    int newCost = 0;

                    int heightDifference = map.HeightMap[neighbour.y][neighbour.x] - map.HeightMap[current.y][current.x];

                    if (!countZeroLevel && heightDifference == 0 && map.HeightMap[current.y][current.x] == 0) 
                    {
                        newCost = 0;
                    }
                    else if (heightDifference <= 1)
                    {
                        newCost = costMap[current.y][current.x] + 1;
                    }
                    else
                    {
                        newCost = int.MaxValue;
                    }

                    if (newCost < costMap[neighbour.y][neighbour.x])
                    {
                        costMap[neighbour.y][neighbour.x] = newCost;
                    }

                    unseenQueue.Enqueue(neighbour, costMap[neighbour.y][neighbour.x]);
                }
            }

            Console.WriteLine(costMap[map.Finish.y][map.Finish.x]);
        }

        private static Map CreateMap(string filePath)
        {
            using(StreamReader reader = new StreamReader(filePath))
            {
                var lines = reader.ReadToEnd().Split(Environment.NewLine);

                int[][] map = new int[lines.Length][];
                (int, int) start = (0, 0);
                (int, int) end = (0, 0);

                for(int i = 0; i < lines.Length; i++)
                {
                    LinkedList<int> row = new LinkedList<int>();

                    for(int j = 0; j < lines[i].Length; j++)
                    {
                        int height;

                        if (char.IsLower(lines[i][j]))
                        {
                            height = lines[i][j] - 'a';
                        }
                        else
                        {
                            if(lines[i][j] == 'S')
                            {
                                height = 0;
                                start = (j, i);
                            }
                            else if(lines[i][j] == 'E')
                            {
                                height = 'z' - 'a';
                                end = (j, i);
                            }
                            else
                            {
                                height = int.MaxValue;
                            }
                        }

                        row.AddLast(height);
                    }

                    map[i] = row.ToArray();
                }

                var mapObject = new Map();

                mapObject.Start = start;
                mapObject.Finish = end;
                mapObject.HeightMap = map;

                return mapObject;
            }
        }

        public static void SolveTask2()
        {
            Solve(false);
        }
    }
}
