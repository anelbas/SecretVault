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
    public class PostGroupController : Controller
    {

        public SecretVaultDBContext _context = new SecretVaultDBContext();

        [HttpPost("/groupPost")]
        public IActionResult AddPostToGroup([FromBody] PostGroupDTO request)
        {

            bool validRequest = request != null;
            validRequest |= (request._groupId != null);
            validRequest |= (request.PostId != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            GroupUser newGroupPost = new GroupUser();

            newGroupPost.GroupId = (int)request._groupId;
            newGroupPost.PostId = (int)request._postId;

            try
            {
                _context.PostGroups.Add(newGroupPost);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(newGroupPost);
        }

    }
}
