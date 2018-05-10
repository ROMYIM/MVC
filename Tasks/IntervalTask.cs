using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Tasks
{
    public class IntervalTask
    {
        private readonly QrCoreContext _context;

        public IntervalTask(QrCoreContext context)
        {
            this._context = context;
        }

        public void UpdateOpenNumber()
        {
            var openNumber = _context.Equipment.First().OpenNumber;
            openNumber = new Random(openNumber).Next(1000, 10000);
            _context.Database.ExecuteSqlCommand($"update equipment set OpenNumber = {openNumber}");
        }
    }
}