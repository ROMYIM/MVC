using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Models;
using Newtonsoft.Json.Linq;

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
        public async Task<IActionResult> OpenDoor([FromBody]JObject json)
        {
            if (ModelState.IsValid)
            {
                var equipmentID = json["equipmentid"].Value<string>();
                var openNumber = json["opennumber"].Value<int>();
                var equipment = _context.Equipment.Find(equipmentID);
                var openInformation = _context.UserOpenInformation.Find(openNumber);
                if (equipment != null && openInformation != null && !openInformation.Result)
                {
                    equipment.WorkingTime++;
                    openInformation.Equipment = equipment;
                    openInformation.EquipmentID = equipment.EquipmentID;
                    openInformation.Time = DateTime.Now;
                    openInformation.Result = true;
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

        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _context.Equipment.Remove(_context.Equipment.Find(id));
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}