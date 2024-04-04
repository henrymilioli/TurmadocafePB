
using Cafeteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafeteria.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<CafeteriaC> CafeteriaC { get; set; }

        public DbSet<Avaliacao> Avaliacao { get; set; }

        public DbSet<Evento> Evento { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // para alterar configuracoes de relacionamento de entidades
            // tipo de delecao etc - chave estrangeira o EF ja faz automatico
            // so mexer se for preciso ok??
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Avaliacao>()
                .HasOne(a => a.Usuarios)
                .WithMany(u => u.Avaliacoes)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evento>()
              .HasOne(a => a.Usuarios)
              .WithMany(u => u.Eventos)
              .OnDelete(DeleteBehavior.NoAction);


        }

    }
}
