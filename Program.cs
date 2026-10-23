using Microsoft.EntityFrameworkCore;
using GerenciadorAcademico.Data;
using GerenciadorAcademico.Models;
using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.Services; // Caso sua pasta use 's' minúsculo, o Visual Studio ajusta automaticamente com o 'using' correto

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do MVC
builder.Services.AddControllersWithViews();

// Configuração da Connection String do SQL Server
var connectionString = builder.Configuration.GetConnectionString("AcademicoConnection")
    ?? throw new InvalidOperationException("A conexão AcademicoConnection não foi configurada.");

builder.Services.AddDbContext<AcademicoContext>(options =>
    options.UseSqlServer(connectionString));

// Registro dos Repositórios e Serviços (Scoped por conta do EF Core)
builder.Services.AddScoped<IProfessorRepository, ProfessorRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
