using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TourismDXP.Data.Context;

namespace TourismDXP.Admin
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            MigrateManagementContext();
            StartLogging();
        }


        /// <summary>
        /// Start Serilog instance.
        /// </summary>
        public void StartLogging()
        {
            //Initialize Serilog instance.
            var logEventLevel = (LogEventLevel)Enum.Parse(typeof(LogEventLevel), ConfigurationManager.AppSettings["LogLevel"]);
            var logSwitch = new LoggingLevelSwitch(logEventLevel);
            Log.Logger = new LoggerConfiguration()
                        //.WriteTo.LiterateConsole()
                        .MinimumLevel.ControlledBy(logSwitch)
                        .WriteTo.File(AppDomain.CurrentDomain.BaseDirectory + "AppData\\Log\\" + ConfigurationManager.AppSettings["EnvironmentCode"] + "-" + "{Date}.txt",
                        rollingInterval: RollingInterval.Hour, rollOnFileSizeLimit: true, fileSizeLimitBytes: 10048576,
                        shared: true, retainedFileCountLimit: null,
                        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [{SourceContext}] {Message}{NewLine}{Exception}")
                       .CreateLogger();

        }
        /// <summary>
        /// Migrate Management Context
        /// </summary>
        private static void MigrateManagementContext()
        {
            try
            {
                System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<TourismDXPContext, Data.Configuration.Configuration>());
                var configuration = new Data.Configuration.Configuration();
                configuration.AutomaticMigrationsEnabled = true;
                configuration.AutomaticMigrationDataLossAllowed = true;
                var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
                migrator.Update();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "MigrateManagementContext");
            }
        }
    }
}
