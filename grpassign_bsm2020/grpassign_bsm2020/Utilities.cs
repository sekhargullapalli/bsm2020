using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace grpassign_bsm2020
{
    public static class Utilities
    {
        public static void CreateExhaustiveCombinations(int startindex = 0, int step = 2,
            bool preventduplicates = true,
            bool zeroallowed = false,
            bool consolelog=false,
            bool saveresults = false,
            string outputpath = @"/serailized/allweights.csv")
        {
            List<WeightCombination> AllWeights = new List<WeightCombination>();            

            for (int i = startindex; i < 100; i += step)
            {
                for (int j = startindex; j < 100; j += step)
                {
                    if (i + j >= 100) break;
                    if (preventduplicates)
                        if (i == j) continue;
                    for (int k = startindex; k < 100; k += step)
                    {
                        if (i + j + k >= 100) break;
                        if (preventduplicates)
                            if (i == k || j == k) continue;
                        for (int l = startindex; l < 100; l += step)
                        {
                            if (i + j + k + l >= 100) break;
                            if (preventduplicates)
                                if (i == l || j == l || k == 1) continue;
                            for (int m = startindex; m < 100; m += step)
                            {
                                if (i + j + k + l + m >= 100) break;
                                if (preventduplicates)
                                    if (i == m || j == m || k == m || l == m) continue;
                                for (int n = startindex; n < 100; n += step)
                                {
                                    if (preventduplicates)
                                        if (i == n || j == n || k == n || l == n || m == n) continue;
                                    int sum = i + j + k + l + m + n;
                                    if (!zeroallowed)
                                    {
                                        if (i == 0 || j == 0 || k == 0 || l == 0 || m == 0 || n == 0) continue;
                                    }
                                    if (sum == 100)
                                    {
                                        WeightCombination c = new WeightCombination
                                        {
                                            A1 = i / 100.0,
                                            A2 = j / 100.0,
                                            A3 = k / 100.0,
                                            A4 = l / 100.0,
                                            A5 = m / 100.0,
                                            A6 = n / 100.0
                                        };
                                        AllWeights.Add(c);
                                        if (consolelog)
                                            Console.WriteLine(c);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"totalcombinations: {AllWeights.Count}");
            if (saveresults)
            {
                using (var writer = new StreamWriter(outputpath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(AllWeights);
                }
            }
            Console.WriteLine("Done!");

        }


        public static void ConsoleWriteGreen(string text)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(text);
            Console.ForegroundColor = clr;
        }
        public static void ConsoleWriteRed(string text)
        {
            var clr = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(text);
            Console.ForegroundColor = clr;
        }

    }
}
