using friend_tracker_api.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace friend_tracker_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InteractionsController : ControllerBase
{
    public sealed class InteractionGetDto
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
        public required DateTime Date { get; set; }
    }
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactsController> _logging;
    public InteractionsController(ApplicationDbContext context, ILogger<ContactsController> logging)
    {
        _context = context;
        _logging = logging;
    }
}