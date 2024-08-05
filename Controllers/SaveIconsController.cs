using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using save_changed_image_api.Models.Entities;

namespace SaveIcons.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IconsController : ControllerBase
    {
        private readonly IconsDbContext _context;
        private readonly ILogger<IconsController> _logger;

        public IconsController(IconsDbContext context, ILogger<IconsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("save-icons")]
        public async Task<IActionResult> SavePlacedIcons([FromBody] SaveIconsRequest request)
        {
            if (request.PlacedIcons == null || request.PlacedIcons.Count == 0)
                return BadRequest("No icons to save.");

            // Generate a new session ID
            string sessionId = Guid.NewGuid().ToString();

            // Set the session ID for all icons and add them to the database
            foreach (var icon in request.PlacedIcons)
            {
                icon.SessionId = sessionId;
                _context.PlacedIcons.Add(icon);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Icons saved successfully.", SessionId = sessionId });
        }

        [HttpGet("get-icons/{sessionId}")]
        public async Task<ActionResult<List<PlacedIcon>>> FetchPlacedIcons(string sessionId)
        {
            var placedIcons = await _context.PlacedIcons
                .Where(pi => pi.SessionId == sessionId)
                .ToListAsync();

            if (placedIcons == null || placedIcons.Count == 0)
            {
                return NotFound("No icons found for the provided session ID.");
            }

            return Ok(placedIcons);
        }
    }

    public class SaveIconsRequest
    {
        public List<PlacedIcon> PlacedIcons { get; set; }
    }
}