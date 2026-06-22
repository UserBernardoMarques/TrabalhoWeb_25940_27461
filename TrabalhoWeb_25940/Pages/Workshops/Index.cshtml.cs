using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrabalhoWeb_25940.Data;
using TrabalhoWeb_25940.Models;

namespace TrabalhoWeb_25940.Pages.Workshops
{
    public class IndexModel : PageModel
    {
        private readonly TrabalhoWeb_25940.Data.AppDbContext _context;

        public IndexModel(TrabalhoWeb_25940.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Workshop> Workshop { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Workshop = await _context.Workshops
                .Include(w => w.Categoria).ToListAsync();
        }
    }
}
