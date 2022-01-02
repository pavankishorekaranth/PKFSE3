using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Buyer.API.Entity
{
    public class Bid
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BidId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Minimum should be 5 characters and max should be 30")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Minimum should be 3 characters and max should be 25")]
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"((\+*)((0[ -]*)*|((91 )*))((\d{12})+|(\d{10})+))|\d{5}([- ]*)\d{6}", ErrorMessage = "Not a valid number")]
        public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        public decimal BidAmount { get; set; }
        public string ProductId { get; set; }
    }
}
