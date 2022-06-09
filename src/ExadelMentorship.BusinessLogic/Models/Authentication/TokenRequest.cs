using System.ComponentModel.DataAnnotations;

namespace ExadelMentorship.BusinessLogic.Models
{
    public class TokenRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
