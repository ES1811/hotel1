using Microsoft.AspNetCore.Mvc;
using hotel1.Model;
using Microsoft.EntityFrameworkCore;

namespace hotel1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CustomerController(AppDbContext context)
        {
            _context = context;
        }

        //  Create a new customer
        //---- json serialization issue
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            var hotel = await _context.Hotels.FindAsync(customer.HotelId);
            if (hotel == null)
                return NotFound("Hotel not found.");

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(customer);
        }

        //  Get all customers
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers
                // .Include(c => c.Hotel) --- does it need to show the entire hotel? lol
                // .Include(c => c.Room) -- this could be optional
                .ToListAsync();

            return Ok(customers);
        }

        //  Get a customer by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers
                // .Include(c => c.Hotel)
                // .Include(c => c.Room)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                return NotFound("Customer not found.");

            return Ok(customer);
        }
    }
}
