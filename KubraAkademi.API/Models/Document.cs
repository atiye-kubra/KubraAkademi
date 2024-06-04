namespace KubraAkademi.API.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

    }
}
