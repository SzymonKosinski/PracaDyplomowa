using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PracaDyplomowa.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace PracaDyplomowa.Pages
{
    public class AdminModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IEnumerable<IdentityUser> AspNetUsers { get; set; }
        public string selectedValue { get; set; }
        public AdminModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        { 
            AspNetUsers = _context.AspNetUsers;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string id)
        {
            IdentityUserRole<String> role=new IdentityUserRole<string>();
            role.UserId = id;
            role.RoleId = "2";
            //IdentityUser user = (IdentityUser)AspNetUsers.Where(x => x.Id == id);
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
                
            }
            else
                return NotFound();
        }
    }
}
