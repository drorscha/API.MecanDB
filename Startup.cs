using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
using API.MecanDB.Controllers;
using API.MecanDB.Models;
using API.MecanDB.Repositories;
using API.MecanDB.Services;

namespace API.MecanDB
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API.MecanDB", Version = "v1" });
            });

            services.AddScoped<ClienteRepository>(provider => new ClienteRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ClienteService>();
            services.AddScoped<CarrinhoRepository>(provider => new CarrinhoRepository(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<CarrinhoService>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API.MecanDB v1"));
            }
            else
            {
                // Aqui você pode configurar o tratamento de erros para ambientes de produção
                // app.UseExceptionHandler("/Error");
                // app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
