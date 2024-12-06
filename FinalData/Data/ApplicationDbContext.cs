using FinalData.Data.Entitys;
using FinalData.Data.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalData.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TipoDeporte>().ToTable(nameof(TipoDeporte)).HasKey(x => x.IdTipo);
            builder.Entity<TipoDeporte>().HasMany(x => x.Deportes).WithOne(x => x.TipoDeporte).HasForeignKey(x => x.IdTipo);
        }
        public DbSet<Deporte> Deportes { get; set; }
        public DbSet<TipoDeporte> TipoDeportes { get; set; }
    }
}
