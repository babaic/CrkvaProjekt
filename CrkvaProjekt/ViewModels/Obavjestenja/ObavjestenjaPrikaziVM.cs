using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.Obavjestenja
{
    public class ObavjestenjaPrikaziVM
    {
        public int ObavjestenjaID { get; set; }
        public string Text { get; set; }
        public byte[] Slika { get; set; }
        public DateTime DatumObjavljivanja { get; set; } = DateTime.Now;
        public string Naslov { get; set; }
        public string ObavjestenjeKategorija { get; set; }
    }
}
