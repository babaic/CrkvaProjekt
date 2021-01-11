using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrkvaProjekt.ViewModels
{
    public class DonirajKategorijeUrediVM
    {
        public int DonirajKategorijeID { get; set; }
        [StringLength(60,MinimumLength =3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naziv { get; set; }
    }
}
