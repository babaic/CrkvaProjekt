using System;
using System.Collections.Generic;
using System.Text;
using CrkvaProjekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CrkvaProjekt.Data
{
    public partial class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Novosti> Novosti { get; set; }
        public DbSet<NovostiSlike> NovostiSlike { get; set; }
        public DbSet<Obavjestenja> Obavjestenja { get; set; }
        public DbSet<ObavjestenjaKategorije> ObavjestenjaKategorije { get; set; }
        public DbSet<ObavjestenjaSlike> ObavjestenjaSlike { get; set; }
        public DbSet<ObrediKategorije> ObrediKategorije { get; set; }
        public DbSet<DonirajKategorije> DonirajKategorije { get; set; }

    
       

    }
}
