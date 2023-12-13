using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public interface IProvjera
    {
        public bool DaLiJeVecGlasao(string IDBroj);
        public bool DaLiImaGlas(int redniBroj);
    }
}
