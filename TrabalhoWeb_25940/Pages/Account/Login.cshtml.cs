using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies; // <-- 1. Adicionado para o Login funcionar
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;

namespace TrabalhoWeb_25940.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
            [Required(ErrorMessage = "O Email é obrigatório.")]
            [EmailAddress(ErrorMessage = "Email inválido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A Password é obrigatória.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        // 2. Adicionado o "returnUrl" para ele saber de onde o utilizador foi expulso
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Procurar o utilizador na Base de Dados (A TUA LÓGICA EXCELENTE!)
            var utilizador = await _context.Participantes
                .FirstOrDefaultAsync(p => p.Email == Input.Email && p.Password == Input.Password);

            if (utilizador == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou palavra-passe incorretos.");
                return Page();
            }

            // 1.5 REGRA DE APROVAÇÃO: Se a conta ainda não estiver aprovada pelo Admin
            if (utilizador.Aprovado == false && utilizador.Email != "admin@gather.io")
            {
                ModelState.AddModelError(string.Empty, "A tua conta ainda está a aguardar aprovação da administração. Tenta mais tarde.");
                return Page();
            }

            // 2. Criar a "Identidade" do utilizador (O Crachá virtual)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilizador.Nome),
                new Claim(ClaimTypes.Email, utilizador.Email),
                new Claim("ParticipanteId", utilizador.Id.ToString())
            };

            // 3. REGRA DE ADMINISTRADOR
            if (utilizador.Email == "admin@gather.io")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Utilizador"));
            }

            // 4. A CORREÇÃO: Substituir "CookieAuth" pelo nome oficial do Program.cs
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // 5. Fazer o Login efetivo
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            // 6. Redirecionar inteligentemente de volta (ex: para o Create) se houver um link pendente
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index");
        }
    }
}