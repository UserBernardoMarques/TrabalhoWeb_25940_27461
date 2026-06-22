using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. ATIVAR RAZOR PAGES E OS CONTROLLERS DA API
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Obrigatório para a API funcionar

// 2. CONFIGURAR A CONEXÃO À BASE DE DADOS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Server=(localdb)\\mssqllocaldb;Database=TrabalhoWeb_25940;Trusted_Connection=True;MultipleActiveResultSets=true"));

// 3. CONFIGURAR O SISTEMA DE COOKIEAUTH
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", options =>
    {
        options.LoginPath = "/Account/Login";
    });

var app = builder.Build();

// 4. EXECUTAR O SEED DATA AUTOMÁTICO SESSÃO APÓS LIGAR
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configuração do Pipeline do Servidor
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// 5. MAPEAR AS ROTAS DO SITE E DA API
app.MapRazorPages();
app.MapControllers(); // Obrigatório para mapear as rotas da API

app.Run();