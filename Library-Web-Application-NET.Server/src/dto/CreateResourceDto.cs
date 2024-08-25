namespace Library_Web_Application_NET.Server.src.dto
{
    public class CreateResourceDto
    {
        public string Title { get; set; }

        public string Identifier { get; set; }

        public string Description { get; set; }

        public string Publisher { get; set; }

        public List<string> Authors { get; set; }
    }
}
