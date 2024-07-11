namespace Library_Web_Application_NET.Server.src.model
{
    public class Publisher
    {
        public int PublisherId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public List<Resource> Resources { get; set; } = [];
    }
}
