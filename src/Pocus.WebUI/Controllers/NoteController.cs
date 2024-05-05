using Microsoft.AspNetCore.Mvc;
using Pocus.Application.Dto;
using Pocus.Application.Interfaces;
using Pocus.WebUI.Models;
using System.Security.Claims;

namespace Pocus.WebUI.Controllers
{
    public class NoteController(ILogger<NoteController> logger,
                                IHttpContextAccessor httpContextAccessor,
                                INoteService noteService) : Controller
    {
        private readonly ILogger<NoteController> _logger = logger;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly INoteService _noteService = noteService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            var model = new NoteModel
            {
                Notes = await _noteService.GetNotArchived(userId)
            };

            return View("~/Views/Note/Notes.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetNoteById([FromQuery] int id)
        {
            NoteViewDto note = await _noteService.GetById(id);
            if (note != null)
            {
                return Ok(new { message = "Note data get successful", note = note });
            }
            else
            {
                return Ok(new { message = "There are no such Note" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNote([FromBody] NoteAddDto note)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? throw new InvalidOperationException("User not found");

            try
            {
                note.UserId = userId;

                await _noteService.Create(note);
                return Ok(new { message = "Note successfully added" });
            }
            catch
            {
                return BadRequest(new { message = "Error adding Note" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CopyNote([FromBody] ObjectRequest request)
        {
            try
            {
                await _noteService.Copy(request.Id);

                return Ok(new { message = "Note copy successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note copy failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateNote([FromBody] NoteUpdateDto request)
        {
            try
            {
                await _noteService.Update(request);

                return Ok(new { message = "Note update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note update failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateNotePin([FromBody] ObjectRequest request)
        {
            try
            {
                await _noteService.UpdatePinnedStatus(request.Id);

                return Ok(new { message = "Note pin update successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note pin update failed" });
            }
        }

        [HttpPatch]
        public async Task<IActionResult> ArchiveNote([FromBody] ObjectRequest request)
        {
            try
            {
                await _noteService.UpdateArchivedStatus(request.Id);

                return Ok(new { message = "Note archive successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note archive failed" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote([FromBody] ObjectRequest request)
        {
            try
            {
                await _noteService.Delete(request.Id);

                return Ok(new { message = "Note deleted successfully" });
            }
            catch
            {
                return BadRequest(new { message = "Note delete failed" });
            }
        }
    }
}
