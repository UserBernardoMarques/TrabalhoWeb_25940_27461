using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace TrabalhoWeb_25940.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Limpa o esquema padrão atual do sistema (Cookies)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Tenta limpar o esquema antigo (CookieAuth) caso o browser do utilizador ainda o tenha guardado em memória
            try
            {
                await HttpContext.SignOutAsync("CookieAuth");
            }
            catch
            {
                // Ignora silenciosamente se o esquema legado não existir
            }

            // 3. Redireciona de volta para a página inicial
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Limpa o esquema padrão atual do sistema (Cookies)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Tenta limpar o esquema antigo (CookieAuth) caso o browser do utilizador ainda o tenha guardado em memória
            try
            {
                await HttpContext.SignOutAsync("CookieAuth");
            }
            catch
            {
                // Ignora silenciosamente se o esquema legado não existir
            }

            // 3. Redireciona de volta para a página inicial
            return RedirectToPage("/Index");
        }
    }
}