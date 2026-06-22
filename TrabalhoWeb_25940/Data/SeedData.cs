using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // 1. Garante que a Base de Dados é criada
                context.Database.EnsureCreated();

                // 2. CRIA AS CATEGORIAS OBRIGATÓRIAS (Se a tabela estiver vazia)
                if (!context.Categorias.Any())
                {
                    context.Categorias.AddRange(
                        new Categoria { Nome = "Tecnologia & Programação" },
                        new Categoria { Nome = "Design & Multimédia" },
                        new Categoria { Nome = "Negócios & Marketing" }
                    );
                    context.SaveChanges();
                }

                // 3. CRIA A CONTA DO ADMIN OBRIGATÓRIA (Se não existir)
                if (!context.Participantes.Any(p => p.Email == "admin@gather.io"))
                {
                    context.Participantes.Add(
                        new Participante
                        {
                            Nome = "Administrador do Sistema",
                            Email = "admin@gather.io",
                            Idade = 25,
                            Password = "admin", // Password padrão para os testes do professor
                            IsAluno = true,
                            IsFormador = true,
                            Aprovado = true // O Admin já nasce aprovado e ativo!
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}