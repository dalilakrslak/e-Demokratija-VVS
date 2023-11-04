using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e_Demokratija
{
    public class Izbori
    {
        private List<Glasac> glasaci;
        public Izbori() 
        {
            glasaci = new List<Glasac>();
        }
        public Izbori(List<Glasac> glasac) { 
            this.glasaci = glasac; 
        }
        public List<Glasac> Glasaci { get => glasaci; set => glasaci = value; }
    }
}
