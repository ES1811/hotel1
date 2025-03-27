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
        //-------- it shouldn't be using CreateHotelRequest DTO to create the hotel in the first place
        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] /*CreateHotelRequest*/Hotel request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Hotel name is required.");
            if (request.Rooms == null)
                return BadRequest("RoomCount must be greater than 0.");

            var hotel = new Hotel
            {
                Name = request.Name,
                Rooms = new List<Room>()
            };

            for (int i = 1; i <= request.Rooms.Count; i++)
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
        //------ because of the incorrect Hotel creation, it also doesn't show rooms or customers
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            var hotels = await _context.Hotels.Include(c => c.Customers).Include(r => r.Rooms).ToListAsync(); //-----include rooms and customers too
            return Ok(hotels);
        }

        //  Get hotel by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel = await _context.Hotels.Include(c=>c.Customers).Include(r=>r.Rooms).FirstOrDefaultAsync(h => h.Id == id);
            if (hotel == null)
                return NotFound("Hotel not found.");

            return Ok(hotel);
        }
    }
}
