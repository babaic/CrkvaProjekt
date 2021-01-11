using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels.ObavjestenjaSlike;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
    public class ObavjestenjaSlikeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ObavjestenjaSlikeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi(int ObavjestenjeID)
        {
            List<ObavjestenjaSlikePrikaziVM> slike = _context.ObavjestenjaSlike.Where(x => x.ObavjestenjaID == ObavjestenjeID).Select(x => new ObavjestenjaSlikePrikaziVM
            {
                Naslov = x.Obavjestenja.Naslov,
                Kategorija = x.Obavjestenja.ObavjestenjaKategorije.Naziv,
                Slika = x.Slika,
                ObavjestenjeID = x.ObavjestenjaID,
                ObavjestenjaSlikeID = x.ObavjestenjaSlikeID
            }).ToList();
            ViewData["obavjestenje-kljuc"] = ObavjestenjeID;
            ViewData["slike-kljuc"] = slike;
            return View();
        }
        public IActionResult Obrisi(int ObavjestenjeSlikaID)
        {
            ObavjestenjaSlike slika = _context.ObavjestenjaSlike.Find(ObavjestenjeSlikaID);
            int ObavijestID = slika.ObavjestenjaID;
            if (slika == null)
            {
                TempData["error_poruka"] = "Slika ne postoji. ";
                return RedirectToAction("Prikazi", new { ObavjestenjeID = slika.ObavjestenjaID });
            }
            else
            {
                _context.Remove(slika);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    TempData["error_poruka"] = "Nemoguće izbrisati sliku trenutno. Vezana je za nešto u bazi. ";
                }
                TempData["success_poruka"] = "Uspješno ste izbrisali sliku. ";
            }
            return RedirectToAction("Prikazi", new { ObavjestenjeID = ObavijestID });
        }
        public IActionResult Dodaj(int ObavjestenjeID)
        {
            Obavjestenja o = _context.Obavjestenja.Find(ObavjestenjeID);
            ObavjestenjaKategorije k = _context.ObavjestenjaKategorije.Where(x => x.ObavjestenjaKategorijeID == o.ObavjestenjaKategorijeID).FirstOrDefault();
            ObavjestenjaSlikeDodajVM model = new ObavjestenjaSlikeDodajVM();
            model.ObavjestenjeID = o.ObavjestenjaID;
            model.Naslov = o.Naslov;
            model.Kategorija = k.Naziv;
            return View("Dodaj", model);
        }
        public IActionResult Snimi(ObavjestenjaSlikeDodajVM input)
        {
            ObavjestenjaSlike o = new ObavjestenjaSlike();
            _context.Add(o);
            TempData["success_poruka"] = "Uspješno ste dodali novu sliku za obavještenje. ";
            o.ObavjestenjaID = input.ObavjestenjeID;
            o.ObavjestenjaSlikeID = input.ObavjestenjaSlikeID;
            if (input.UploadSlike != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.UploadSlike.Slika.CopyTo(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        o.Slika = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "File je prevelik.");
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Prikazi", new { ObavjestenjeID = input.ObavjestenjeID });
        }
    }
}
