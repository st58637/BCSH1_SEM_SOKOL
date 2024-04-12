using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH1_SEM_SOKOL.Model
{
    public class Vesnice
    {
        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Jmeno { get; set; }
        public Hrac Vlastnik { get; set; }
        public int Populace { get; set; }

        public Vesnice(int id, int x, int y, string jmeno, Hrac vlastnik, int populace)
        {
            ID = id;
            X = x;
            Y = y;
            Jmeno = jmeno;
            Vlastnik = vlastnik;
            Populace = populace;
        }
    }
}
