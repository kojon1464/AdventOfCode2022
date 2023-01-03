using System.Globalization;

namespace AdventOfCode2022.Day10
{
    internal class Day10Solver
    {
        public static void Solve()
        {
            using(StreamReader reader = new StreamReader(@"Day10/input1.txt"))
            {
                var line = reader.ReadLine();

                var cycle = 0;
                var registry = 1;
                var signalSum = 0;
                var image = "";

                while(line != null)
                {
                    var instruction = GetCycelsAndRegisteryChnage(line);

                    for(var i = 0; i < instruction.cycles; i++)
                    {
                        if (cycle % 40 == 0)
                        {
                            image += Environment.NewLine;
                        }

                        image += GetPixelToDraw(cycle, registry);

                        cycle++;

                        if ((cycle + 20) % 40 == 0)
                        {
                            signalSum += cycle * registry;
                        }



                        if (i >= instruction.cycles - 1)
                        {
                            registry += instruction.registryChnage;
                        }
                    }

                    line= reader.ReadLine();
                }

                Console.WriteLine(signalSum);
                Console.WriteLine(image);
            }
        }

        private static (int cycles, int registryChnage) GetCycelsAndRegisteryChnage(string text)
        {
            var words = text.Split(' ');

            int cycles = words[0] switch
            {
                "noop" => 1,
                "addx" => 2
            };

            int registryChnage = 0;

            if(words.Length > 1)
            {
                int.TryParse(words[1], out registryChnage);
            }

            return (cycles, registryChnage);
        }

        private static char GetPixelToDraw(int cycle, int registry)
        {
            var positionToDraw = cycle % 40;

            if (positionToDraw <= registry + 1 && positionToDraw >= registry - 1)
            {
                return '#';
            }
            else
            {
                return '.';
            }
        }
    }
}
