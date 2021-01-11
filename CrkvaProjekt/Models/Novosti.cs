using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.Models
{
    public class Novosti
    {
        public int NovostiID { get; set; }
        [StringLength(600, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        public byte[] Slika { get; set; }
        public DateTime DatumObjavljivanja { get; set; } = DateTime.Now;
        [StringLength(30, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naslov { get; set; }
    }
}
