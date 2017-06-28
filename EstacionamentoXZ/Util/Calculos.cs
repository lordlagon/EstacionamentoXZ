using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EstacionamentoXZ.Model;

namespace EstacionamentoXZ.Util
{
    class Calculos
    {
        public static double CalcularEstadia(Estadia estadia)
        {
            double segundos = (estadia.Saida - estadia.Entrada).TotalSeconds;
            return segundos * 1.5;
        }
    }
}

