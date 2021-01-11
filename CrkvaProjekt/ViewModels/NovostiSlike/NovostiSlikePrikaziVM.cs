using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.NovostiSlike
{
    public class NovostiSlikePrikaziVM
    {
        public int NovostiSlikeID { get; set; }
        public int NovostiID { get; set; }
        public byte[] Slika { get; set; }
        public string Naslov { get; set; }
    }
}
