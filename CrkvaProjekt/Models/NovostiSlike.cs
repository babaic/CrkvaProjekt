using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.Models
{
    public class NovostiSlike
    {
        public int NovostiSlikeID { get; set; }
        public int NovostiID { get; set; }
        public Novosti Novosti { get; set; }
        public byte[] Slika { get; set; }
    }
}
