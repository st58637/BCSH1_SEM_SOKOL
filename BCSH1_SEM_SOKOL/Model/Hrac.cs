using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH1_SEM_SOKOL.Model
{
    public class Hrac
    {
        public int ID { get; set; }
        public string Jmeno { get; set; }
        public Narod Narod { get; set; }
        public List<Vesnice> Vesnice { get; set; } = new List<Vesnice>();
        public Aliance? Aliance { get; set; }
        public int CelkovaPopulace
        {
            get { return Vesnice.Sum(v => v.Populace); }
        }

        public Hrac(int id, string jmeno, Narod narod)
        {
            ID = id;
            Jmeno = jmeno;
            Narod = narod;
        }
    }
}
