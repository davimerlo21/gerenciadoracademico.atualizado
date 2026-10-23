using GerenciadorAcademico.Models;

using Microsoft.EntityFrameworkCore;

namespace GerenciadorAcademico.Data;

public class AcademicoContext : DbContext
{
    public AcademicoContext(DbContextOptions<AcademicoContext> options)
        : base(options)
    {
    }

    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Projeto> Projetos => Set<Projeto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.ToTable("Professores");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Projeto>(entity =>
        {
            entity.ToTable("Projetos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Titulo)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.Descricao)
                .IsRequired(false)
                .HasMaxLength(1000);

            entity.Property(p => p.Area)
                .IsRequired(false)
                .HasMaxLength(80);

            // Configura a coluna de data no SQL Server
            entity.Property(p => p.DataEntrega)
                .IsRequired();

            // Relacionamento 1:N com Professor
            entity.HasOne(p => p.Professor)
                .WithMany(p => p.Projetos)
                .HasForeignKey(p => p.ProfessorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
