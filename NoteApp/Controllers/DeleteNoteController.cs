using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteApp.App_Data;
using NoteApp.Models;


namespace NoteApp.Controllers
{
    [Route("Home/DeleteNote")]
    public class DeleteNoteController : Controller
    {
        [HttpGet("{note_id}")]
        [Authorize]
        public IActionResult DeleteNote(int note_id)
        {
            // Удаляем заметку по её ID
            using (var cxt = new AuthDbContext())
            {
                Note nt = new Note() { Id = note_id };

                cxt.Notes.Remove(nt);
                cxt.SaveChanges();
            }

            return Redirect("~/Home");
        }
    }
}
