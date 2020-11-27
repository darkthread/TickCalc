using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

namespace TickCalcTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VerifyResult();
            var summary = BenchmarkRunner.Run<TestRunner>();
        }

        private static void VerifyResult()
        {
            DumbCalculator dc = new DumbCalculator();
            dc.RunTest();
            FormulaCalculator fc = new FormulaCalculator();
            fc.RunTest();
            LookUpCalculator lc = new LookUpCalculator();
            lc.RunTest();
            bool pass = true;
            for (var i = 0; i < dc.Questions.Length; i++)
            {
                if (dc.Questions[i].DiffTicks != fc.Questions[i].DiffTicks ||
                    dc.Questions[i].DiffTicks != lc.Questions[i].DiffTicks)
                {
                    var q = dc.Questions[i];
                    Console.WriteLine(
                        $"{q.LowPrz,8:0.00} ~ {q.HighPrz,8:0.00} => {q.DiffTicks,8:n0} / {fc.Questions[i].DiffTicks,8:n0} / {lc.Questions[i].DiffTicks,8:n0}");
                    pass = false;
                }
            }
            Console.WriteLine($"Verification Reustl = {(pass ? "PASS" : "FAIL")}");
            if (!pass) throw new ApplicationException("Verification Failed");
        }
    }

    public class TestRunner
    {
        [Benchmark]
        public void RunDumbCalc()
        {
            var c = new DumbCalculator();
            c.RunTest();
        }

        [Benchmark]
        public void RunFormulaCalc()
        {
            var c = new FormulaCalculator();
            c.RunTest();
        }

        [Benchmark]
        public void RunLookupCalc()
        {
            var c = new LookUpCalculator();
            c.RunTest();
        }
    }
}
