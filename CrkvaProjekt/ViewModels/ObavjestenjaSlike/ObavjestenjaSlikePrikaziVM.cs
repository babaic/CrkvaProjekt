using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.ObavjestenjaSlike
{
    public class ObavjestenjaSlikePrikaziVM
    {
        public int ObavjestenjaSlikeID { get; set; }
        public int ObavjestenjeID { get; set; }
        public byte[] Slika { get; set; }
        public string Naslov { get; set; }
        public string Kategorija { get; set; }
    }
}
