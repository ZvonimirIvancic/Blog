using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly BlogContext _context;
        private readonly IMapper _mapper;


        public BlogPostController(IConfiguration configuration, BlogContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<BlogPostController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetBlogPosts()
        {

            if (_context.BlogPosts == null)
            {
                return NotFound();
            }
            var polls = await _context.BlogPosts
                        .ToListAsync();
            return _mapper.Map<List<BlogPostDto>>(polls);
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
        {
            if (_context.BlogPosts == null)
            {
                return NotFound();
            }
            var poll = await _context.BlogPosts.FindAsync(id);

            if (poll == null)
            {
                return NotFound();
            }

            return _mapper.Map<BlogPostDto>(poll);
        }

        // POST api/<BlogPostController>
        [HttpPost]

        public async Task<ActionResult<BlogPostDto>> PostBlogPost(BlogPostDto post)
        {

            var newPost = _mapper.Map<BlogPost>(post);
            _context.BlogPosts.Add(newPost);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetPoll", new { id = newPost.IdblogPosts }, _mapper.Map<BlogPost>(newPost));
        }

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, BlogPost blogPostDto)
        {
            BlogPost post = _mapper.Map<BlogPost>(blogPostDto);
            if (id != post.IdblogPosts)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            if (_context.BlogPosts == null)
            {
                return NotFound();
            }
            var poll = await _context.BlogPosts.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.BlogPosts.Remove(poll);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogPostExists(int id)
        {
            return (_context.BlogPosts?.Any(e => e.IdblogPosts == id)).GetValueOrDefault();
        }
    }
}
