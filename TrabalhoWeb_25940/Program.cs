using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Hubs;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar os serviços base do projeto
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// 2. Adicionar o SignalR (Serviço do motor de tempo real)
builder.Services.AddSignalR();

// 3. Configurar a Base de Dados SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🛡️ 4. Sistema de Autenticação (Cookies)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
    });

// 5. Configurar os serviços do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 6. Configurar o pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gather.io API V1");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔧 CORREÇÃO PARA A AZURE: Ativar explicitamente o suporte de WebSockets no pipeline antes da segurança
app.UseWebSockets();

// 7. O CADEADO PRINCIPAL (Autenticação e Autorização)
app.UseAuthentication();
app.UseAuthorization();

// 8. Mapear todas as rotas da aplicação
app.MapRazorPages();
app.MapControllers();

// Mapeamento do Hub do SignalR (Rota idêntica à do site.js)
app.MapHub<WorkshopHub>("/workshopHub");

app.Run();