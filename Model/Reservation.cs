using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace hotel1.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer? Customer { get; set; }

        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        [JsonIgnore]
        public Room? Room { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
