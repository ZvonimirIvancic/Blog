using AutoMapper;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogContext _context;
        private readonly IMapper _mapper;

        public UserController(BlogContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult ProfileDetails()
        {
            var username = _context.Users.FirstOrDefault().Username;

            var userDb = _context.Users.FirstOrDefault(x => x.Username == username);
            var userVm = new VMUser
            {
                Idusers = userDb.Idusers,
                Username = userDb.Username,
                Email = userDb.Email,

            };

            return View(userVm);
        }

        public IActionResult ProfileEdit(int id)
        {
            var userDb = _context.Users.First(x => x.Idusers == id);
            var userVm = new VMUser
            {
                Idusers = userDb.Idusers,
                Username = userDb.Username,
                Email = userDb.Email,

            };

            return View(userVm);
        }

        [HttpPost]
        public IActionResult ProfileEdit(int id, VMUser userVm)
        {
            var userDb = _context.Users.First(x => x.Idusers == id);
            userDb.Username = userVm.Username;
            userDb.Email = userVm.Email;


            _context.SaveChanges();

            return RedirectToAction("ProfileDetails");
        }
        // GET: UserController
        public ActionResult Index()
        {
            try
            {
                var usersVms = _context.Users.Select(x => new VMUser
                {
                    Idusers = x.Idusers,
                    Username = x.Username,
                    Password = x.Password,
                    Email = x.Email
                }).ToList();

                return View(usersVms);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Idusers == id);
                var userVM = new VMUser
                {
                    Idusers = user.Idusers,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                };

                return View(userVM);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VMUser user)
        {
            try
            {
                var newUser = new User
                {
                    Idusers = user.Idusers,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                };

                _context.Users.Add(newUser);

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Idusers == id);
                var userVM = new VMUser
                {
                    Idusers = user.Idusers,
                    Username = user.Username,
                    Password = user.Password,
                    Email = user.Email,
                };

                return View(userVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, VMUser user)
        {
            try
            {   
                var dbUser = _context.Users.FirstOrDefault(x => x.Idusers == id);
                dbUser.Username = user.Username;
                dbUser.Password = user.Password;
                dbUser.Email = user.Email;

                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Idusers == id);
                var userVM = new VMUser
                {
                    Idusers = user.Idusers,
                    Password = user.Password,
                    Username = user.Username,
                    Email = user.Email,
                };

                return View(userVM);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, VMUser user)
        {
            try
            {
                var dbUser = _context.Users.FirstOrDefault(x => x.Idusers == id);

                _context.Users.Remove(dbUser);

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
