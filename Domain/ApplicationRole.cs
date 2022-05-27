using AspNetCore.Identity.MongoDbCore.Models;
using Domain;
using MongoDbGenericRepository.Attributes;

namespace PrizeBondChecker.Domain
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid>
    {
    }
}
