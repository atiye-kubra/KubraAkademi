using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KubraAkademi.API.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public string Description { get; set; }
        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual ICollection<Video>? Videos { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
