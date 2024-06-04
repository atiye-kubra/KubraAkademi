namespace KubraAkademi.API.Dtos
{
    public class UpdateVideoDto
    {
        public string Title { get; set; }
        public string EmbedUrl { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
