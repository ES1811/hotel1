using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace hotel1.Model
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [JsonIgnore]
        public List<Room> Rooms { get; set; } = new List<Room>();
        [JsonIgnore]
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }
}
