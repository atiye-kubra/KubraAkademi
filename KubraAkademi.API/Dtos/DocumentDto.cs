namespace KubraAkademi.API.Dtos
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DocUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
