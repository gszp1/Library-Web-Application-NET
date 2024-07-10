namespace Library_Web_Application_NET.Server.src.model
{
    public class AuthorResource
    {
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; }
    }
}
