using friend_tracker_api.Model;
using Microsoft.AspNetCore.JsonPatch;
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

    [HttpPost]
    public async Task<ActionResult<Contact>> AddContact(Contact contact)
    {
        await _context.Contacts.AddAsync(contact);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(Guid id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact is null)
        {
            return NotFound();
        }
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();
        return Ok(contact);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchContact(Guid id, JsonPatchDocument<Contact> patchDoc)
    {
        if (patchDoc is null)
        {
            return BadRequest(ModelState);
        }

        var contact = await _context.Contacts.FindAsync(id);

        if (contact is null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(contact, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _context.Contacts.Update(contact);
        await _context.SaveChangesAsync();
        return AcceptedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }
}