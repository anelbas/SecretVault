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

      return Ok(postToReturn);
    }

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
      newPost.Content = request._content;
      newPost.Timestamp = request._timestamp;
      newPost.PrivacyStatusId = request._privacyStatusId;
      newPost.UserId = request._userId;

      _context.Add(newPost);
      _context.SaveChanges();

      return Ok(newPost);
    }

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
      newPost.Content = request._content;
      newPost.Timestamp = request._timestamp;
      newPost.PrivacyStatusId = request._privacyStatusId;
      newPost.UserId = request._userId;


      _context.Entry(postToEdit).CurrentValues.SetValues(newPost);
      _context.SaveChanges();


      return Ok(newPost);
    }





  }
}
