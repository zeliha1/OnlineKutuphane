using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineKutuphane.Models;

namespace OnlineKutuphane.Utility
{
    public class KutuphaneDbContext : IdentityDbContext
    {
        public KutuphaneDbContext(DbContextOptions<KutuphaneDbContext> options) : base(options) { }
        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> kitaplar {  get; set; }
		public DbSet<Kiralama> Kiralamalar { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

	}
}
