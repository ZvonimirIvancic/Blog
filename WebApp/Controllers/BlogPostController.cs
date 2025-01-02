using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;
using X.PagedList.Extensions;

namespace WebApp.Controllers
{
    public class BlogPostController : Controller
    {

        private readonly BlogContext _context;
        private readonly IMapper _mapper;

        public BlogPostController(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: BlogPostController
        public async Task<IActionResult> Index(int? page, string? searchText, string? sortOrder)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "date_desc" : "";


            IQueryable<BlogPost> blogsQuery = _context.BlogPosts.Include(p => p.Comments);

            if (!string.IsNullOrEmpty(searchText))
            {

                blogsQuery = blogsQuery.Where(p =>
                    p.Title.Contains(searchText)
                );
            }


            switch (sortOrder)
            {
                case "date_desc":
                    blogsQuery = blogsQuery.OrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    blogsQuery = blogsQuery.OrderBy(p => p.CreatedAt);
                    break;
            }

            var blogs = await blogsQuery.ToListAsync();

            ViewData["pages"] = blogs.Count / pageSize;
            Response.Cookies.Append("SearchText", searchText ?? "", new CookieOptions { Expires = DateTime.Now.AddDays(7) });
            ViewData["page"] = page;

            return View(blogs.ToPagedList(pageNumber, pageSize));
        }



        // GET: BlogPostController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var post = _context.BlogPosts.FirstOrDefault(x => x.IdblogPosts == id);
                var postVM = new VMBlogPost
                {
                    IdblogPosts = post.IdblogPosts,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,
                    UserId = post.UserId
                };

                return View(postVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: BlogPostController/Create
        public ActionResult Create()
        {
             ViewBag.UserItems = _context.Users.Select(x =>
            new SelectListItem
                {
              Text = x.Username,
                Value = x.Idusers.ToString()
                });

            ViewBag.Users = new SelectList(_context.Comments, "Idusers", "Content");
            return View();
        }

        // POST: BlogPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VMBlogPost post)
        {
            try
            {
                if (!ModelState.IsValid)
                {


                    ModelState.AddModelError("", "Failed to create post");

                    return View();
                }

                var existingPoll = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Title == post.Title);
                if (existingPoll != null)
                {
                    ModelState.AddModelError("Name", "A post with the same title already exists.");
                    ViewBag.Comments = new SelectList(_context.Comments, "Idcomments", "Content");
                    return View(post);
                }



                var newPost = _mapper.Map<BlogPost>(post);
                _context.BlogPosts.Add(newPost);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: BlogPostController/Edit/5
        public ActionResult Edit(int id)
        {

                  ViewBag.UserItems = _context.Users.Select(x =>
            new SelectListItem
            {
                Text = x.Username,
                Value = x.Idusers.ToString()
                });

            var post = _context.BlogPosts.FirstOrDefault(x => x.IdblogPosts == id);
            var blogPostVM = new VMBlogPost
            {
                IdblogPosts = post.IdblogPosts,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                UserId = post.UserId


            };
            return View(blogPostVM);

        }

        // POST: BlogPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMBlogPost post)
        {
            try
            {
                var dbBlogPost = _context.BlogPosts.FirstOrDefault(x => x.IdblogPosts == id);
                dbBlogPost.Title = post.Title;
                dbBlogPost.Content = post.Content;
                dbBlogPost.CreatedAt = post.CreatedAt;
                dbBlogPost.UpdatedAt = post.UpdatedAt;
                dbBlogPost.UserId = post.UserId;




                _context.SaveChanges();


                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogPostController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var post = _context.BlogPosts.FirstOrDefault(x => x.IdblogPosts == id);
                var postVM = new VMBlogPost
                {
                    IdblogPosts = post.IdblogPosts,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedAt = post.CreatedAt,
                    UpdatedAt = post.UpdatedAt,


                };

                return View(postVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: BlogPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMBlogPost post)
        {
            try
            {
                var dbPost = _context.BlogPosts.FirstOrDefault(x => x.IdblogPosts == id);

                _context.BlogPosts.Remove(dbPost);

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
