using friend_tracker_api.Model;
using Microsoft.AspNetCore.Mvc;

namespace friend_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    public sealed class NoteUpdateDTO
    {
        public required string Content { get; set; }
    }

    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactsController> _logging;
    public NotesController(ApplicationDbContext context, ILogger<ContactsController> logging)
    {
        this._context = context;
        this._logging = logging;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNote(Guid id)
    {
        var existing = await _context.Notes.FindAsync(id);

        if (existing is null)
        {
            var note = new Note
            {
                ContactId = id,
                Content = String.Empty
            };
            
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNote), new { id = note.ContactId }, existing);
        }
        return Ok(existing);
    }

    [HttpPut("{id}")]
    [HttpPost("{id}")]
    public async Task<IActionResult> UpdateNote(Guid id, [FromBody] NoteUpdateDTO dto) // Using DTO because client only needs to send content
    {
        var existing = await _context.Notes.FindAsync(id);

        if (existing is null)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact is null) return NotFound("Conact doesn't exist");
            var note = new Note
            {
                ContactId = id,
                Content = dto.Content,
            };
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetNote), new { id = note.ContactId }, existing);
        }
        existing.Content = dto.Content;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(Guid id)
    {
        var existing = await _context.Notes.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }
        _context.Notes.Remove(existing);
        await _context.SaveChangesAsync();
        return Ok(existing);
    }

}