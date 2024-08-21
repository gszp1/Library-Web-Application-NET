namespace Library_Web_Application_NET.Server.src.statistics
{
    public class ResourceStatisticsDto
    {
        public long NumberOfResources { get; set; }

        public long NumberOfInstances { get; set; }

        public long BorrowedInstances { get; set; }

        public long ReservedInstances { get; set; }
    }
}
