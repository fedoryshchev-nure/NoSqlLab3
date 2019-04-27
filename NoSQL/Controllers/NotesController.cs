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
        public async Task<ActionResult<IEnumerable<User>>> Get(bool includeAll = false)
        {
            IEnumerable<User> users = includeAll ? 
                await userCollection.GetAllAsync() :
                new List<User> { await userManager.GetUserAsync(User) };

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post([FromBody]Note note)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            user.Notes.Add(note);
            await userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(string id, [FromBody]Note note)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            var noteToBeUpdated = user.Notes.FirstOrDefault(x => x.Id == id);
            noteToBeUpdated = note;

            await userManager.UpdateAsync(user);

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            User user = await userManager.GetUserAsync(HttpContext.User);
            var deleted = user.Notes.Remove(new Note { Id = id });

            if (deleted)
                return Ok();

            await userManager.UpdateAsync(user);

            return NoContent();
        }
    }
}
