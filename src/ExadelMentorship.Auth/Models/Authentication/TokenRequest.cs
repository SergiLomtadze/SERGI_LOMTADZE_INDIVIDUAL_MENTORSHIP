using System.ComponentModel.DataAnnotations;

namespace ExadelMentorship.Auth.Models
{
    public class TokenRequest
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
