using Authentification.JWT.Service.DTOs;
using Authentification.JWT.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Authentification.JWT.WebAPI._Pages
{
    public class RegisterModel(IUserService userService) : PageModel
    {
        [BindProperty]
        public RegisterDto Register { get; set; }

        public string? ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var user = await userService.RegisterUserAsync(Register.Username, Register.Email, Register.Password);
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
