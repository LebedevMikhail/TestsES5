using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestsES5.Models;
using TestsES5.Models.Interfaces;

namespace TestsES5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStrings:ConnectionToDb"]);

            });
            services.AddTransient<IQuestionRepository, QuestionRepository>();
            services.AddScoped<QuestionStorage>(sp => SessionQuestionStorage.GetStorage(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                       name: "",
                       template: "/",
                       defaults: new { Controller = "Question", action = "Index" });

                //routes.MapRoute(
                //    name: "",
                //    template: "/Test/Question",
                //    defaults: new { Controller = "Question", action = "Index" });
                routes.MapRoute(
                     name: "",
                     template: "Question/Next",
                     defaults: new { Controller = "Question", action = "GetNextQuestion" });

                routes.MapRoute(
                    name: "",
                    template: "/{controller=Question}/{action=TestInit}/{query?}");




            });
        }
    }
}
