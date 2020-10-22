using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using static grpassign_bsm2020.Utilities;

namespace grpassign_bsm2020
{
    class Program
    {
        enum SolverMethod 
        {
            None,

            Combination_Dominance_AttributeValues,
            Combination_Dominance_ValueFunctions,
            Combination_ExhaustiveSwing_AllWeights,
            Combination_ExhaustiveSwing_DistinctWeights,

            Single_Dominance_AttributeValues,
            Single_Dominance_ValueFunctions,
            Single_ExhaustiveSwing_AllWeights,
            Single_ExhaustiveSwing_DistinctWeights
        }

        static SolverMethod Solver = SolverMethod.Combination_Dominance_ValueFunctions;

        static void Main(string[] args)
        {

            ////List single values
            //foreach (Partner p in DataModels.GetPartners())
            //    Console.WriteLine(p);
            //return;

            //List combination values
            //foreach (Partner p in DataModels.GetCombinations())
            //    Console.WriteLine(p);
            //return;

            //Combination_Dominance_AttributeValues
            if (Solver == SolverMethod.Combination_Dominance_AttributeValues)
            {
                DominanceMethod.Combination_DominanceMethod();
            }

            //Combination_Dominance_ValueFunctions
            if (Solver == SolverMethod.Combination_Dominance_ValueFunctions)
            {
                DominanceMethod.Combination_DominanceValueMethod();
            }

            //Combination_ExhaustiveSwing_AllWeights
            if (Solver == SolverMethod.Combination_ExhaustiveSwing_AllWeights)
            {
                List<WeightCombination> Weights = new List<WeightCombination>();
                string filepath = @"../../../serialized/allweights.csv";
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Weights = csv.GetRecords<WeightCombination>().ToList();
                }
                List<Partner> Combinations = DataModels.GetCombinations();
                ExhaustiveSwingMethod.Combination_ExhaustiveSwing_AllWeights(Combinations, Weights);
            }

            //Combination_ExhaustiveSwing_DistinctWeights
            if (Solver == SolverMethod.Combination_ExhaustiveSwing_DistinctWeights)
            {
                List<WeightCombination> Weights = new List<WeightCombination>();
                string filepath = @"../../../serialized/distinctweights.csv";
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Weights = csv.GetRecords<WeightCombination>().ToList();
                }

                //Sorting weights
                Dictionary<string, List<WeightCombination>> SortedWeights = new Dictionary<string, List<WeightCombination>>();
                for(int i=1;i<=6;i++)
                    SortedWeights.Add($"W{i}", new List<WeightCombination>());
                foreach (WeightCombination weight in Weights)
                    SortedWeights[weight.DominanceWeight].Add(weight);

                List<Partner> Combinations = DataModels.GetCombinations();

                //Conducting Exhaustive Swing For All Sets
                foreach(KeyValuePair<string, List<WeightCombination>> kvp in SortedWeights)
                {
                    Console.WriteLine($"\nDominant Weight {kvp.Key}");
                    Console.WriteLine("========================");
                    ExhaustiveSwingMethod.Combination_ExhaustiveSwing_AllWeights(Combinations, kvp.Value);
                }            
            }


            //Single_Dominance_AttributeValues
            if (Solver == SolverMethod.Single_Dominance_AttributeValues)
            {
                DominanceMethod.Single_DominanceMethod();
            }

            //Single_Dominance_ValueFunctions
            if (Solver == SolverMethod.Single_Dominance_ValueFunctions)
            {
                DominanceMethod.Single_DominanceValueMethod();
            }

