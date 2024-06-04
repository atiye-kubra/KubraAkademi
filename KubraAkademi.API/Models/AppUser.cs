using Microsoft.AspNetCore.Identity;

namespace KubraAkademi.API.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    } 
    //sadece videoyu yükleyen kişi için adı soy adı alındığından bu alan eklendi
}
