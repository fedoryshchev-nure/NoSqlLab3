using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NoSQL.Entities;

namespace NoSQL.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class NotesController : Controller
    {
        private readonly UserManager<User> userManager;
        readonly IIdentityUserCollection<User> userCollection;

        public NotesController(UserManager<User> userManager,
            IIdentityUserCollection<User> userCollection)
        {
            this.userManager = userManager;
            this.userCollection = userCollection;
        }

        [HttpGet("{includeAll}")]
        public async Task<ActionResult<IEnumerable<Note>>> Get(bool includeAll = false)
        {
            IEnumerable<User> users = includeAll ? 
                await userCollection.GetAllAsync() :
                new List<User> { await userManager.GetUserAsync(User) };
            IEnumerable<Note> notes = users.SelectMany(x => x.Notes);

            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody]Note note)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            note.Id = Guid.NewGuid().ToString();
            user.Notes.Add(note);
            await userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, [FromBody]Note note)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            var noteToBeUpdated = user.Notes.FirstOrDefault(x => x.Id == id);
            noteToBeUpdated.Text = note.Text;

            await userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            var itemToBeRemoved = user.Notes.FirstOrDefault(x => x.Id == id);
            var deleted = user.Notes.Remove(itemToBeRemoved);

            if (!deleted)
                return NoContent();

            await userManager.UpdateAsync(user);

            return Ok();
        }
    }
}
