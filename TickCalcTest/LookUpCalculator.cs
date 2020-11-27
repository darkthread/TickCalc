using System;
using System.Collections.Generic;
using System.Linq;

namespace TickCalcTest
{
    public class LookUpCalculator : CalculatorBase
    {
        static decimal TopPrz = 0M;
        static decimal TopTickSz = 0;
        static int TopPrzTicks = 0;
        static Dictionary<decimal, int> AllTicks = null;
        static object syncObj = new object();
        public LookUpCalculator() : base()
        {
            lock (syncObj)
            {
                if (AllTicks == null)
                {
                    var prz = 0M;
                    TopPrz = TickSizeRanges.Last().StartPrz;
                    TopTickSz = TickSizeRanges.Last().TickSize;
                    AllTicks = new Dictionary<decimal, int>();
                    var count = 1;
                    while (prz < TopPrz)
                    {
                        AllTicks.Add(prz, count++);
                        prz += GetTickSize(prz);
                    }
                    TopPrzTicks = count;
                }
            }
        }

        int GetAbsTicks(decimal prz)
        {
            if (prz >= TopPrz) 
                return TopPrzTicks + Convert.ToInt32((prz - TopPrz) / TopTickSz);
            return AllTicks[prz];
        }

        public override int CalcTicks(decimal lowPrz, decimal highPrz)
        {
            return GetAbsTicks(highPrz) - GetAbsTicks(lowPrz);
        }
    }
}
