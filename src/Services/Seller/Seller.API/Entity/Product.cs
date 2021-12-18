using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Seller.API.Entity
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Minimum should be 5 characters and max should be 30")]
        public string ProductName { get; set; }
        public string ShortDescription { get; set; }
        public string DetailedDescription { get; set; }
        public int StartingPrice { get; set; }
        public DateTime BidEndDate { get; set; }
    }
}
