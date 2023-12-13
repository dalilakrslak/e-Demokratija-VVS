using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Spy : IProvjera
    {
        public bool Glasao { get; set; }
        public int Glasovi { get; set; }
        public bool DaLiJeVecGlasao(string IDBroj)
        {
            if (Glasao == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool DaLiImaGlas(int redniBroj)
        {
            if (Glasovi == 0)
            {
                return false;
            }
            return true;
        }
    }
}
