using System;
using System.Collections.Generic;
using System.Text;
using static grpassign_bsm2020.Utilities;

namespace grpassign_bsm2020
{
    public class DominanceMethod
    {        
        public static void Combination_DominanceMethod()
        {
            Console.OutputEncoding = Encoding.Unicode;            
            List<Partner> Combinations = DataModels.GetCombinations();

            Console.WriteLine("\nAttribute values of combinations");
            Console.WriteLine("------------------------------------");
            foreach (var p in Combinations)
                Console.WriteLine(p.ToString());

            Console.WriteLine("\nChecking for domination");
            Console.WriteLine("------------------------------------");
            List<string> DominatedModels = new List<string>();
            List<int> DominatedScores = new List<int>();
            foreach (Partner p1 in Combinations)
            {
                int dominatedscore = 0;
                Console.WriteLine($"           {p1.Name}");
                Console.WriteLine($"           ----- ");
                Console.WriteLine("          1 2 3 4 5 6");
                foreach (Partner p2 in Combinations)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"   {p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (
                        (p1.A1 > 8 || p1.A1 < 2) ||
                        ((p2.A1 >= 4 && p2.A1 <= 6) && (p1.A1 < 4 || p1.A1 > 6))
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }


                    //Check for A2 dominance
                    if (
                         (p1.A2 > 1.2) ||
                         (p2.A2 <= 0.6 && p1.A2 > 0.6)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A3 dominance
                    if (
                        (p1.A3 > 25) ||
                        (p2.A3 <= 15 && p1.A3 > 15) ||
                        (p2.A3 < p1.A3)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A4 dominance                    
                    if (
                        (p2.A4 >= 20 && p1.A4 < 20) ||
                        (p2.A4 > p1.A4)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A5 dominance                    
                    if (p2.A5 > p1.A5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A6 dominance                    
                    if (p2.A6 < p1.A6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();

                }
                DominatedScores.Add(dominatedscore);
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");
            Console.WriteLine();
            Console.WriteLine("Dominaton Scores");
            Console.WriteLine("------------------------------------");

            for (int i = 0; i < DominatedScores.Count; i++)
            {
                Console.WriteLine($"{Combinations[i].Name}: {DominatedScores[i] / 54.0 * 100.0:0.00} %");
            }
        }

        public static void Combination_DominanceValueMethod()
        {
            Console.OutputEncoding = Encoding.Unicode;
            List<Partner> Combinations = DataModels.GetCombinations();

            Console.WriteLine("\nAttribute values of combinations");
            Console.WriteLine("------------------------------------");
            foreach (var p in Combinations)
                Console.WriteLine(p.ToString());

            Console.WriteLine("\nChecking for domination");
            Console.WriteLine("------------------------------------");
            List<string> DominatedModels = new List<string>();
            List<int> DominatedScores = new List<int>();
            foreach (Partner p1 in Combinations)
            {
                int dominatedscore = 0;
                Console.WriteLine($"           {p1.Name}");
                Console.WriteLine($"           ----- ");
                Console.WriteLine("          1 2 3 4 5 6");
                foreach (Partner p2 in Combinations)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"   {p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (p2.VA1>p1.VA1)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }


                    //Check for A2 dominance
                    if (p2.VA2 > p1.VA2)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A3 dominance
                    if (p2.VA3 > p1.VA3)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A4 dominance                    
                    if (p2.VA4 > p1.VA4)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A5 dominance                    
                    if (p2.VA5 > p1.VA5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A6 dominance                    
                    if (p2.VA6 > p1.VA6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();

                }
                DominatedScores.Add(dominatedscore);
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");
            Console.WriteLine();
            Console.WriteLine("Dominaton Scores");
            Console.WriteLine("------------------------------------");

            for (int i = 0; i < DominatedScores.Count; i++)
            {
                Console.WriteLine($"{Combinations[i].Name}: {DominatedScores[i] / 54.0 * 100.0:0.00} %");
            }
        }


        public static void Single_DominanceMethod()
        {
            Console.OutputEncoding = Encoding.Unicode;
            List<Partner> Partners = DataModels.GetPartners();

            Console.WriteLine("\nAttribute values of partners");
            Console.WriteLine("------------------------------------");
            foreach (var p in Partners)
                Console.WriteLine(p.ToString());

            Console.WriteLine("\nChecking for domination");
            Console.WriteLine("------------------------------------");
            List<string> DominatedModels = new List<string>();
            List<int> DominatedScores = new List<int>();
            foreach (Partner p1 in Partners)
            {
                int dominatedscore = 0;
                Console.WriteLine($"           {p1.Name}");
                Console.WriteLine($"           ----- ");
                Console.WriteLine("          1 2 3 4 5 6");
                foreach (Partner p2 in Partners)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"   {p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (
                        (p1.A1 > 4 || p1.A1 < 1) ||
                        ((p2.A1 >= 2 && p2.A1 <= 3) && (p1.A1 < 2 || p1.A1 > 3))
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }


                    //Check for A2 dominance
                    if (
                         (p1.A2 > 0.6) ||
                         (p2.A2 <= 0.3 && p1.A2 > 0.3)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A3 dominance
                    if (
                        (p1.A3 > 25) ||
                        (p2.A3 <= 15 && p1.A3 > 15) ||
                        (p2.A3 < p1.A3)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A4 dominance                    
                    if (
                        (p2.A4 >= 20 && p1.A4 < 20) ||
                        (p2.A4 > p1.A4)
                        )
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A5 dominance                    
                    if (p2.A5 > p1.A5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A6 dominance                    
                    if (p2.A6 < p1.A6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();

                }
                DominatedScores.Add(dominatedscore);
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");
            Console.WriteLine();
            Console.WriteLine("Dominaton Scores");
            Console.WriteLine("------------------------------------");

            for (int i = 0; i < DominatedScores.Count; i++)
            {
                Console.WriteLine($"{Partners[i].Name}: {DominatedScores[i] / 24.0 * 100.0:0.00} %");
            }
        }

        public static void Single_DominanceValueMethod()
        {
            Console.OutputEncoding = Encoding.Unicode;
            List<Partner> Partners = DataModels.GetPartners();

            Console.WriteLine("\nAttribute values of partners");
            Console.WriteLine("------------------------------------");
            foreach (var p in Partners)
                Console.WriteLine(p.ToString());

            Console.WriteLine("\nChecking for domination");
            Console.WriteLine("------------------------------------");
            List<string> DominatedModels = new List<string>();
            List<int> DominatedScores = new List<int>();
            foreach (Partner p1 in Partners)
            {
                int dominatedscore = 0;
                Console.WriteLine($"           {p1.Name}");
                Console.WriteLine($"           ----- ");
                Console.WriteLine("          1 2 3 4 5 6");
                foreach (Partner p2 in Partners)
                {
                    if (p1.Name == p2.Name) continue;
                    Console.Write($"   {p2.Name}: ");
                    bool isdominated = true;


                    //Check for A1 dominanace
                    if (p2.VA1 > p1.VA1)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }


                    //Check for A2 dominance
                    if (p2.VA2 > p1.VA2)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A3 dominance
                    if (p2.VA3 > p1.VA3)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A4 dominance                    
                    if (p2.VA4 > p1.VA4)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A5 dominance                    
                    if (p2.VA5 > p1.VA5)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    //Check for A6 dominance                    
                    if (p2.VA6 > p1.VA6)
                    {
                        isdominated &= true;
                        ConsoleWriteRed("\u2022 ");
                        dominatedscore++;
                    }
                    else
                    {
                        isdominated &= false;
                        ConsoleWriteGreen("\u2022 ");
                    }

                    if (isdominated)
                    {
                        DominatedModels.Add(p1.Name);
                        Console.WriteLine($"{p1.Name} is dominated by {p2.Name}");
                    }
                    Console.WriteLine();

                }
                DominatedScores.Add(dominatedscore);
                Console.WriteLine("\n");
            }

            Console.WriteLine($"Total dominated models: {DominatedModels.Count}");
            Console.WriteLine();
            Console.WriteLine("Dominaton Scores");
            Console.WriteLine("------------------------------------");

            for (int i = 0; i < DominatedScores.Count; i++)
            {
                Console.WriteLine($"{Partners[i].Name}: {DominatedScores[i] / 24.0 * 100.0:0.00} %");
            }
        }
    }
}
