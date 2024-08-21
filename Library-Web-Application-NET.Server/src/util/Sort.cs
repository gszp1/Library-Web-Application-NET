namespace Library_Web_Application_NET.Server.src.util
{
    public class Sort
    {
        public string Field { get; set; }

        public bool Descending { get; set; }

        public Sort(string field, bool descending)
        {
            this.Field = field;
            this.Descending = descending;
        }
    }
}
