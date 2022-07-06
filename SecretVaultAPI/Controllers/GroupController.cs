using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.Model;
using System.Linq;
using SecretVaultAPI.DTOs;

namespace SecretVaultAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : Controller
    {

        public SecretVaultDBContext _context = new SecretVaultDBContext();

        [HttpGet("group/{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Group groupToReturn = _context.Groups.Find(id);
            if(groupToReturn == null)
            {
                return NotFound();
            }

            return Ok(groupToReturn);
        }

        [HttpGet("group")]
        public IActionResult DetailsForAllGroups()
        {
            return Ok(_context.Groups.ToList());
        }

        [HttpPost("/group")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPut("group/{id}")]
        public IActionResult EditPut(int? id, [FromBody] GroupDTO request)
        {
            if(id == null)
            {
                return BadRequest();
            }

            bool validRequest = request != null;
            validRequest |= (request._createdBy != null);
            validRequest |= (request._groupName != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            Group groupToEdit = _context.Groups.Find(id);
            if(groupToEdit == null)
            {
                return NotFound();
            }


            Group newGroup = new Group();

            newGroup.CreatedBy = request._createdBy;
            newGroup.GroupName = request._groupName;

            _context.Entry(groupToEdit).CurrentValues.SetValues(newGroup);
            _context.SaveChanges();
            

            return Ok(newGroup);
        }

        [HttpPatch("group/{id}")]
        public IActionResult EditPatch(int? id, [FromBody] GroupDTO request)
        {
            if (id == null)
            {
                return BadRequest();
            }

            bool validRequest = request != null;
            validRequest &= (request._createdBy != null);
            validRequest &= (request._groupName != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            Group groupToEdit = _context.Groups.Find(id);
            if (groupToEdit == null)
            {
                return NotFound();
            }

            groupToEdit.CreatedBy = request._createdBy;
            groupToEdit.GroupName = request._groupName;

            _context.Groups.Update(groupToEdit);
            _context.SaveChanges();


            return Ok(groupToEdit);
        }

        [HttpDelete("group/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Group groupToDelete = _context.Groups.Find(id);

            _context.Groups.Remove(groupToDelete);
            _context.SaveChanges();


            return Ok(groupToDelete);
        }
    }
}
