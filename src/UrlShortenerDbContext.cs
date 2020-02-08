using Microsoft.EntityFrameworkCore;
using urlShortener.Models;

namespace urlShortener
{
    public class UrlShortenerDbContext : DbContext
    {
        public DbSet<Url> urls { get; set; }

        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Url>().ToTable("url");
            builder.Entity<Url>().HasKey(u => u.ShortUrl);
            builder.Entity<Url>().Property(u => u.ShortUrl).IsRequired();
            builder.Entity<Url>().Property(u => u.LongUrl).IsRequired();
            builder.Entity<Url>().Property(u => u.LongAbsoluteUri).IsRequired();
           
        }
    }
}