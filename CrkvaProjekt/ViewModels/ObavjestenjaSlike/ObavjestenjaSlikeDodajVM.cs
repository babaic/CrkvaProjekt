using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.ObavjestenjaSlike
{
    public class ObavjestenjaSlikeDodajVM
    {
        public int ObavjestenjaSlikeID { get; set; }
        public int ObavjestenjeID { get; set; }
        public string Naslov { get; set; }
        public string Kategorija { get; set; }
        public UploadFile UploadSlike { get; set; }

        public class UploadFile
        {
            public IFormFile Slika { get; set; }
        }
    }
}
