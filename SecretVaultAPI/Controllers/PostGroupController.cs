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
            validRequest |= (request._postId != null);

            if (!validRequest)
            {
                return BadRequest();
            }

            PostGroup newGroupPost = new PostGroup();

            newGroupPost.GroupId = (int)request._groupId;
            newGroupPost.PostId = (int)request._postId;

            bool isPrivate = checkIfPostIsPrivate(newGroupPost.PostId);

            if (isPrivate)
            {
                return BadRequest();
            }
            
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

        [HttpDelete("/groupPost/{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            PostGroup groupPostToDelete = _context.PostGroups.Find(id);

            try
            {
                _context.PostGroups.Remove(groupPostToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


            return Ok(groupPostToDelete);
        }

        private bool checkIfPostIsPrivate(int postId)
        {
            int privacyStatus = 0;
            //get privacy status from post

            if(privacyStatus == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
