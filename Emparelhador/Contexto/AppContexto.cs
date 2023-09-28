using Emparelhador.Models;
using Microsoft.EntityFrameworkCore;

namespace Emparelhador.Contexto
{
    public class AppContexto : DbContext
    {
        public AppContexto(DbContextOptions<AppContexto> options) : base(options)
        {

        }
        public DbSet<Jogador> jogadores { get; set; }
        public DbSet<JogadorTorneio> jogadoresTorneios { get; set; }
        public DbSet<Mesa> mesas { get; set; }
        public DbSet<PontosJogadorMesa> pontosJogadoresMesa { get; set; }
        public DbSet<Torneio> torneios { get; set; }
        public DbSet<confrontosdiretos> confrontosdiretos { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Jogador>().HasKey(c => c.id);
            mb.Entity<Jogador>().Property(c => c.nome).HasMaxLength(100).IsRequired();
            mb.Entity<JogadorTorneio>().HasKey(c => c.id);  
            mb.Entity<Mesa>().HasKey(c => c.id);
            mb.Entity<PontosJogadorMesa>().HasKey(c => c.id);
            mb.Entity<Torneio>().HasKey(c => c.id);
            mb.Entity<confrontosdiretos>().HasKey(c => c.id);
        }
        
    }
}
