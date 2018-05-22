using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Extensions;
using MVC.Models;
using MVC.Utils;

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

        [HttpPost]
        public async Task<IActionResult> Login([Bind("ID, Password"), FromForm]User user)
        {
            if (ModelState.IsValid)
            {
                var userTemp = await _context.User.FindAsync(user.ID);
                if (userTemp != null && userTemp.Password == user.Password)
                {
                    HttpContext.Session.SetString("ID", user.ID);
                    if (userTemp.Status == Status.Administrator)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return Json(new Result(1));
                    }
                }
            }
            return BadRequest("用户不存在或密码错误");
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("ID, Password")]User user)
        {
            if (ModelState.IsValid && UserExists(user.ID))
            { 
                _context.User.Add(user);
                int judgement = await _context.SaveChangesAsync();
                return Json(new Result(judgement));
            }
            return Json(new Result(0));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, Password")]User user)
        {
            if (ModelState.IsValid && UserExists(user.ID))
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return BadRequest("用户已存在或输入信息有误");
            }
        }

        public async Task<IActionResult> Detail(string id, string view)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("用户号不能为空");
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
                    return View(model: user, viewName: view);
                }
            }    
        }

        
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                int deleteCount = await _context.Database.ExecuteSqlCommandAsync($"delete from user where id = {id}");
                _logger.LogDebug($"删除数：{deleteCount}");
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("ID, UnitNumber, Status")]User user)
        {
            var userTemp = _context.User.Find(user.ID);
            await _context.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Detail), routeValues: new { id = user.ID});
        }

        [HttpPost]
        public async Task<IActionResult> GetOpenNumber(string id)
        {
            var user = _context.User.Find(id);
            if (user != null)
            {
                var openNumber = this.GetOpenNumber();
                _context.UserOpenInformation.Add(new UserOpenInformation(user, openNumber));
                await _context.SaveChangesAsync();
                return Json(new Result(openNumber));
            }
            return Json(new Result(0));
        }

        private bool UserExists(string id)
        {
            return (!_context.User.Any(e => e.ID == id) && _context.Resident.Any(r => r.PhoneNumber == id));
        }
    }
}