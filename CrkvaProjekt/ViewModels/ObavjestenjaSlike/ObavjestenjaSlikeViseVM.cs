using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.ObavjestenjaSlike
{
    public class ObavjestenjaSlikeViseVM
    {
        public int ObavjestenjeID { get; set; }
        public string Naslov { get; set; }
        public string Text { get; set; }
        public string Kategorija { get; set; }
        public DateTime DatumObjavljivanja { get; set; }
        public byte[] SlikaThumbnail { get; set; }
        public List<Row> lista { get; set; }

        public class Row
        {
            public int ObavjestenjaSlikeID { get; set; }
            public byte[] Slika { get; set; }
        }
    }
}
