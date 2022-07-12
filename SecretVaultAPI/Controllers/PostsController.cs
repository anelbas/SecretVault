using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.DTOs;
using SecretVaultAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using SecretVaultAPI.Utils;
using SecretVaultAPI.Adapter;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

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
        public AuthUtil _authUtil = new AuthUtil();

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
        [HttpGet]
        public IActionResult DetailsForAllPublicPosts()
        {
            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, ""))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            List<Post> posts = _context.Posts.Where(item => item.PrivacyStatusId == 2).ToList();
            posts.ForEach(post => post.Content = _encodingUtil.Base64Decode(post.Content));
            List<PostDTO> postsDTO = new List<PostDTO>();

            try
            {
                posts.ForEach(post => postsDTO.Add(_responseAdapter.asDTO(post)));
            }
            catch
            {
                return StatusCode(500, "Unable to fetch posts from database.");
            }

            return Ok(postsDTO);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
        [HttpGet("user/{userId}")]
        public IActionResult DetailsForAllUserPosts(string userId)
        {

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Please provide a user id");
            }

            if(_context.Users.Find(userId) == null)
            {
                return NotFound("Please provide a valid user id");
            }

            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, userId))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            List<Post> posts = _context.Posts.Where(item => item.UserId == userId).ToList();
            if(posts.Count == 0)
            {
                return Ok("No posts found for user");
            }
            posts.ForEach(post => post.Content = _encodingUtil.Base64Decode(post.Content));

            List<PostDTO> postsDTO = new List<PostDTO>();

            try
            {
                posts.ForEach(post => postsDTO.Add(_responseAdapter.asDTO(post)));
            }
            catch
            {
                return StatusCode(500, "Unable to fetch posts from database.");
            }

            return Ok(postsDTO);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
        [HttpGet("user/{userId}/{title}")]
        public IActionResult SearchPostTitle(string userId, string title)
        {

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Please provide a user id");
            }

            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, userId))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            if (_context.Users.Find(userId) == null)
            {
                return NotFound("Please provide a valid user id");
            }

            List<Post> posts = _context.Posts.Where(item => item.UserId == userId).ToList();
            if(posts.Count == 0)
            {
                return Ok("No posts found for user");
            }

            List<Post> selectedPosts = new List<Post>();
            
            posts.ForEach(post => 
            {
                if(post.Title.ToLower().Contains(title.ToLower())) {
                    selectedPosts.Add(post);
                }
            });

            if(selectedPosts.Count == 0)
            {
                return Ok("No posts found for title search");
            }

            selectedPosts.ForEach(post => post.Content = _encodingUtil.Base64Decode(post.Content));

            List<PostDTO> postsDTO = new List<PostDTO>();

            try
            {
                selectedPosts.ForEach(post => postsDTO.Add(_responseAdapter.asDTO(post)));
            }
            catch
            {
                return StatusCode(500, "Unable to fetch posts from database.");
            }

            return Ok(postsDTO);
        }

        [DisableCors]
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

            try
            {
                postToReturn.Content = _encodingUtil.Base64Decode(postToReturn.Content);
            }
            catch
            {
                return StatusCode(500, "Unable to fetch post details from database.");
            }

            return Ok(_responseAdapter.asDTO(postToReturn));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
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

            string username = request.userId;
            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, username))
            {
                return Unauthorized("You do not have permission to access this data");
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

            try
            {
                newPost.PrivacyStatus = privacyObject;
                newPost.PrivacyStatusId = privacyObject.PrivacyStatusId;
                privacyObject.Posts.Add(newPost);
                newPost.UserId = request.userId;
                newPost.User = user;
                user.Posts.Add(newPost);

                _context.Posts.Update(newPost);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "Unable to create new post.");
            }

            return Ok(_responseAdapter.asDTO(newPost));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
        [HttpPut("{id}")]
        public IActionResult EditPut(int? id, [FromBody] PostDTO request)
        {
            if (id == null)
            {
                return BadRequest("Please provide a valid id");
            }

            bool validRequest = request != null;
            validRequest &= (request.title != null);
            validRequest &= (request.content != null);
            validRequest &= (request.privacyStatus != null);

            if (!validRequest)
            {
                return BadRequest("Please enter all the valid information");
            }

            Post postToEdit = _context.Posts.Find(id);
            if (postToEdit == null)
            {
                return NotFound("Please provide a valid id");
            }

            string username = postToEdit.UserId;
            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, username))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            Post newPost = new Post();

            PrivacyStatus privacyObject = _fkUtil.getPrivacyStatus(request.privacyStatus);

            newPost.PostId = postToEdit.PostId;
            newPost.Title = request.title;
            newPost.Content = _encodingUtil.Base64Encode(request.content);
            newPost.Timestamp = DateTime.Now;

            try
            {
                newPost.PrivacyStatusId = privacyObject.PrivacyStatusId;
                newPost.PrivacyStatus = privacyObject;
                privacyObject.Posts.Add(newPost);

                newPost.UserId = postToEdit.UserId;
                newPost.User = _context.Users.Find(newPost.UserId);

                _context.Entry(postToEdit).CurrentValues.SetValues(newPost);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "Unable to save changes.");
            }

            return Ok(_responseAdapter.asDTO(newPost));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
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

            string username = postToEdit.UserId;
            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, username))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            if (request == null)
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

            try
            {
                _context.Posts.Update(postToEdit);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "Unable to save changes.");
            }


            return Ok(_responseAdapter.asDTO(postToEdit));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [EnableCors("PostsPolicy")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Please provide a valid id");
            }

            Post postToDelete = _context.Posts.Find(id);

            if (postToDelete == null)
            {
                return NotFound("Please provide a valid id");
            }

            string username = postToDelete.UserId;
            var authToken = _authUtil.decodeJWT(Request.Headers["Authorization"]);

            if (!_authUtil.isUser(authToken, username))
            {
                return Unauthorized("You do not have permission to access this data");
            }

            try
            {
                _context.Posts.Remove(postToDelete);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Unable to delete post.");
            }

            return Ok(_responseAdapter.asDTO(postToDelete));
        }

    }
}
