using AspNetCore.Identity.MongoDbCore.Models;
using Domain;
using MongoDbGenericRepository.Attributes;

namespace PrizeBondChecker.Domain
{
    [CollectionName("Users")]
    public class Users : MongoIdentityUser<Guid>, IBaseEntity
    {
        //public string Username { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        //public string PhoneNumber { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastUpdatedBy { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
    }
}
