using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH1_SEM_SOKOL.Model
{
    public class Aliance
    {
        public int? ID { get; set; }
        public string Zkratka { get; set; }
        public List<Hrac> Hraci { get; set; } = new List<Hrac>();
        public int PopulaceAliance
        {
            get { return Hraci.SelectMany(h => h.Vesnice).Sum(v => v.Populace); }
        }

        public Aliance(int? id, string zkratka)
        {
            ID = id;
            Zkratka = zkratka;
        }
    }
}
