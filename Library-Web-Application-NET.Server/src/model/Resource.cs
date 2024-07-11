namespace Library_Web_Application_NET.Server.src.model
{
    public class Resource
    {
        public int ResourceId { get; set; }
    
        public string Title { get; set; }

        public string Identifier { get; set; }

        public string? Descripiton { get; set; }

        public string? ImageUrl { get; set; }

        public List<ResourceInstance> Instances { get; set; } = [];

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public List<Author> Authors { get; } = [];
    }
}
