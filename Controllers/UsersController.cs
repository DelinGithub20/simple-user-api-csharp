using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.Data;
using SimpleApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly ApiDbContext _context;

    public UsersController(ApiDbContext context)
    {
        _context = context;
    }

    // --- 1. GET /api/users (Menampilkan daftar pengguna) ---
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        
        return await _context.Users.ToListAsync();
    }

    // --- 2. GET /api/users/{id} (Menampilkan detail pengguna) ---
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
        
            return NotFound(new 
            { 
                status = 404,
                message = $"Pengguna dengan ID {id} tidak ditemukan." 
            });
        }

        
        return user;
    }

    // --- 3. POST /api/users (Menambah pengguna baru) ---
    [HttpPost]
    public async Task<ActionResult<User>> PostUser([FromBody] User user)
    {
       
        if (!ModelState.IsValid)
        {
           
            return BadRequest(ModelState);
        }
        
        try 
        {
          
            _context.Users.Add(user);
            
           
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (DbUpdateException)
        {

            return StatusCode(500, new { message = "Gagal menyimpan data ke database." });
        }
    }
}
