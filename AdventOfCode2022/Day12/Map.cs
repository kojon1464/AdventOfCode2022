
namespace AdventOfCode2022.Day12
{
    internal class Map
    {
        public (int x, int y) Start { get; set; }
        public (int x, int y) Finish { get; set; }

        public int[][] HeightMap { get; set; }
    }
}
