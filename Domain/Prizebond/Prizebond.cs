using Domain;
using MongoDB.Bson.Serialization.Attributes;
using static PrizeBondChecker.Domain.Enums.EnumPrizebond;

namespace PrizeBondChecker.Domain.Prizebond

{
    [BsonIgnoreExtraElements]
    public class Prizebond : BaseEntity
    {
        //[BsonId]
        //[BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        //public string? Id { get; set; }
        public string? serial { get; set; }

        public string? bondId { get; set; }
        public DateTime? entryDate { get; set; }
        public CheckBond? Checked { get; set; }
    }
}
