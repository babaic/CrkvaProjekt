using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.NovostiSlike
{
    public class NovostiSlikeViseVM
    {
        public int NovostiID { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public DateTime DatumObjavljivanja { get; set; }
        public byte[] SlikaThumbnail { get; set; }
        public List<Row> lista { get; set; }


        public class Row
        {
            public int NovostiSlikaID { get; set; }
            public byte[] Slika { get; set; }
        }
    }
}
