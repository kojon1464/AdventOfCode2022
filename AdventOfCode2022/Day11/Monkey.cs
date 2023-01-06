using System.Numerics;

namespace AdventOfCode2022.Day11
{
    internal class Monkey
    {
        public int InspectCount { get; set; }
        public LinkedList<BigInteger> Items { get; set; }
        public int DivisibleByTestNumber { get; set; }
        public int MonkeyIndexIfTestTrue { get; set; }
        public int MonkeyIndexIfTestFalse { get; set; }
        public Func<BigInteger, BigInteger> WorryIndexChangeFunction { get; set; }

        public void InspectItems(List<Monkey> monkeies, bool divideWorryLevel = true, int? moduloOf = null)
        {
            while(Items.Count > 0)
            {
                BigInteger itemWorryIndexChnaged = WorryIndexChangeFunction(Items.First());

                if(divideWorryLevel)
                {
                    itemWorryIndexChnaged = itemWorryIndexChnaged / 3;
                }

                if(moduloOf != null)
                {
                    itemWorryIndexChnaged = itemWorryIndexChnaged % (int)moduloOf;
                }

                InspectCount++;
                Items.RemoveFirst();

                if(itemWorryIndexChnaged % DivisibleByTestNumber == 0)
                {
                    monkeies[MonkeyIndexIfTestTrue].Items.AddLast(itemWorryIndexChnaged);
                }
                else
                {
                    monkeies[MonkeyIndexIfTestFalse].Items.AddLast(itemWorryIndexChnaged);
                }
            }

        }
    }
}
