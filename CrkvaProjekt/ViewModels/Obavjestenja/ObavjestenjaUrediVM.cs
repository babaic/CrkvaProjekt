using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.Obavjestenja
{
    public class ObavjestenjaUrediVM
    {
        public int ObavjestenjaID { get; set; }
        [StringLength(300, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        [StringLength(30, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naslov { get; set; }
        public int ObavjestenjaKategorijeID { get; set; }
        public List<SelectListItem> kategorije { get; set; }
        [BindProperty]
        public UploadFile UploadSlike { get; set; }

        public class UploadFile
        {
            public IFormFile Slika { get; set; }
        }
    }
}

