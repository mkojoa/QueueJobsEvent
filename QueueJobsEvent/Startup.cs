using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coravel;
using Coravel.Events.Interfaces;
using Coravel.Queuing.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using QueueJobsEvent.Events;
using QueueJobsEvent.Invocables;
using QueueJobsEvent.Listeners;

namespace QueueJobsEvent
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            /*
             * ****** Coravel Scheduling;Queuing;Mail ******
             */
            services.AddScheduler();
            services.AddQueue();
            services.AddMailer(this.Configuration);


            /*
             * ****** Register Invocables ******
             */
            services.AddScoped<DoExpensiveCalculationAndStore>();
            services.AddScoped<SendDailyReportsEmailJob>();


            /*
             * ****** Register Event Listiners ******
             */
            services.AddEvents();
            services.AddTransient<NotifyAccountByEmailListener>()
                    .AddTransient<NotifyAdministratorsListener>();

            SwaggerConfiguration(services);

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                "QueueJobsEvent v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            IEventRegistration registration = app.ApplicationServices.ConfigureEvents();

            /*
             * ****** Bind Event with Listiners ******
             */
            registration.Register<AccountCreated>()
                .Subscribe<NotifyAccountByEmailListener>()
                .Subscribe<NotifyAdministratorsListener>();

            //app.ApplicationServices.UseScheduler(scheduler =>
            //{
            //    scheduler.OnWorker("CPUIntensiveTasks");
            //    scheduler
            //        .Schedule<RebuildStaticCachedData>().Hourly();

            //    scheduler.OnWorker("TestingSeconds");
            //    scheduler.Schedule(
            //        () => Console.WriteLine($"Runs every second. Ran at: {DateTime.UtcNow}")
            //    ).EverySecond();
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every thirty seconds. Ran at: {DateTime.UtcNow}")).EveryThirtySeconds().Zoned(TimeZoneInfo.Local);
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every ten seconds. Ran at: {DateTime.UtcNow}")).EveryTenSeconds();
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every fifteen seconds. Ran at: {DateTime.UtcNow}")).EveryFifteenSeconds();
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every thirty seconds. Ran at: {DateTime.UtcNow}")).EveryThirtySeconds();
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every minute Ran at: {DateTime.UtcNow}")).EveryMinute();
            //    scheduler.Schedule(() => Console.WriteLine($"Runs every 2nd minute Ran at: {DateTime.UtcNow}")).Cron("*/2 * * * *");
            //});

            app.ApplicationServices
                .ConfigureQueue()
                .LogQueuedTaskProgress(app.ApplicationServices.GetService<ILogger<IQueue>>());

            app.ApplicationServices.ConfigureQueue()
                .LogQueuedTaskProgress(app.ApplicationServices.GetService<ILogger<IQueue>>());

        }



        private static void SwaggerConfiguration(IServiceCollection services)
        {
            var contact = new OpenApiContact()
            {
                Name = "FirstName LastName",
                Email = "user@example.com",
                Url = new Uri("http://www.example.com")
            };
            var license = new OpenApiLicense()
            {
                Name = "My License",
                Url = new Uri("http://www.example.com")
            };
            var info = new OpenApiInfo()
            {
                Version = "v1",
                Title = "QueueJobsEvent API",
                Description = "Swagger Demo API Description",
                TermsOfService = new Uri("http://www.example.com"),
                Contact = contact,
                License = license
            };
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", info);
            });
        }
    }
}
