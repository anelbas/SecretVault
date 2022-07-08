using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.DTOs;
using SecretVaultAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using SecretVaultAPI.Utils;
using SecretVaultAPI.Adapter;

namespace SecretVaultAPI.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PostsController : Controller
    {

        public SecretVaultDBContext _context = new SecretVaultDBContext();
        public ForeignKeyObjectUtil _fkUtil = new ForeignKeyObjectUtil();
        public EncodingUtil _encodingUtil = new EncodingUtil();
        public ResponseAdapter _responseAdapter = new ResponseAdapter();

        [HttpGet]
        public IActionResult DetailsForAllPublicPosts()
        {
            List<Post> posts = _context.Posts.Where(item => item.PrivacyStatusId == 2).ToList();
            posts.ForEach(post => post.Content = _encodingUtil.Base64Decode(post.Content));
            List<PostDTO> postsDTO = new List<PostDTO>();
            posts.ForEach(post => postsDTO.Add(_responseAdapter.asDTO(post)));

            return Ok(postsDTO);
        }

        [HttpGet("user/{userId}")]
        public IActionResult DetailsForAllUserPosts(int? userId)
        {

            if (userId == null)
            {
                return BadRequest("Please provide a user id");
            }

            if(_context.Users.Find(userId) == null)
            {
                return NotFound("Please provide a valid user id");
            }

            List<Post> posts = _context.Posts.Where(item => item.UserId == userId).ToList();
            if(posts.Count == 0)
            {
                return Ok("No posts found for user");
            }
            posts.ForEach(post => post.Content = _encodingUtil.Base64Decode(post.Content));

            List<PostDTO> postsDTO = new List<PostDTO>();
            posts.ForEach(post => postsDTO.Add(_responseAdapter.asDTO(post)));

            return Ok(postsDTO);
        }

        // GET: Posts/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Please provide an id");
            }

            Post postToReturn = _context.Posts.Find(id);
            if (postToReturn == null)
            {
                return NotFound("Please provide a valid id");
            }

            postToReturn.Content = _encodingUtil.Base64Decode(postToReturn.Content);

            return Ok(_responseAdapter.asDTO(postToReturn));
        }

        //Encrypt the post
        [HttpPost]
        public IActionResult Create([FromBody] PostDTO request)
        {

            bool validRequest = request != null;
            validRequest |= (request.title != null);
            validRequest |= (request.content != null);
            validRequest |= (request.privacyStatus != null);
            validRequest |= (request.userId != 0);

            if (!validRequest)
            {
                return BadRequest("Please enter all the valid information");
            }

            User user = _context.Users.Find(request.userId);
            if(user == null)
            {
                return BadRequest("Please provide a valid user id");
            }

            Post newPost = new Post();

            newPost.Title = request.title;
            newPost.Content = _encodingUtil.Base64Encode(request.content);
            newPost.Timestamp = DateTime.Now;

            PrivacyStatus privacyObject = _fkUtil.getPrivacyStatus(request.privacyStatus);
            if(privacyObject == null)
            {
                return BadRequest("Please enter a valid privacy status");
            }
            newPost.PrivacyStatus = privacyObject;
            newPost.PrivacyStatusId = privacyObject.PrivacyStatusId;
            privacyObject.Posts.Add(newPost);
            newPost.UserId = request.userId;
            newPost.User = user;
            user.Posts.Add(newPost);

            _context.Posts.Update(newPost);
            _context.SaveChanges();

            return Ok(_responseAdapter.asDTO(newPost));
        }


        //Encrypt the post
        [HttpPut("{id}")]
        public IActionResult EditPut(int? id, [FromBody] PostDTO request)
        {
            if (id == null)
            {
                return BadRequest("Please provide a valid id");
            }

            bool validRequest = request != null;
            validRequest |= (request.title != null);
            validRequest |= (request.content != null);
            validRequest |= (request.privacyStatus != null);

            if (!validRequest)
            {
                return BadRequest("Please enter all the valid information");
            }

            Post postToEdit = _context.Posts.Find(id);
            if (postToEdit == null)
            {
                return NotFound("Please provide a valid id");
            }


            Post newPost = new Post();

            PrivacyStatus privacyObject = _fkUtil.getPrivacyStatus(request.privacyStatus);

            newPost.PostId = postToEdit.PostId;
            newPost.Title = request.title;
            newPost.Content = _encodingUtil.Base64Encode(request.content);
            newPost.Timestamp = DateTime.Now;

            newPost.PrivacyStatusId = privacyObject.PrivacyStatusId;
            newPost.PrivacyStatus = privacyObject;
            privacyObject.Posts.Add(newPost);

            newPost.UserId = postToEdit.UserId;
            newPost.User = _context.Users.Find(newPost.UserId);

            _context.Entry(postToEdit).CurrentValues.SetValues(newPost);
            _context.SaveChanges();


            return Ok(_responseAdapter.asDTO(newPost));
        }

        [HttpPatch("{id}")]
        public IActionResult EditPatch(int? id, [FromBody] PostDTO request)
        {
            if (id == null)
            {
                return BadRequest("Please provide a valid id");
            }


            Post postToEdit = _context.Posts.Find(id);
            if (postToEdit == null)
            {
                return NotFound("Please provide a valid id");
            }

            if(request == null)
            {
                return BadRequest("Please send a valid request");
            }

            if(request.privacyStatus != null)
            {
                PrivacyStatus privacyObject = _fkUtil.getPrivacyStatus(request.privacyStatus);
                if (privacyObject != null)
                {
                    postToEdit.PrivacyStatusId = privacyObject.PrivacyStatusId;
                    postToEdit.PrivacyStatus = privacyObject;
                    privacyObject.Posts.Add(postToEdit);
                }
            }
            

            postToEdit.Title = (request.title == null) ? postToEdit.Title : request.title;
            postToEdit.Content = (request.content == null) ? postToEdit.Content : _encodingUtil.Base64Encode(request.content);
            postToEdit.Timestamp = DateTime.Now;

            _context.Posts.Update(postToEdit);
            _context.SaveChanges();


            return Ok(_responseAdapter.asDTO(postToEdit));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Post postToDelete = _context.Posts.Find(id);

            try
            {
                _context.Posts.Remove(postToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error");
            }


            return Ok(_responseAdapter.asDTO(postToDelete));
        }


    }
}
