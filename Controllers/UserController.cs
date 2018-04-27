using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly QrCoreContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(QrCoreContext context, ILogger<UserController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View(model: await _context.User.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Password, UnitNumber")]User user)
        {
            if (ModelState.IsValid && !UserExists(user.ID))
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("用户已存在");
            }
        }

        public async Task<IActionResult> Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("用户号不能为空");
            } 
            else
            {
                var user = await _context.User.FindAsync(id);
                if (user == null)
                {   
                    return NotFound("用户不存在");
                }
                else
                {
                    return View(model: user);
                }
            }    
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new NullReferenceException("用户号不能为空");
            }
            else
            {
                await _context.Database.ExecuteSqlCommandAsync($"delete from user where id = {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool UserExists(string id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}