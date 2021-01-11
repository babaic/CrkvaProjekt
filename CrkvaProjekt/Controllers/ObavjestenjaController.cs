using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels.Obavjestenja;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrkvaProjekt.Controllers
{
    public class ObavjestenjaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ObavjestenjaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi()
        {
            List<ObavjestenjaPrikaziVM> obavjestenja = _context.Obavjestenja.Select
                (x => new ObavjestenjaPrikaziVM
            {
                    ObavjestenjaID=x.ObavjestenjaID,
                    Naslov=x.Naslov,
                    DatumObjavljivanja=x.DatumObjavljivanja,
                    Slika=x.Slika,
                    Text=x.Text,
                    ObavjestenjeKategorija=x.ObavjestenjaKategorije.Naziv
            }).ToList();
            ViewData["obavjestenja-kljuc"] = obavjestenja;
            return View();
        }
        public IActionResult Obrisi(int ObavjestenjeID) 
        {
            Obavjestenja o = _context.Obavjestenja.Find(ObavjestenjeID);
            List<ObavjestenjaSlike> slike = _context.ObavjestenjaSlike.Where(x => x.ObavjestenjaID == ObavjestenjeID).ToList();
            if (o == null)
            {
                TempData["error_poruka"] = "Obavještenje ne postoji. ";
            }
            else
            {
                if (slike != null)
                {
                    foreach(ObavjestenjaSlike x in slike)
                    {
                        _context.Remove(x);
                    }
                }
                _context.Remove(o);
                try
                {
                    _context.SaveChanges();
                }
                catch
                {
                    TempData["error_poruka"] = "Nemoguće izbrisati obavještenje trenutno. Vezano je za nešto u bazi. ";
                }
                TempData["success_poruka"] = "Uspješno ste izbrisali obavještenje. ";
            }
            return RedirectToAction("Prikazi");
        }
        public IActionResult Dodaj()
        {
            ObavjestenjaUrediVM model = new ObavjestenjaUrediVM();
            model.kategorije = _context.ObavjestenjaKategorije.Select(x => new SelectListItem(x.Naziv, x.ObavjestenjaKategorijeID.ToString())).ToList();
            return View("Uredi", model);
        }
        public IActionResult Uredi(int ObavjestenjeID)
        {
            Obavjestenja o = _context.Obavjestenja.Find(ObavjestenjeID);
            if (o == null)
            {
                TempData["error_poruka"] = "Obavještenje ne postoji. ";
                return RedirectToAction("Prikazi");
            }
            ObavjestenjaUrediVM model = new ObavjestenjaUrediVM();
            model.ObavjestenjaID = o.ObavjestenjaID;
            model.Naslov = o.Naslov;
            model.Text = o.Text;
            model.kategorije = _context.ObavjestenjaKategorije.Select(x => new SelectListItem(x.Naziv, x.ObavjestenjaKategorijeID.ToString())).ToList();
            return View("Uredi", model);
        }
        public IActionResult Snimi(ObavjestenjaUrediVM input)
        {
            Obavjestenja o;
            if (input.ObavjestenjaID == 0)
            {
                o = new Obavjestenja();
                _context.Add(o);
                TempData["success_poruka"] = "Uspješno ste dodali novo obavještenje. ";
            }
            else
            {
                o = _context.Obavjestenja.Find(input.ObavjestenjaID);
            }
            o.ObavjestenjaID = input.ObavjestenjaID;
            o.Naslov = input.Naslov;
            o.Text = input.Text;
            o.ObavjestenjaKategorijeID = input.ObavjestenjaKategorijeID;
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
                        TempData["error_poruka"] = "File je prevelik. ";
                    }
                }
            }
            _context.SaveChanges();
            if (input.ObavjestenjaID == 0)
                ViewData["success_poruka"] = "Uspješno ste dodali novo obavještenje. ";
            else
                ViewData["success_poruka"] = "Uspješno ste izmijenili podatke o obavještenju.";

            return RedirectToAction("Prikazi");
        }
    }
}
