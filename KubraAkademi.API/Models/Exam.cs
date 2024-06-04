using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KubraAkademi.API.Models
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
