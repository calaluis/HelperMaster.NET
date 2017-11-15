using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperMaster.NET.General
{
    public class Conversion
    {
        public static decimal StringToDecimal(string Dato, decimal ValorDefecto = 0)
        {
            decimal TempDecimal = 0;
            if(decimal.TryParse(Dato, out TempDecimal))
            {
                return decimal.Parse(Dato);
            }
            return ValorDefecto;
        }
    }
}
