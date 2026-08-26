using gerenciadoracademico.services;
using GerenciadorAcademico.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços para o padrão MVC (Controllers com Views)
builder.Services.AddControllersWithViews();

// ==========================================
// REGISTRO DOS SERVIÇOS EM MEMÓRIA (SINGLETON)
// ==========================================
builder.Services.AddSingleton<IProjetoService, ProjetoService>();
builder.Services.AddSingleton<IProfessorService, ProfessorService>();

var app = builder.Build();

// Configura o pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // O valor padrão do HSTS é de 30 dias. Você pode querer mudar isso para cenários de produção.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configura a rota padrão do sistema (Inicia na tela de Login do HomeController)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
