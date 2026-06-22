using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Workshop> Workshops { get; set; } = default!;
        public DbSet<Categoria> Categorias { get; set; } = default!;
        public DbSet<Participante> Participantes { get; set; } = default!;
        public DbSet<Inscricao> Inscricoes { get; set; } = default!; // Nova tabela adicionada aqui!

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}