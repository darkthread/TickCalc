using System;
using System.Linq;

namespace TickCalcTest
{
    public class FormulaCalculator : CalculatorBase
    {
        public override int CalcTicks(decimal lowPrz, decimal highPrz)
        {
            var loRange = GetTickSizeRange(lowPrz);
            var hiRange = GetTickSizeRange(highPrz);
            if (loRange.Index == hiRange.Index)
                return Convert.ToInt32((highPrz - lowPrz) / loRange.TickSize);
            return Convert.ToInt32((loRange.EndPrz - lowPrz) / loRange.TickSize) +
                   TickSizeRanges.Skip(loRange.Index + 1).TakeWhile(o => o.Index <= hiRange.Index - 1).Sum(o => o.Qty) +
                   Convert.ToInt32((highPrz - hiRange.StartPrz) / hiRange.TickSize) + 1;
        }
    }
}