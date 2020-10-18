using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace grpassign_bsm2020
{
    public class ExhaustiveSwingMethod
    {
        public static void Combination_ExhaustiveSwing_AllWeights(List<Partner> Combinations, List<WeightCombination> Weights)
        {
            Console.WriteLine("\nExhaustive Swing Method for Combinations");
            Console.WriteLine($"Total Combinations: {Combinations.Count}\n");
            Dictionary<string, long> Votes = new Dictionary<string, long>();            
            foreach (Partner p in Combinations)
                Votes.Add(p.Name, 0);
            foreach (WeightCombination weight in Weights)
            {
                double maxVal = double.MinValue;
                foreach (Partner p in Combinations)
                {
                    p.GetAdditiveValueFunction(weight);
                    maxVal = Math.Max(maxVal, p.AdditiveValueFunction);
                }
                foreach (Partner p in Combinations)
                {
                    if (p.AdditiveValueFunction == maxVal)
                        Votes[p.Name]++;
                }
            }
            //printing results
            Console.WriteLine("Results:");            
            foreach(KeyValuePair<string,long> kvp in Votes.OrderByDescending(v=>v.Value))
            {
                Console.WriteLine($"{kvp.Key},{kvp.Value*100.0/ Weights.Count:#0.00}");
            }
        }


        public static void Combination_ExhaustiveSwing_AllWeights2(List<Partner> Combinations, List<WeightCombination> Weights)
        {
            Console.WriteLine("\nExhaustive Swing Method for Combinations");
            Console.WriteLine($"Total Combinations: {Combinations.Count}\n");
            Dictionary<string, long> Votes = new Dictionary<string, long>();
            foreach (Partner p in Combinations)
                Votes.Add(p.Name, 0);
            foreach (WeightCombination weight in Weights)
            {
                double maxVal = double.MinValue;
                foreach (Partner p in Combinations)
                {
                    p.GetAdditiveValueFunction(weight);
                    maxVal = Math.Max(maxVal, p.AdditiveValueFunction);
                }
                foreach (Partner p in Combinations)
                {
                    if (p.AdditiveValueFunction == maxVal)
                        Votes[p.Name]++;
                }
            }
            //printing results
            Console.WriteLine("Results");
            Console.WriteLine("..............\n");
            foreach (KeyValuePair<string, long> kvp in Votes.OrderByDescending(v => v.Value))
            {
                Console.WriteLine($"{kvp.Key}\t{kvp.Value * 100.0 / Weights.Count:#0.00}%");
            }
        }






    }
}
