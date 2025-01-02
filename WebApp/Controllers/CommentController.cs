using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly BlogContext _context;
        private readonly IMapper _mapper;

        public CommentController(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: CommentController
        public ActionResult Index(int id)
        {
            try
            {
                var commentsVms = _context.Comments.Select(x => new VMComment
                {
                    Idcomments = x.Idcomments,
                    Content = x.Content,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt,
                    UserId = x.UserId,
                    BlogPostsId = x.BlogPostsId,

                }).ToList().Where(x=>x.BlogPostsId.Equals(id));

                return View(commentsVms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var comment = _context.Comments.FirstOrDefault(x => x.Idcomments == id);
                var commentVM = new VMComment
                {
                    Idcomments = comment.Idcomments,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt,
                    UserId = comment.UserId,
                    BlogPostsId = comment.BlogPostsId,
                };

                return View(commentVM);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: CommentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMComment comment)
        {
            try
            {
                var newComment = new Comment
                {
                    Idcomments = comment.Idcomments,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt,
                    UserId = comment.UserId,
                    BlogPostsId = comment.BlogPostsId,
                };

                _context.Comments.Add(newComment);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var comment = _context.Comments.FirstOrDefault(x => x.Idcomments == id);
                var commentVM = new VMComment
                {
                    Idcomments = comment.Idcomments,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt,
                    UserId = comment.UserId,
                    BlogPostsId = comment.BlogPostsId
                };

                return View(commentVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMComment comment)
        {
            try
            {
                var dbComment = _context.Comments.FirstOrDefault(x => x.Idcomments == id);
                dbComment.Idcomments = comment.Idcomments;
                dbComment.Content = comment.Content;
                dbComment.CreatedAt = comment.CreatedAt;
                dbComment.UpdatedAt = comment.UpdatedAt;
                dbComment.UserId = comment.UserId;
                dbComment.BlogPostsId = comment.BlogPostsId;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var comment = _context.Comments.FirstOrDefault(x => x.Idcomments == id);
                var commentVM = new VMComment
                {
                    Idcomments = comment.Idcomments,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt,
                    UserId = comment.UserId,
                    BlogPostsId = comment.BlogPostsId,
                };

                return View(commentVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMComment comment)
        {
            try
            {
                var dbComment = _context.Comments.FirstOrDefault(x => x.Idcomments == id);

                _context.Comments.Remove(dbComment);

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
