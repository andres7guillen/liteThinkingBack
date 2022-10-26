using System.ComponentModel.DataAnnotations;

namespace CompanyManagerAPI.Models.Security
{
    public class UserCredentialsModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }        
    }
}
