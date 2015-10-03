using System;
using Hangfire;
using Hangfire.Logging;
using Hangfire.MemoryStorage;
using HangFireTest.App_Start;
using HangFireTest.Controllers;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(HangFireTest.Startup))]

namespace HangFireTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
            GlobalConfiguration.Configuration.UseNinjectActivator(NinjectWebCommon.kernel);

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            var options = new BackgroundJobServerOptions();
            LogProvider.SetCurrentLogProvider(new RollbarLogProvider());
            RecurringJob.AddOrUpdate<SuperJob>(sb => sb.DoIt(), Cron.Minutely());
        }
    }

    public class SuperJob
    {
        private readonly IJustMakeMeHappy _justMakeMeHappy;

        public SuperJob(IJustMakeMeHappy justMakeMeHappy)
        {
            _justMakeMeHappy = justMakeMeHappy;
        }

        public void DoIt()
        {
            System.Diagnostics.Debug.WriteLine(HomeController.message++);
            System.Diagnostics.Debug.WriteLine(_justMakeMeHappy.Message());
        }
    }

    public class RollbarLogProvider : ILogProvider
    {
        public ILog GetLogger(string name)
        {
            return new RollbarLog();
        }
    }

    public class RollbarLog : ILog
    {
        public bool Log(LogLevel logLevel, Func<string> messageFunc, Exception exception = null)
        {
            System.Diagnostics.Debug.WriteLine($"{logLevel} => {messageFunc?.Invoke()} ------- {exception}");
            return true;
        }
    }
}
