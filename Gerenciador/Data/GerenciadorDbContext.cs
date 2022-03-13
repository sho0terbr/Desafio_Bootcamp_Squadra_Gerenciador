using Gerenciador.Models;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador.Context
{
    public class GerenciadorDbContext : DbContext
    {
        public GerenciadorDbContext(DbContextOptions<GerenciadorDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserModel>(entity =>
            {
                entity.HasIndex(e => e.Usuario).IsUnique();
            });
            builder.Entity<CursoModel>(entity =>
            {
                entity.HasIndex(e => e.Titulo).IsUnique();
            });
            builder.Entity<UserModel>().Property(c => c.Usuario).HasMaxLength(20).IsRequired(true);
            builder.Entity<UserModel>().Property(c => c.Senha).HasMaxLength(16).IsRequired(true);
            builder.Entity<CursoModel>().Property(c => c.Titulo).HasMaxLength(30).IsRequired(true);
            builder.Entity<CursoModel>().Property(c => c.Duracao).HasMaxLength(20).IsRequired(true);
        }

        public DbSet<CursoModel> Cursos { get; set; }
        public DbSet<UserModel> Users { get; set; }

    }
}
