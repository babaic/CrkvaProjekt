using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.Models
{
    public class ObavjestenjaKategorije
    {
        public int ObavjestenjaKategorijeID { get; set; }
        [StringLength(30, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naziv { get; set; }
    }
}
