using gerenciadoracademico.services;
using GerenciadorAcademico.Data;
using GerenciadorAcademico.Repositories;
using GerenciadorAcademico.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços do MVC
builder.Services.AddControllersWithViews();

// ==========================================
// REGISTRO DA PERSISTÊNCIA EM JSON
// ==========================================

// Configuração dos arquivos locais na pasta Data
builder.Services.AddSingleton(sp => new JsonFileStore("projetos.json"));
builder.Services.AddSingleton(sp => new JsonFileStore("professores.json"));

// Registro das interfaces e classes de repositório
builder.Services.AddSingleton<IProjetoRepository, ProjetoJsonRepository>();
builder.Services.AddSingleton<IProfessorRepository, ProfessorJsonRepository>();

// Serviços da aplicação
builder.Services.AddSingleton<IProjetoService, ProjetoService>();
builder.Services.AddSingleton<IProfessorService, ProfessorService>();

var app = builder.Build();

// ==========================================
// CONFIGURAÇÃO DE ERROS
// ==========================================

// TEMPORARIAMENTE: mostra os detalhes do erro
app.UseDeveloperExceptionPage();

// HTTPS
app.UseHttpsRedirection();

// Arquivos estáticos
app.UseStaticFiles();

// Roteamento
app.UseRouting();

// Autorização
app.UseAuthorization();

// ==========================================
// ROTA PADRÃO
// ==========================================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
