using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrkvaProjekt.Data;
using CrkvaProjekt.Models;
using CrkvaProjekt.ViewModels.NovostiSlike;
using Microsoft.AspNetCore.Mvc;

namespace CrkvaProjekt.Controllers
{
    public class NovostiSlikeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NovostiSlikeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Prikazi(int NovostiID)
        {
            List<NovostiSlikePrikaziVM> slike = _context.NovostiSlike.Where(x => x.NovostiID == NovostiID).Select(x => new NovostiSlikePrikaziVM
            {
                NovostiID = x.NovostiID,
                Naslov=x.Novosti.Naslov,
                Slika=x.Slika,
                NovostiSlikeID=x.NovostiSlikeID
            }).ToList();
            ViewData["novosti-kljuc"] = NovostiID;
            ViewData["slike-kljuc"] = slike;
            return View();
        }
        public IActionResult Obrisi(int NovostiSlikeID)
        {
            NovostiSlike slika = _context.NovostiSlike.Find(NovostiSlikeID);
            int NovostID = slika.NovostiID;
            if (slika == null)
            {
                TempData["error_poruka"] = "Slika ne postoji. ";
                return RedirectToAction("Prikazi", new { NovostiID = slika.NovostiID });
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
            return RedirectToAction("Prikazi", new { NovostiID = NovostID });
        }
        public IActionResult Dodaj(int NovostiID)
        {
            Novosti n = _context.Novosti.Find(NovostiID);
            NovostiSlikeDodajVM model = new NovostiSlikeDodajVM();
            model.NovostiID = NovostiID;
            model.Naslov = n.Naslov;
            return View("Dodaj", model);
        }
        public IActionResult Snimi(NovostiSlikeDodajVM input)
        {
            NovostiSlike n = new NovostiSlike();
            _context.Add(n);
            TempData["success_poruka"] = "Uspješno ste dodali novu sliku za novost. ";
            n.NovostiSlikeID = input.NovostiSlikeID;
            n.NovostiID = input.NovostiID;
            if (input.UploadSlike != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.UploadSlike.Slika.CopyTo(memoryStream);
                    if (memoryStream.Length < 2097152)
                    {
                        n.Slika = memoryStream.ToArray();
                    }
                    else
                    {
                        ModelState.AddModelError("File", "File je prevelik.");
                    }
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Prikazi", new { NovostiID = input.NovostiID });
        }
    }
}
