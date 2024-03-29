using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSouthlLibrary.Book.Api.Core.ContextMongoDB;
using TheSouthlLibrary.Book.Api.Core.Entities;
using TheSouthlLibrary.Book.Api.Repository;

namespace TheSouthlLibrary.Book.Api
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
            services.Configure<MongoSetting>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoDb:Database").Value;
                });

            services.AddSingleton<MongoSetting>();
            services.AddTransient<IAutorContext, AutorContext>();
            services.AddTransient<IAutorRepository, AutorRepository>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books", Version = "v1" });
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule => {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));

            app.UseRouting();
            app.UseCors("CorsRule");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
