using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KubraAkademi.API.Models
{
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string EmbedUrl { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public bool IsActive { get; set; }
        [NotMapped]
        public string CreateDateStr
        {
            get { return CreateDate.ToString("dd MMMM yyyy", new System.Globalization.CultureInfo("tr-TR")); }
            set
            {
                if (DateTime.TryParse(value, out DateTime result))
                {
                    CreateDate = result;
                }
            }
        }
    }
}
