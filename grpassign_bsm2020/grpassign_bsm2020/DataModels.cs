using System;
using System.Collections.Generic;
using System.Text;

namespace grpassign_bsm2020
{
    public class DataModels
    {
        public static List<Partner> GetPartners()
        {
            List<Partner> Partners = new List<Partner>();
            //Given attributes
            Partners.Add(new Partner { Name = "p1", A1 = 2.35, A2 = 0.20, A3 = 16.78, A4 = 16.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p2", A1 = 0.55, A2 = 0.38, A3 = 11.34, A4 = 24.55, A5 = 1, A6 = 3 });
            Partners.Add(new Partner { Name = "p3", A1 = 2.48, A2 = 0.12, A3 = 24.32, A4 = 16.64, A5 = 2, A6 = 1 });
            Partners.Add(new Partner { Name = "p4", A1 = 2.62, A2 = 0.27, A3 = 20.55, A4 = 22.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p5", A1 = 3.36, A2 = 0.66, A3 = 12.67, A4 = 13.65, A5 = 2, A6 = 1 });
            foreach (Partner p in Partners)
                p.GetValueFunctions();
            return Partners;
        }

        public static List<Partner> GetCombinations()
        {
            List<Partner> Partners = GetPartners();
            List<Partner> Combinations = new List<Partner>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    Partner p = new Partner();
                    Partner C1 = Partners[i];
                    Partner C2 = Partners[j];
                    p.Name = $"{C1.Name}+{C2.Name}";
                    //Console.WriteLine($"Creating Combination {p.Name}");

                    //A1 - Supply quantity
                    //The combination will have the combined supply quantity
                    p.A1 = C1.A1 + C2.A1;

                    //A2 - Variance
                    //The combination will have summed variabce
                    p.A2 = C1.A2 + C2.A2;

                    //A3 - Delay Time
                    //Combination will have the worst delay time
                    p.A3 = Math.Max(C1.A3, C2.A3);

                    //A4 - Supply ratio of low demand routes
                    //Combination value is counnted using averaging by A1
                    p.A4 = (C1.A4 / 100 * C1.A1) + (C2.A4 / 100 * C2.A1);
                    p.A4 /= p.A1;
                    p.A4 *= 100;

                    //A5 - Payment reputation
                    //Combination will assume worst case
                    p.A5 = Math.Min(C1.A5, C2.A5);

                    //A6 - Switching risk
                    //Combination will assume worst case
                    p.A6 = Math.Max(C1.A6, C2.A6);

                    Combinations.Add(p);

                }
            }

            //Calculate value functions
            foreach (var p in Combinations)
            {
                p.IsCombination = true;
                p.GetValueFunctions();
            }
            return Combinations;
        }
    }
}
