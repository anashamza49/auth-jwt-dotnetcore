using System.ComponentModel.DataAnnotations;

namespace Authentification.JWT.Service.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Le nom d'utilisateur est requis")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
