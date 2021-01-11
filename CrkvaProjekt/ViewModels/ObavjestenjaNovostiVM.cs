using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels.Obavjestenja;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrkvaProjekt.ViewModels
{
    public class ObavjestenjaNovostiVM
    {
        public List<Novosti> listanovosti { get; set; }
        public List<ObavjestenjaPrikaziVM> listaobavjestenja { get; set; }
    }
}
