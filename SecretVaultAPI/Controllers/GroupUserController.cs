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
    public class GroupUserController : Controller
    {

        public SecretVaultDBContext _context = new SecretVaultDBContext();

        [HttpPost("/groupUser")]
        public IActionResult AddUserToGroup([FromBody] GroupUserDTO request)
        {

            bool validRequest = request != null;
            validRequest |= (request._groupId != null);
            validRequest |= (request._usrId != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            GroupUser newGroupUser = new GroupUser();

            newGroupUser.GroupId = (int)request._groupId;
            newGroupUser.UsrId = (int)request._usrId;

            try
            {
                _context.GroupUsers.Add(newGroupUser);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(newGroupUser);
        }

        [HttpDelete("/groupUser/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            GroupUser groupUserToDelete = _context.GroupUsers.Find(id);

            try
            {
                _context.GroupUsers.Remove(groupUserToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(groupUserToDelete);
        }
    }
}
