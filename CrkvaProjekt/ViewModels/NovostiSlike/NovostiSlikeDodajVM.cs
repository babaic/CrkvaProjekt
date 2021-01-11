using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels.NovostiSlike
{
    public class NovostiSlikeDodajVM
    {
        public int NovostiSlikeID { get; set; }
        public int NovostiID { get; set; }
        public string Naslov { get; set; }
        public UploadFile UploadSlike { get; set; }

        public class UploadFile
        {
            public IFormFile Slika { get; set; }
        }
    }
}
