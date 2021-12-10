using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexts
{
  public class ProEventosContext : DbContext
  {

    public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options) { }

    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Lote> Lotes { get; set; }
    public DbSet<Palestrante> Palestrantes { get; set; }
    public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
    public DbSet<RedeSocial> RedesSocials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PalestranteEvento>()
        .HasKey(PE => new { PE.PalestranteId, PE.EventoId });

      modelBuilder.Entity<Evento>()
        .HasMany(e => e.RedesSociais)
        .WithOne(rs => rs.Evento)
        .OnDelete(DeleteBehavior.Cascade);

      modelBuilder.Entity<Palestrante>()
        .HasMany(p => p.RedeSocials)
        .WithOne(rs => rs.Palestrante)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}