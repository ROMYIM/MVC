using System;
using System.Linq;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Tasks
{
    public class IntervalTask : ITask
    {

        public IntervalTask(QrCoreContext context)
        {
            Context = context;
            RecurringJob.AddOrUpdate(() => UpdateOpenNumberTask(), Cron.MinuteInterval(5));
        }

        public QrCoreContext Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void UpdateOpenNumberTask()
        {
            var openNumber = Context.Equipment.First().OpenNumber;
            openNumber = new Random(openNumber).Next(1000, 10000);
            Context.Database.ExecuteSqlCommand($"update equipment set OpenNumber = {openNumber}");
        }
    }
}