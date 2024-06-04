namespace KubraAkademi.UI.Models.List
{
    public class ListIndexViewModel
    {
        public string Exam { get; set; }
        public string Category { get; set; }
    }

    public class ListVideoViewModel
    {
        public Guid VideoId { get; set; }
    }

    public class CoursePreviewDto
    {
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DateString { get; set; }
    }
}
