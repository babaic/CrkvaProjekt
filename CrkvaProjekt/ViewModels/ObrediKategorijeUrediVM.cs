using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels
{
    public class ObrediKategorijeUrediVM
    {
        public int ObrediKategorijeID { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naziv { get; set; }
    }
}
