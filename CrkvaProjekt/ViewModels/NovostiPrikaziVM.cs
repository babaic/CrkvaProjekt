using CrkvaProjekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels
{
    public class NovostiPrikaziVM
    {
        public int NovostiID { get; set; }
        public string Text { get; set; }
        public byte[] Slika { get; set; }
        public DateTime DatumObjavljivanja { get; set; }
        public string Naslov { get; set; }
    }
}
