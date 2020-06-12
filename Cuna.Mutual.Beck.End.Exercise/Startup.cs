using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cuna.Mutual.Beck.End.Exercise.Api;
using Cuna.Mutual.Beck.End.Exercise.Api.Data;
using Cuna.Mutual.Beck.End.Exercise.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;




namespace Cuna.Mutual.Beck.End.Exercise
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
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddControllers();
            services.AddDbContext<MacGuffinContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MacGuffinContext")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IThirdPartyService, ThirdPartyService>();
            services.AddTransient<IMacGuffinRepository, MacGuffinRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //turning of https redirection because site is only an api This will lower some veteran of abilities of bots automatically sniffing open HTTP ports.  and then attacking the related https ports.
            //app.UseHttpsRedirection();




            //I would be turning on Hsts But for the sake of the demo I'm going to leave it off I would consider security with certificate management to be outside scope what was asked for.
            //app.UseHsts();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
