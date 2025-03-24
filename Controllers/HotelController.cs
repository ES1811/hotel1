using Microsoft.AspNetCore.Mvc;
using hotel1.Model;
using Microsoft.EntityFrameworkCore;
using hotel1.DTOs;

namespace hotel1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HotelController(AppDbContext context)
        {
            _context = context;
        }

        //  Create a new hotel
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Hotel name is required.");
            if (request.RoomCount <= 0)
                return BadRequest("RoomCount must be greater than 0.");

            var hotel = new Hotel
            {
                Name = request.Name,
                Rooms = new List<Room>()
            };

            for (int i = 1; i <= request.RoomCount; i++)
            {
                hotel.Rooms.Add(new Room
                {
                    RoomNumber = i,
                    Availability = true
                });
            }

            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return Ok(hotel);
        }

        //  Get all hotels
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.ToListAsync();
            return Ok(hotels);
        }

        //  Get hotel by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            return Ok(hotel);
        }
    }
}
