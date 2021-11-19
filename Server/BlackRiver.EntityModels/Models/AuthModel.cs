using System.ComponentModel.DataAnnotations;

namespace BlackRiver.EntityModels
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
