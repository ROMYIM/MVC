using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using Pomelo.AspNetCore.TimedJob;

namespace MVC.Tasks
{
    public class IntervalTask : Job
    {
        private readonly QrCoreContext _context;
        private readonly ILogger _logger;
        // private readonly RequestDelegate _next;

        // public IntervalTask(RequestDelegate next,QrCoreContext context, ILoggerFactory loggerFactory)
        // {
        //     this._next = next;
        //     this._context = context;
        //     this._logger = loggerFactory.CreateLogger<IntervalTask>();
        // }

        public IntervalTask(QrCoreContext context, ILoggerFactory loggerFactory)
        {
            this._context = context;
            this._logger = loggerFactory.CreateLogger<IntervalTask>();
        }

        [Invoke(Begin = "2016-6-12 18:00", Interval = 1000 * 1800)]
        public void UpdateOpenNumber()
        {
            _logger.LogDebug("定时任务运行");
            var openNumber = _context.Equipment.First().OpenNumber;
            _logger.LogDebug($"库存openNumber:{openNumber}");
            openNumber = new Random().Next(1000, 10000);
            _logger.LogDebug($"修改后openNumber:{openNumber}");
            _context.Database.ExecuteSqlCommand($"update equipment set OpenNumber = {openNumber}");
            _context.SaveChanges();
        }

        // public async Task InvokeAsync(HttpContext context)
        // {
        //     _logger.LogDebug("定时任务");
        //     StartTask();
        //     await _next.Invoke(context);
        // }

        // public void StartTask()
        // {
        //     RecurringJob.AddOrUpdate(() => UpdateOpenNumber(), Cron.MinuteInterval(3));
        // }
    }
}