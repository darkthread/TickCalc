namespace TickCalcTest
{
    public class DumbCalculator : CalculatorBase
    {
        public override int CalcTicks(decimal lowPrz, decimal highPrz)
        {
            var prz = lowPrz;
            int count = 0;
            while (prz < highPrz)
            {
                count++;
                prz += GetTickSize(prz);
            }
            return count;
        }
    }
}