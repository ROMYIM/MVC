using System;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVC.Data;
using MVC.Utils;
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
            _logger.LogDebug("定时任务初始化");
        }

        [Invoke(Begin = "2016-6-12 18:00", Interval = 1000 * 60)]
        public async Task UpdateOpenNumber()
        {
            _logger.LogDebug("定时任务运行");
            if (this.UpdateOpenNumbers())
            {
                _context.UserOpenInformation.RemoveRange(_context.UserOpenInformation);
                await _context.SaveChangesAsync();
            }
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