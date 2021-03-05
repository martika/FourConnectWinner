using ConnectFourWinner.Api.DependencyResolution;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Text.Json.Serialization;

namespace ConnectFourWinner.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            var logConfiguration = new LoggerConfiguration()
                   .ReadFrom.Configuration(configuration);
            Log.Logger = logConfiguration.CreateLogger();
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServices();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                  });
            });

            services.AddControllers()
                .AddNewtonsoftJson(
                    opt =>
                    {
                        opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                    }
                )
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                }); 
            
            services.AddSwaggerGen(c =>
            {

                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "ConnectFourWinner.Api", 
                    Version = "v1",
                    Description = "Connect Four - Find the winner",
                    Contact = new OpenApiContact
                    {
                        Name = "Martina Santonja",
                        Email = string.Empty
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ConnectFourWinner.Api v1"));
            }

            app.UseRouting();
            app.UseAuthorization();

            app.UseCors(MyAllowSpecificOrigins);
                        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
