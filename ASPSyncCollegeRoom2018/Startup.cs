#region Copyright Syncfusion Inc. 2001-2017.
// Copyright Syncfusion Inc. 2001-2017. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPSyncCollegeRoom2018.Data;
using ASPSyncCollegeRoom2018.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ASPSyncCollegeRoom2018
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

            //System.InvalidOperationException: 'Services for database providers 'Microsoft.EntityFrameworkCore.InMemory', 'Microsoft.EntityFrameworkCore.Sqlite' have been registered in the service provider. Only a single database provider can be registered in a service provider. If possible, ensure that Entity Framework is managing its service provider by removing the call to UseInternalServiceProvider. Otherwise, consider conditionally registering the database provider, or maintaining one service provider per database provider.'

            //  Adding In Memory Database. https://dotnetthoughts.net/getting-started-with-odata-in-aspnet-core/

            //services.AddDbContext<CalendarDBContext>(options =>
            //{
            //    options.UseInMemoryDatabase("InMemoryDb");
            //});

            //sqlite connection
            services.AddDbContext<CalendarDBContext>(options =>
                options.UseSqlite("Data Source=cal.db"));

            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ScheduleDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False


            //SQL connection
            var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ScheduleDB;Integrated Security=True";
            //services.AddDbContext<CalendarDBContext>(options => options.UseSqlServer(connection));

            //Adding OData middleware.
            services.AddOData();
            services.AddMvc();

        }






        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            //Adding Model class to OData
            var builder = GetEdmModel(app.ApplicationServices);
            builder.EntitySet<ScheduleData>(nameof(ScheduleData));
          //  builder.EntitySet<Appointment>(nameof(Appointment));


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                // app.UseExceptionHandler("/Home/Error");
                app.UseDeveloperExceptionPage();

            }


            app.UseStaticFiles();

            app.UseMvc((routebuilder =>
            {
                routebuilder.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
                routebuilder.EnableDependencyInjection(); //hack? https://github.com/OData/WebApi/issues/1175
            }));

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
        //https://stackoverflow.com/questions/48551571/creating-enbdpoints-that-return-odata-in-asp-net-core-web-api/48558352#48558352 
        private static ODataConventionModelBuilder GetEdmModel(IServiceProvider serviceProvider)
        {
            var builder = new ODataConventionModelBuilder(serviceProvider);

            return builder;
        }

    }
}
