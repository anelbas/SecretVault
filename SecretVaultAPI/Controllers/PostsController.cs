using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretVaultAPI.DTOs;
using SecretVaultAPI.Model;
using System.Linq;

namespace SecretVaultAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostsController : Controller
  {

    public SecretVaultDBContext _context = new SecretVaultDBContext();

    [HttpGet("post")]
    public IActionResult DetailsForAllPosts()
    {
      return Ok(_context.Posts.ToList());
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
      validRequest |= (request._timestamp != null);
      validRequest |= (request._privacyStatusId != null);
      validRequest |= (request._userId != null);

      if (!validRequest)
      {
        return BadRequest();
      }

      Post newPost = new Post();

      newPost.Title = request._title;
      newPost.Content = Base64Encode(request._content);
      newPost.Timestamp = request._timestamp;
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
      validRequest |= (request._timestamp != null);
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
      newPost.Timestamp = request._timestamp;
      newPost.PrivacyStatusId = request._privacyStatusId;
      newPost.UserId = request._userId;


      _context.Entry(postToEdit).CurrentValues.SetValues(newPost);
      _context.SaveChanges();


      return Ok(newPost);
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
