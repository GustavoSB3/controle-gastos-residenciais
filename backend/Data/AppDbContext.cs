using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Transacao> Transacoes => Set<Transacao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().Property(p => p.Nome).HasMaxLength(120).IsRequired();
        modelBuilder.Entity<Transacao>().Property(t => t.Descricao).HasMaxLength(200).IsRequired();
        modelBuilder.Entity<Transacao>().Property(t => t.Valor).HasPrecision(18, 2);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            // Garante que as transações sejam removidas junto com a pessoa vinculada.
            .OnDelete(DeleteBehavior.Cascade);
    }
}
