using System;
using System.Collections.Generic;
using System.Linq;

namespace TickCalcTest
{
    public abstract class CalculatorBase
    {
        protected readonly List<Range> TickSizeRanges;
        protected Range GetTickSizeRange(decimal prz)
        {
            foreach (var ts in TickSizeRanges)
                if (prz <= ts.EndPrz) return ts;
            return TickSizeRanges.Last();
        }

        protected decimal GetTickSize(decimal prz) => GetTickSizeRange(prz).TickSize;

        public Question[] Questions;

        public CalculatorBase()
        {
            this.TickSizeRanges = TestData.TickSizes;
            this.Questions = TestData.Questions.ToArray();
        }

        public abstract int CalcTicks(decimal lowPrz, decimal highPrz);

        
        public void RunTest()
        {
            foreach (var q in Questions)
            {
                q.DiffTicks = CalcTicks(q.LowPrz, q.HighPrz);
            }
        }

        public void OutputResult()
        {
            foreach(var q in Questions)
            {
                Console.WriteLine($"{q.LowPrz, 8:0.00} ~ {q.HighPrz, 8:0.00} => {q.DiffTicks, 8:n0} Ticks");
            }
        }
    }
}
