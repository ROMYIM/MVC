using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers
{
    public class EquipmentController : Controller
    {
        private readonly QrCoreContext _context;
        private readonly ILogger<EquipmentController> _logger;

        public EquipmentController(QrCoreContext context, ILogger<EquipmentController> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        [HttpPost]
        public IActionResult OpenDoor([Bind("EquipmentID, OpenNumber"), FromBody]Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                if (_context.Equipment.Where(e => e.EquipmentID == equipment.EquipmentID && e.OpenNumber == equipment.OpenNumber).Any())
                {
                    return Json(new Result(1));
                }
            }
            return Json(new Result(0));
        }
    }
}