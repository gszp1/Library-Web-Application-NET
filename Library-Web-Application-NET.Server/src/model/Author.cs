namespace Library_Web_Application_NET.Server.src.model
{
    public class Author
    {
        public int? AuthorId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public List<Resource> Resources { get; set; } = [];
    }
}
