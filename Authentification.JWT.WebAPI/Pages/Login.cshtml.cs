using Authentification.JWT.DAL.Models;
using Authentification.JWT.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Authentification.JWT.WebAPI._Pages
{
    public class LoginModel(IUserService userService) : PageModel
    {
        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        [BindProperty]
        public Login Login { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var user = await userService.AuthenticateAsync(Login.Username, Login.Password);

            if (user != null)
            {
                return RedirectToPage("/Index");
            }
            else
            {
                ErrorMessage = "username or password incorrect";
                return Page();
            }
        }

    }

}
