using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TickCalcTest
{
    /// <summary>
    /// 級距資料
    /// </summary>
    public class Range
    {
        public int Index { get; private set; }
        public decimal StartPrz { get; private set; }
        public decimal EndPrz { get; private set; }
        public decimal TickSize { get; private set; }
        public int Qty { get; private set; }
        public Range(int idx, decimal startPrz, decimal endPrz, decimal tickSize)
        {
            Index = idx;
            StartPrz = startPrz;
            EndPrz = endPrz;
            TickSize = tickSize;
            Qty = Convert.ToInt32((endPrz - startPrz) / tickSize + 1);
        }
    }

    /// <summary>
    /// 計算題
    /// </summary>
    public class Question
    {
        public decimal LowPrz { get; private set; }
        public decimal HighPrz { get; private set; }
        public int DiffTicks { get; set; }

        public Question(decimal lowPrz, decimal highPrz)
        {
            LowPrz = lowPrz;
            HighPrz = highPrz;
        }

    }
    public class TestData
    {
        public static List<Range> TickSizes = new List<Range>()
        {
            new Range(0, 0, 1.99M, 0.01M),
            new Range(1, 2, 4.98M, 0.02M),
            new Range(2, 5, 9.95M, 0.05M),
            new Range(3, 10, 24.9M, 0.10M),
            new Range(4, 25, 99.75M, 0.25M),
            new Range(5, 100, 199.5M, 0.5M),
            new Range(6, 200, 399, 1),
            new Range(7, 400, 5000, 2)
        };

        const int Seed = 8825252;

        public static IEnumerable<Question> Questions
        {
            get
            {
                var rnd = new Random(Seed); //相同亂數種子產生相同題庫
                return Enumerable.Range(0, 1000).Select(o =>
                {
                    var rIdx = rnd.Next(TickSizes.Count);
                    var range = TickSizes[rIdx];
                    var prz1 = range.StartPrz + rnd.Next(range.Qty) * range.TickSize;
                    rIdx = rnd.Next(TickSizes.Count);
                    range = TickSizes[rIdx];
                    var prz2 = range.StartPrz + rnd.Next(range.Qty) * range.TickSize;
                    return new Question(Math.Min(prz1, prz2), Math.Max(prz1, prz2));
                });
            }
        }
    }
}
