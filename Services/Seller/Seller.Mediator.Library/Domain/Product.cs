using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Seller.Mediator.Library.Domain
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // [Required]
        // [StringLength(30, MinimumLength = 5, ErrorMessage = "Minimum should be 5 characters and max should be 30")]
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
        //public User UserDetails { get; set; }
        public string FirstName { get; set; }

        //[Required]
        // [StringLength(25, MinimumLength = 3, ErrorMessage = "Minimum should be 3 characters and max should be 25")]
        public string LastName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pin { get; set; }

        //[Required]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"((\+*)((0[ -]*)*|((91 )*))((\d{12})+|(\d{10})+))|\d{5}([- ]*)\d{6}", ErrorMessage = "Not a valid number")]
        public string Phone { get; set; }

        //[Required]
        // [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
    }
}
