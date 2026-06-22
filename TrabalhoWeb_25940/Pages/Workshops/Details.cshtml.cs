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
    public class DetailsModel : PageModel
    {
        private readonly TrabalhoWeb_25940.Data.AppDbContext _context;

        public DetailsModel(TrabalhoWeb_25940.Data.AppDbContext context)
        {
            _context = context;
        }

        public Workshop Workshop { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workshop = await _context.Workshops.FirstOrDefaultAsync(m => m.Id == id);

            if (workshop is not null)
            {
                Workshop = workshop;

                return Page();
            }

            return NotFound();
        }
    }
}