            //Single_ExhaustiveSwing_AllWeights
            if (Solver == SolverMethod.Single_ExhaustiveSwing_AllWeights)
            {
                List<WeightCombination> Weights = new List<WeightCombination>();
                string filepath = @"../../../serialized/allweights.csv";
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Weights = csv.GetRecords<WeightCombination>().ToList();
                }
                List<Partner> Partners = DataModels.GetPartners();
                ExhaustiveSwingMethod.Combination_ExhaustiveSwing_AllWeights(Partners, Weights);
            }

            if (Solver == SolverMethod.Single_ExhaustiveSwing_DistinctWeights)
            {
                List<WeightCombination> Weights = new List<WeightCombination>();
                string filepath = @"../../../serialized/distinctweights.csv";
                using (var reader = new StreamReader(filepath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    Weights = csv.GetRecords<WeightCombination>().ToList();
                }

                //Sorting weights
                Dictionary<string, List<WeightCombination>> SortedWeights = new Dictionary<string, List<WeightCombination>>();
                for (int i = 1; i <= 6; i++)
                    SortedWeights.Add($"W{i}", new List<WeightCombination>());
                foreach (WeightCombination weight in Weights)
                    SortedWeights[weight.DominanceWeight].Add(weight);

                List<Partner> Partners = DataModels.GetPartners();

                //Conducting Exhaustive Swing For All Sets
                foreach (KeyValuePair<string, List<WeightCombination>> kvp in SortedWeights)
                {
                    Console.WriteLine($"\nDominant Weight {kvp.Key}");
                    Console.WriteLine("========================");
                    ExhaustiveSwingMethod.Combination_ExhaustiveSwing_AllWeights(Partners, kvp.Value);
                }
            }






            //----------------Serializing All Weights with Duplicates and resoultion of 0.02-------------

            //Utilities.CreateExhaustiveCombinations(startindex: 0,
            //    step: 2,
            //    preventduplicates: false,
            //    zeroallowed: false, 
            //    consolelog:false,
            //    saveresults: true,
            //    outputpath: @"../../../serialized/allweights.csv");

            //Utilities.CreateExhaustiveCombinations(startindex: 0,
            //   step: 2,
            //   preventduplicates: true,
            //   zeroallowed: false,
            //   consolelog: false,
            //   saveresults: true,
            //   outputpath: @"../../../serialized/distinctweights.csv");

            Console.WriteLine("Done");

        }







        static void CombinationDominance2()
        {

           

            List<Partner> Partners = new List<Partner>();
            
            //Given attributes
            Partners.Add(new Partner { Name = "p1", A1 = 2.35, A2 = 0.20, A3 = 16.78, A4 = 16.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p2", A1 = 0.55, A2 = 0.38, A3 = 11.34, A4 = 24.55, A5 = 1, A6 = 3 });
            Partners.Add(new Partner { Name = "p3", A1 = 2.48, A2 = 0.12, A3 = 24.32, A4 = 16.64, A5 = 2, A6 = 1 });
            Partners.Add(new Partner { Name = "p4", A1 = 2.62, A2 = 0.27, A3 = 20.55, A4 = 22.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p5", A1 = 3.36, A2 = 0.66, A3 = 12.67, A4 = 13.65, A5 = 2, A6 = 1 });

            //Creating combinatioins
            List<Partner> Combinations = new List<Partner>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = i+1; j < 5; j++)
                {
                    Partner p = new Partner();
                    Partner C1 = Partners[i];
                    Partner C2 = Partners[j];
                    p.Name = $"{C1.Name}+{C2.Name}";
                    Console.WriteLine($"Creating Combination {p.Name}");

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

                    //A5 - Payment reputation
                    //Combination will assume worst case
                    p.A5 = Math.Min(C1.A5, C2.A5);

                    //A6 - Switching risk
                    //Combination will assume worst case
                    p.A6 = Math.Max(C1.A6, C2.A6);

                    Combinations.Add(p);
                    
                }
            }

            Console.WriteLine("\nAttribute values of combinations");
            foreach(var p in Combinations)
                Console.WriteLine(p.ToString());

            Console.WriteLine("\nChecking for domination");
            List<string> DominatedModels = new List<string>();

            foreach (Partner p1 in Combinations)
            {
                Console.WriteLine($"Checking {p1.Name}");
                foreach (Partner p2 in Combinations)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"{p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if  (
                        (p1.A1 > 8 || p1.A1 < 2) ||
                        ((p2.A1 >= 4 && p2.A1 <= 6) && (p1.A1 < 4 || p1.A1 > 6))                        
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A1, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A1, ");
                    }


                    //Check for A2 dominance
                    if (
                         (p1.A2 > 1.2) ||
                         (p2.A2 <= 0.6 && p1.A2 > 0.6)                          
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A2, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A2, ");
                    }

                    //Check for A3 dominance
                    if (
                        (p1.A3 > 25) ||
                        (p2.A3 <= 15 && p1.A3 > 15) ||
                        (p2.A3 < p1.A3)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A3, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A3, ");
                    }

                    //Check for A4 dominance                    
                    if (
                        (p2.A4 >= 20 && p1.A4 < 20) ||
                        (p2.A4 > p1.A4)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A4, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A4, ");
                    }

                    //Check for A5 dominance                    
                    if (p2.A5 > p1.A5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A5, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A5, ");
                    }

                    //Check for A6 dominance                    
                    if (p2.A6 < p1.A6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A6, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A6, ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");






        }

        static void MainTemp(string[] args)
        {
            List<Partner> Partners = new List<Partner>();

            //L   1
            //M   2
            //H   3

            //DL,VL,L 1
            //VL,L,M  2
            //L,M,H   3
            //M,H,VH  4
            //H,VH,DH 5

            Partners.Add(new Partner { Name = "p1", A1 = 2.35, A2 = 0.20, A3 = 16.78, A4 = 16.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p2", A1 = 0.55, A2 = 0.38, A3 = 11.34, A4 = 24.55, A5 = 1, A6 = 3 });
            Partners.Add(new Partner { Name = "p3", A1 = 2.48, A2 = 0.12, A3 = 24.32, A4 = 16.64, A5 = 2, A6 = 1 });
            Partners.Add(new Partner { Name = "p4", A1 = 2.62, A2 = 0.27, A3 = 20.55, A4 = 22.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p5", A1 = 3.36, A2 = 0.66, A3 = 12.67, A4 = 13.65, A5 = 2, A6 = 1 });

            List<string> DominatedModels = new List<string>();

            foreach (Partner p1 in Partners)
            {
                Console.WriteLine($"Checking {p1.Name}");
                foreach (Partner p2 in Partners)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"{p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (
                        (p1.A1 > 4 || p1.A1 < 1) ||
                        ((p2.A1 >= 2 && p2.A1 <= 3) && (p1.A1 < 2 || p1.A1 > 3))                         
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A1, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A1, ");
                    }


                    //Check for A2 dominance
                    if (
                         (p1.A2 > 0.6) ||
                         (p2.A2 <= 0.3 && p1.A2 > 0.3)                          
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A2, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A2, ");
                    }

                    //Check for A3 dominance
                    if (
                        (p1.A3 > 25) ||
                        (p2.A3 <= 15 && p1.A3 > 15)                         
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A3, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A3, ");
                    }

                    //Check for A4 dominance                    
                    if (                        
                        (p2.A4 > p1.A4)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A4, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A4, ");
                    }

                    //Check for A5 dominance                    
                    if (p2.A5 > p1.A5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A5, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A5, ");
                    }

                    //Check for A6 dominance                    
                    if (p2.A6 < p1.A6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A6, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A6, ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");


        }

        static void MainSingle(string[] args)
        {
            List<Partner> Partners = new List<Partner>();

            //L   1
            //M   2
            //H   3

            //DL,VL,L 1
            //VL,L,M  2
            //L,M,H   3
            //M,H,VH  4
            //H,VH,DH 5

            Partners.Add(new Partner { Name = "p1", A1 = 2.35, A2 = 0.20, A3 = 16.78, A4 = 16.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p2", A1 = 0.55, A2 = 0.38, A3 = 11.34, A4 = 24.55, A5 = 1, A6 = 3 });
            Partners.Add(new Partner { Name = "p3", A1 = 2.48, A2 = 0.12, A3 = 24.32, A4 = 16.64, A5 = 2, A6 = 1 });
            Partners.Add(new Partner { Name = "p4", A1 = 2.62, A2 = 0.27, A3 = 20.55, A4 = 22.56, A5 = 3, A6 = 2 });
            Partners.Add(new Partner { Name = "p5", A1 = 3.36, A2 = 0.66, A3 = 12.67, A4 = 13.65, A5 = 2, A6 = 1 });

            List<string> DominatedModels = new List<string>();

            foreach(Partner p1 in Partners)
            {
                Console.WriteLine($"Checking {p1.Name}");
                foreach (Partner p2 in Partners)
                {                    
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"{p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (
                        (p1.A1 > 4 || p1.A1 < 1) ||
                        ((p2.A1 >= 2 && p2.A1 <= 3) && (p1.A1 < 2 || p1.A1 > 3)) ||
                        (p2.A1 > p1.A1)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A1, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A1, ");
                    }


                    //Check for A2 dominance
                    if ( 
                         (p1.A2 > 0.6 ) ||
                         (p2.A2 <= 0.3 && p1.A2 > 0.3) ||
                         (p2.A2 < p1.A2)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A2, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A2, ");
                    }

                    //Check for A3 dominance
                    if (
                        (p1.A3 > 25) ||
                        (p2.A3 <= 15 && p1.A3 > 15) ||
                        (p2.A3 < p1.A3)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A3, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A3, ");
                    }

                    //Check for A4 dominance                    
                    if (
                        (p2.A4 >= 20 && p1.A4 < 20) ||
                        (p2.A4 > p1.A4) 
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A4, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A4, ");
                    }

                    //Check for A5 dominance                    
                    if (p2.A5 > p1.A5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A5, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A5, ");
                    }

                    //Check for A6 dominance                    
                    if (p2.A6 < p1.A6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("A6, ");
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("A6, ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");                        
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");

            
        }
       
    }

    
}
