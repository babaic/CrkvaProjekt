using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels.NovostiSlike;
using CrkvaProjekt.ViewModels.ObavjestenjaSlike;
using Microsoft.AspNetCore.Mvc;
using CrkvaProjekt.Translate;

namespace CrkvaProjekt.Controllers
{
    public class ProcitajViseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Translator _translate = new Translator();
        public ProcitajViseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Prikazi(int ObavjestenjeID)
        {
            Obavjestenja o = _context.Obavjestenja.Where(x => x.ObavjestenjaID == ObavjestenjeID).FirstOrDefault();
            ObavjestenjaKategorije k = _context.ObavjestenjaKategorije.Where(x => x.ObavjestenjaKategorijeID == o.ObavjestenjaKategorijeID).FirstOrDefault();
            ObavjestenjaSlikeViseVM model = new ObavjestenjaSlikeViseVM();
            model.ObavjestenjeID = ObavjestenjeID;
            model.Naslov = o.Naslov;
            model.Kategorija = k.Naziv;
            model.Text = o.Text;
            model.DatumObjavljivanja = o.DatumObjavljivanja;
            model.SlikaThumbnail = o.Slika;
            model.lista = _context.ObavjestenjaSlike.Where(x => x.ObavjestenjaID == ObavjestenjeID).Select(x => new ObavjestenjaSlikeViseVM.Row()
            {
                ObavjestenjaSlikeID=x.ObavjestenjaSlikeID,
                Slika=x.Slika
            }).ToList();

            return View(model);
        }
        public IActionResult PrikaziNovosti(int NovostiID)
        {
            Novosti o = _context.Novosti.Where(x => x.NovostiID == NovostiID).FirstOrDefault();
            NovostiSlikeViseVM model = new NovostiSlikeViseVM();
            model.NovostiID = NovostiID;
            model.Naslov = o.Naslov;
            model.Text = o.Text;
            model.SlikaThumbnail = o.Slika;
            model.DatumObjavljivanja = o.DatumObjavljivanja;
            model.lista = _context.NovostiSlike.Where(x => x.NovostiID == NovostiID).Select(x => new NovostiSlikeViseVM.Row()
            {
                NovostiSlikaID=x.NovostiSlikeID,
                Slika=x.Slika
            }).ToList();

            return View(model);
        }
        public IActionResult PrikaziCir(int ObavjestenjeID)
        {
            Obavjestenja o = _context.Obavjestenja.Where(x => x.ObavjestenjaID == ObavjestenjeID).FirstOrDefault();
            ObavjestenjaKategorije k = _context.ObavjestenjaKategorije.Where(x => x.ObavjestenjaKategorijeID == o.ObavjestenjaKategorijeID).FirstOrDefault();
            ObavjestenjaSlikeViseVM model = new ObavjestenjaSlikeViseVM();
            model.ObavjestenjeID = ObavjestenjeID;
            model.Naslov = _translate.ConvertLatinToCyrillic(o.Naslov);
            model.Kategorija = _translate.ConvertLatinToCyrillic(k.Naziv);
            model.Text = _translate.ConvertLatinToCyrillic(o.Text);
            model.DatumObjavljivanja = o.DatumObjavljivanja;
            model.SlikaThumbnail = o.Slika;
            model.lista = _context.ObavjestenjaSlike.Where(x => x.ObavjestenjaID == ObavjestenjeID).Select(x => new ObavjestenjaSlikeViseVM.Row()
            {
                ObavjestenjaSlikeID = x.ObavjestenjaSlikeID,
                Slika = x.Slika
            }).ToList();

            return View(model);
        }
        public IActionResult PrikaziNovostiCir(int NovostiID)
        {
            Novosti o = _context.Novosti.Where(x => x.NovostiID == NovostiID).FirstOrDefault();
            NovostiSlikeViseVM model = new NovostiSlikeViseVM();
            model.NovostiID = NovostiID;
            model.Naslov = _translate.ConvertLatinToCyrillic(o.Naslov);
            model.Text = _translate.ConvertLatinToCyrillic(o.Text);
            model.SlikaThumbnail = o.Slika;
            model.DatumObjavljivanja = o.DatumObjavljivanja;
            model.lista = _context.NovostiSlike.Where(x => x.NovostiID == NovostiID).Select(x => new NovostiSlikeViseVM.Row()
            {
                NovostiSlikaID = x.NovostiSlikeID,
                Slika = x.Slika
            }).ToList();

            return View(model);
        }
    }
}
