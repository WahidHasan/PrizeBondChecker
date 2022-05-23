using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace PrizeBondChecker.Domain
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityUser<Guid>
    {
    }
}
