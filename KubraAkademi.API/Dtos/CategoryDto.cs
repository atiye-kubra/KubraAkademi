using KubraAkademi.API.Models;

namespace KubraAkademi.API.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int ExamId { get; set; }
        public string Videos { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
