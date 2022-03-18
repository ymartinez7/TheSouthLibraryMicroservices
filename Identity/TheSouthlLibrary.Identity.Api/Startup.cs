using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSouthlLibrary.Identity.Api.Core.Application;
using TheSouthlLibrary.Identity.Api.Core.Entities;
using TheSouthlLibrary.Identity.Api.Core.JwtLogic;
using TheSouthlLibrary.Identity.Api.Core.Persistence;

namespace TheSouthlLibrary.Identity.Api
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
            // EF
            services.AddDbContext<SeguridadContexto>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("ConexionDB"));
            });

            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);
            identityBuilder.AddEntityFrameworkStores<SeguridadContexto>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddMediatR(typeof(Register.UsuarioRegisterCommand).Assembly);

            services.AddAutoMapper(typeof(Register.UsuarioRegisterHandler));

            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.AddScoped<IUsuarioSesion, UsuarioSesion>();

            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Register>());

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity", Version = "v1" });
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsRule", rule =>
                {
                    rule.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });

            // Validación de tokens JWT
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("4Vcm0yBpfERWD0RSJ0MpwnWUqB7SdEP5"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));

            app.UseRouting();

            app.UseCors("CorsRule");

            // Importante usarlo para token JWT
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
