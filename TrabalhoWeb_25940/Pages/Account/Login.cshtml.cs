using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var utilizador = await _context.Participantes
                .FirstOrDefaultAsync(p => p.Email == Input.Email && p.Password == Input.Password);

            if (utilizador == null)
            {
                ModelState.AddModelError(string.Empty, "Email ou palavra-passe incorretos.");
                return Page();
            }

            if (utilizador.Aprovado == false && utilizador.Email != "admin@gather.io")
            {
                ModelState.AddModelError(string.Empty, "A tua conta ainda está a aguardar aprovação da administração. Tenta mais tarde.");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, utilizador.Nome),
                new Claim(ClaimTypes.Email, utilizador.Email),
                new Claim("ParticipanteId", utilizador.Id.ToString())
            };

            if (utilizador.Email == "admin@gather.io")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrador"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "Utilizador"));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToPage("/Index");
        }
    }
}