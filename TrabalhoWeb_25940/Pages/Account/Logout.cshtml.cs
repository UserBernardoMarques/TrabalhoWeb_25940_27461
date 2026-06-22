using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TrabalhoWeb_25940.Pages.Account
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            // A CORREÇÃO ESTÁ AQUI: Usar exatamente o mesmo nome que está registado no teu sistema!
            await HttpContext.SignOutAsync("CookieAuth");

            // Redireciona para a página principal
            return RedirectToPage("/Index");
        }
    }
}