namespace ExadelMentorship.DataAccess.Entities
{
    public class ReportUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Cities { get; set; }
        public int Period { get; set; }

    }
}
