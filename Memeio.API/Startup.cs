using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memeio.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Memeio.API.Helpers;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Memeio.API
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
            services.AddDbContext<DataContext>(x => x.UseSqlite(Configuration.GetConnectionString("DefaultConnection"))); // Integrate the connection/schema to our database
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddCors(); // Integrate Cors so we can communicate our API with our SPA
            services.AddAutoMapper(typeof(MemeioRepository).Assembly);

            //Add our repos to our services
            services.AddScoped<IMemeioRepository, MemeioRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();

            //Add authentication middleware to guard certain resources
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //Add middleware for globally catching exceptions
                app.UseExceptionHandler(builder => {
                    builder.Run(async context => { //context is related to HTTP Request/Response
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Code to return when handling an exception (code 500)
                        var err = context.Features.Get<IExceptionHandlerFeature>(); //store details of the error
                        if (err != null) // if there's any error
                        {
                            context.Response.AddApplicationError(err.Error.Message); // Adds a new header into HTTP response to give clear errors
                            await context.Response.WriteAsync(err.Error.Message); // write the error into our HTTP Response as well
                        }
                    });
                });
            }

            // app.UseHttpsRedirection(); // Configure later, for now let's just deal with 'http'

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); // Allow any connections for our current development

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
