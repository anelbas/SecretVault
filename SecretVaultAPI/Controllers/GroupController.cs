using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.Model;
using System.Linq;
using SecretVaultAPI.DTOs;
using System;

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
        public IActionResult Create([FromBody] GroupDTO request)
        {

            bool validRequest = request != null;
            validRequest |= (request._createdBy != null);
            validRequest |= (request._groupName != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            Group newGroup = new Group();

            newGroup.CreatedBy = (int)request._createdBy;
            newGroup.GroupName = request._groupName;

            try
            {
                _context.Groups.Add(newGroup);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(newGroup);
        }

        // [HttpPost("group")]
        // public IActionResult AddUserToGroup([FromBody] GroupUserDTO request)
        // {
        //     bool validRequest = request != null;
        //     validRequest |= (request._groupId != null);
        //     validRequest |= (request._userId != null);

        //     if (!validRequest)
        //     {
        //         return BadRequest();
        //     }

        //     Group newGroupUser = new Group();

        //     try
        //     {
        //         _context.GroupUser.Add(newGroupUser);
        //         _context.SaveChanges();
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //     }


        //     return Ok(newGroupUser);
        // }

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

            newGroup.GroupId = groupToEdit.GroupId;
            newGroup.CreatedBy = (int)request._createdBy;
            newGroup.GroupName = request._groupName;

            try
            {
                _context.Update(newGroup);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            

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

            groupToEdit.CreatedBy = (int)request._createdBy;
            groupToEdit.GroupName = request._groupName;

            try
            {
                _context.Groups.Update(groupToEdit);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


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

            try
            {
                _context.Groups.Remove(groupToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(groupToDelete);
        }
    }
}
