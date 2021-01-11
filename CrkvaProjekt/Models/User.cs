using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.Models
{
    public class User : IdentityUser<int>
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
