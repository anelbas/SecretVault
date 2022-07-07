using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.DTOs;
using SecretVaultAPI.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SecretVaultAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostsController : Controller
  {

    public SecretVaultDBContext _context = new SecretVaultDBContext();

    [HttpGet("post")]
    public IActionResult DetailsForAllPublicPosts()
    {

      List<Post> posts = _context.Posts.Where(item => item.PrivacyStatusId == 2).ToList();
      List<Post> newPosts = new List<Post>();
      foreach (Post post in posts)
      {
        post.Content = Base64Decode(post.Content);

        newPosts.Add(post);
      }

      return Ok(newPosts);
    }

    [HttpGet("post/user/{id}")]
    public IActionResult DetailsForAllUserPosts(int? id)
    {

    if (id == null)
      {
        return BadRequest();
      }

      List<Post> posts = _context.Posts.Where(item => item.UserId == id).ToList();
      List<Post> newPosts = new List<Post>();
      foreach (Post post in posts)
      {
        post.Content = Base64Decode(post.Content);

        newPosts.Add(post);
      }

      return Ok(newPosts);
    }

    // GET: Posts/Details/5
    [HttpGet("post/{id}")]
    public IActionResult Details(int? id)
    {
      if (id == null)
      {
        return BadRequest();
      }

      Post postToReturn = _context.Posts.Find(id);
      if (postToReturn == null)
      {
        return NotFound();
      }

      postToReturn.Content = Base64Decode(postToReturn.Content);

      return Ok(postToReturn);
    }

    //Encrypt the post
    // GET: Posts/Create
    [HttpPost("post")]
    public IActionResult Create([FromBody] PostDTO request)
    {

      bool validRequest = request != null;
      validRequest |= (request._title != null);
      validRequest |= (request._content != null);
      validRequest |= (request._privacyStatusId != null);
      validRequest |= (request._userId != null);

      if (!validRequest)
      {
        return BadRequest();
      }

      Post newPost = new Post();

      newPost.Title = request._title;
      newPost.Content = Base64Encode(request._content);
      newPost.Timestamp = DateTime.Now;
      newPost.PrivacyStatusId = request._privacyStatusId;
      newPost.UserId = request._userId;

      _context.Add(newPost);
      _context.SaveChanges();

      return Ok(newPost);
    }


    //Encrypt the post
    [HttpPut("post/{id}")]
    public IActionResult EditPut(int? id, [FromBody] PostDTO request)
    {
      if (id == null)
      {
        return BadRequest();
      }

      bool validRequest = request != null;
      validRequest |= (request._title != null);
      validRequest |= (request._content != null);
      validRequest |= (request._privacyStatusId != null);
      validRequest |= (request._userId != null);

      if (!validRequest)
      {
        return BadRequest();
      }

      Post postToEdit = _context.Posts.Find(id);
      if (postToEdit == null)
      {
        return NotFound();
      }


      Post newPost = new Post();

      newPost.Title = request._title;
      newPost.Content = Base64Encode(request._content);
      newPost.Timestamp = DateTime.Now;
      newPost.PrivacyStatusId = request._privacyStatusId;
      newPost.UserId = request._userId;


        newPost.PostId = postToEdit.PostId;

      _context.Entry(postToEdit).CurrentValues.SetValues(newPost);
      _context.SaveChanges();


      return Ok(newPost);
    }

    [HttpPatch("post/{id}")]
    public IActionResult EditPatch(int? id, [FromBody] PostDTO request)
    {
      if (id == null)
      {
        return BadRequest();
      }


      Post postToEdit = _context.Posts.Find(id);
      if (postToEdit == null)
      {
        return NotFound();
      }

      postToEdit.Title = (request._title == null) ? postToEdit.Title : request._title;
      postToEdit.Content = (request._content == null) ? postToEdit.Content : Base64Encode(request._content);
      postToEdit.Timestamp = DateTime.Now;
      postToEdit.PrivacyStatusId = (request._privacyStatusId  == 0) ? postToEdit.PrivacyStatusId :  request._privacyStatusId;
      postToEdit.UserId = (request._userId  == 0) ? postToEdit.UserId : request._userId;

      _context.Posts.Update(postToEdit);
      _context.SaveChanges();


      return Ok(postToEdit);
    }

    [HttpDelete("post/{id}")]
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
            catch  (Exception e)
            {
                Console.WriteLine("Error");
            }


            return Ok(postToDelete);
        }


    public static string Base64Decode(string base64EncodedData)
    {
      var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
      return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
    public static string Base64Encode(string plainText)
    {
      var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
      return System.Convert.ToBase64String(plainTextBytes);
    }


  }
}
