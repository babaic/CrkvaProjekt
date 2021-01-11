using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace CrkvaProjekt.ViewModels
{
    public class NovostiUrediVM
    {
        public int NovostiID { get; set; }
        [StringLength(300, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Text { get; set; }
        [BindProperty]
        public UploadFile UploadSlike { get; set; }
        public DateTime DatumObjavljivanja { get; set; } = DateTime.Now;
        [StringLength(30, MinimumLength = 3)]
        [Required]
        [DataType(DataType.Text)]
        public string Naslov { get; set; }

        public class UploadFile
        {
            public IFormFile Slika { get; set; }
        }
    }
}
