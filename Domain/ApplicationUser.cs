using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace PrizeBondChecker.Domain
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        //public string Username { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        //public string PhoneNumber { get; set; }
    }
}
