using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Controllers
{
    public class OpenInformationController : Controller
    {
        private readonly QrCoreContext _context;

        public OpenInformationController(QrCoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(model:await _context.UserOpenInformation.OrderByDescending(i => i.Time).ToListAsync());
        }

        public async Task<IActionResult> QueryByUser(string id)
        {
            return View(model:await _context.UserOpenInformation.Where(i => i.UserID == id).ToArrayAsync(), viewName:nameof(Index));
        }
    }



}