using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace grpassign_bsm2020
{
    public class WeightCombination
    {
        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }
        public double A4 { get; set; }
        public double A5 { get; set; }
        public double A6 { get; set; }

        
        public string DominanceWeight //Dominance weight only is applicable to distinct weights
        {
            get
            {
                string dw = string.Empty;
                double maxwt = new[]{ A1, A2, A3, A4, A5, A6 }.Max();
                if (maxwt == A1) return "W1";
                else if (maxwt == A2) return "W2";
                else if (maxwt == A3) return "W3";
                else if (maxwt == A4) return "W4";
                else if (maxwt == A5) return "W5";
                else if (maxwt == A6) return "W6";                
                return dw;
            }
        }

        public override string ToString()
        {
            return $"{A1:#0.00},{A2:#0.00},{A3:#0.00},{A4:#0.00},{A5:#0.00},{A6:#0.00},{A1 + A2 + A3 + A4 + A5 + A6:#0.00}";
        }
    }

    public class Partner
    {
        public string Name { get; set; }
        public bool IsCombination { get; set; } = false;

        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }
        public double A4 { get; set; }
        public double A5 { get; set; }
        public double A6 { get; set; }

        public double VA1 { get; private set; } = 0;
        public double VA2 { get; private set; } = 0;
        public double VA3 { get; private set; } = 0;
        public double VA4 { get; private set; } = 0;
        public double VA5 { get; private set; } = 0;
        public double VA6 { get; private set; } = 0;

        public override string ToString()
        {
            return $"{Name}:t, {A1:#0.00} ({VA1:#0.00}), {A2:#0.00} ({VA2:#0.00}), {A3:#0.00} ({VA3:#0.00}), {A4:#0.00} ({VA4:#0.00}), {A5}  ({VA5:#0.00}), {A6} ({VA6:#0.00})";
            //return $"{Name},{VA1:#0.00},{VA2:#0.00},{VA3:#0.00},{VA4:#0.00},{VA5:#0.00},{VA6:#0.00}";

        }
        public string PrintValues()
        {
            return $"{Name},{VA1:#0.00},{VA2:#0.00},{VA3:#0.00},{VA4:#0.00},{VA5:#0.00},{VA6:#0.00}";
        }
        public string PrintAttributes()
        {
            return $"{Name},{A1:#0.00},{A2:#0.00},{A3:#0.00},{A4:#0.00},{A5},{A6}";
        }
        public void GetValueFunctions()
        {
            //VA1 VA2
            if (!IsCombination)
            {
                //VA1
                if (A1 >= 4 || A1 <= 1)
                    VA1 = 0;
                else if (A1 >= 2 && A1 <= 3)
                    VA1 = 1;
                else if (A1 > 1 && A1 < 2)
                    VA1 = A1 - 1;
                else if (A1 > 3 && A1 < 4)
                    VA1 = 4 - A1;

                //VA2
                if (A2 >= 0.6)
                    VA2 = 0;
                else if (A2 >= 0 && A2 <= 0.3)
                    VA2 = 1;
                else if (A2 > 0.3 && A2 < 0.6)
                    VA2 = 2 - (A2 / 0.3);
            }
            else
            {
                //VA1
                if (A1 >= 8 || A1 <= 2)
                    VA1 = 0;
                else if (A1 >= 4 && A1 <= 6)
                    VA1 = 1;
                else if (A1 > 2 && A1 < 4)
                    VA1 = 0.5 * (A1 - 2);
                else if (A1 > 6 && A1 < 8)
                    VA1 = 0.5 * (8 - A1);
                
                //VA2
                if (A2 >= 1.2)
                    VA2 = 0;
                else if (A2 >= 0 && A2 <= 0.6)
                    VA2 = 1;
                else if (A2 > 0.6 && A2 < 1.2)
                    VA2 = 2 - (A2 / 0.6);
            }

            //VA3
            if (A3 >= 25)
                VA3 = 0;
            else if (A3 >= 0 && A3 <= 15)
                VA3 = 1;
            else if (A3 > 15 && A3 < 25)
                VA3 = 2.5 - (A3 / 10);

            //VA4
            if (A4 >= 20)
                VA4 = 1;
            else if (A4 > 0 && A4 < 20)
                VA4 = A4 / 20;

            //VA5
            switch (A5)
            {
                case 1.0:
                    VA5 = 0;
                    break;
                case 2.0:
                    VA5 = 0.5;
                    break;
                case 3.0:
                    VA5 = 1;
                    break;
                default:
                    throw new Exception("Unknown attribute level for A5");
            }

            //VA6
            switch (A6)
            {
                case 1.0:
                    VA6 = 1;
                    break;
                case 2.0:
                    VA6 = 0.5;
                    break;
                case 3.0:
                    VA6 = 0;
                    break;
                default:
                    throw new Exception("Unknown attribute level for A6");
            }

            if (Utilities.UseAlterateValueFunctionforSwitchingRisk)
            {
                switch (A6)
                {
                    case 1.0:
                        VA6 = 1;
                        break;
                    case 2.0:
                        VA6 = 0.75;
                        break;
                    case 3.0:
                        VA6 = 0.5;
                        break;
                    default:
                        throw new Exception("Unknown attribute level for A6");
                }
            }
        }


        public double AdditiveValueFunction { get; private set; }
        public void GetAdditiveValueFunction(WeightCombination W)
        {
            AdditiveValueFunction = VA1 * W.A1 + VA2 * W.A2 + VA3 * W.A3 + VA4 * W.A4 + VA5 * W.A5 + VA6 * W.A6;
            
        }     


    }
}
