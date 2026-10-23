using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace GerenciadorAcademico.Data;

// Esta classe serve apenas para o Console conseguir gerar as Migrations sem depender do appsettings.json
public class AcademicoContextFactory : IDesignTimeDbContextFactory<AcademicoContext>
{
    public AcademicoContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AcademicoContext>();

        // Define a mesma string de conexão que usamos para o SQL Server local
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GerenciadorAcademicoDb;Trusted_Connection=True;");

        return new AcademicoContext(optionsBuilder.Options);
    }
}
