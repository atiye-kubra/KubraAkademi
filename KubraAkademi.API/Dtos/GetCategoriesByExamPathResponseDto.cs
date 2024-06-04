using KubraAkademi.API.Models;

namespace KubraAkademi.API.Dtos
{
    public class GetCategoriesByExamPathResponseDto
    {
        public List<Video>? Videos { get; set; }
        public string ExamTitle { get; set; }
        public string CategoryTitle { get; set; }
        public string Description { get; set; }
    }
}
//istemci uygulamasına sunucudan alınan verileri düzenli bir şekilde aktarmak için kullanılır. İstemci uygulaması bu sınıfı kullanarak sunucudan alınan verileri işleyebilir ve görüntüleyebilir.






