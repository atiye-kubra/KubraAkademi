namespace KubraAkademi.API.Helper
{
    public static class PathHelper
    {
        public static string ConvertToUrl(string data)
        {
            data = data.Replace(",", "").Replace("\"", "").Replace("'", "").Replace(":", "").Replace(";", "").Replace(".", "").Replace("!", "").Replace("?", "").Replace(")", "").Replace("(", " ").Replace("&", " ").Replace(" ", " "); if (data.Length > 75)
            {
                data = data.Substring(0, data.LastIndexOf(" "));
            }
            data = data.Replace(" ", "-").ToLower();
            return data.Replace("ş", "s").Replace("Ş", "s").Replace("ç", "c").Replace("Ç", "c").Replace("ö", "o").Replace("Ö", "o").Replace("ü", "u").Replace("Ü", "u").Replace("İ", "i").Replace("ı", "i").Replace("ğ", "g").Replace("Ğ", "g");
        }
    }
}


// bu sınıf, verilen bir metni URL uyumlu hale getirerek, web uygulamasında URL oluşturmak için kullanılır. Özellikle kullanıcı tarafından girilen veya veritabanından alınan metinlerin URL'ye uygun hale getirilmesi gerektiğinde kullanılır