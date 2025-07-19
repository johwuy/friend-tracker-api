using friend_tracker_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace friend_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactsController> _logging;
    public ContactsController(ApplicationDbContext context, ILogger<ContactsController> logging)
    {
        _context = context;
        _logging = logging;
    }

    [HttpGet]
    public async Task<ActionResult<Contact[]>> GetAllContacts()
    {
        return await _context.Contacts.ToArrayAsync();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContact(Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        if (contact is null)
        {
            return NotFound();
        }
        return Ok(contact);
    }
}