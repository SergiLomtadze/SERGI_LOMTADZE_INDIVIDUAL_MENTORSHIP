namespace ExadelMentorship.BusinessLogic.Models
{
    public class AuthenticationModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
    }
}
