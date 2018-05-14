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

        public async Task<IActionResult> Index()
        {
            return View(model: await _context.Equipment.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> OpenDoor([Bind("EquipmentID, OpenNumber"), FromBody]Equipment equipment)
        {
            if (ModelState.IsValid)
            {
                if (_context.Equipment.Where(e => e.EquipmentID == equipment.EquipmentID && e.OpenNumber == equipment.OpenNumber).Any())
                {
                    var equipmentTemp = _context.Equipment.Find(equipment.EquipmentID);
                    equipmentTemp.WorkingTime++;
                    var operationTime = DateTime.Now;
                    _context.Database.ExecuteSqlCommand($"update open_door_information set time = {operationTime} where EquipmentID = {equipment.EquipmentID}");
                    await _context.SaveChangesAsync();
                    return Json(new Result(1));
                }
            }
            return Json(new Result(0));
        }

        public async Task<IActionResult> Detail(string equipmentID)
        {
            if (string.IsNullOrEmpty(equipmentID))
            {
                return View();
            }
            else
            {
                var equipment = await _context.Equipment.FindAsync(equipmentID);
                if (equipment == null)
                {
                    return NotFound("查询失败");
                }
                return View(model:equipment);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("EquipmentID, OpenNumber"), FromForm]Equipment equipment)
        {
            if (ModelState.IsValid && !_context.Equipment.Any(e => e.EquipmentID == equipment.EquipmentID))
            {
                _context.Add(equipment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("操作失败");
        }
    }
}