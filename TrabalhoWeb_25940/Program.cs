using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Hubs;

var builder = WebApplication.CreateBuilder(args);

// 1. Adicionar os serviços base do projeto
builder.Services.AddRazorPages();
builder.Services.AddControllers();

// 2. Adicionar o SignalR
builder.Services.AddSignalR();

// 3. Configurar a Base de Dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🛡️ 4. O NOVO SISTEMA DE LOGIN (Super leve e sem problemas com a BD!)
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
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

// 7. O CADEADO PRINCIPAL 
app.UseAuthentication();
app.UseAuthorization();

// 8. Mapear todas as rotas
app.MapRazorPages();
app.MapControllers();
app.MapHub<WorkshopHub>("/workshopHub");

app.Run();