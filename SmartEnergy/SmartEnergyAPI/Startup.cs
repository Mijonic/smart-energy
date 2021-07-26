using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergy.Service.Services;
using SmartEnergyAPI.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartEnergyAPI
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApprovedOnly", policy => policy.RequireClaim("Approved"));
            });

            services.AddAuthentication(opt => {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = "http://localhost:44372",
                   ValidAudience = "http://localhost:44372",
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))
               };
           });
            services.AddControllers().AddJsonOptions(options =>
                                     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())); 

            services.AddDbContext<SmartEnergyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmartEnergyDatabase")));

            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Energy API", Version = "v1" });
                
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader()
                                        .AllowAnyMethod().AllowCredentials();
                });
            });



            //Add Service implementations
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IIconService, IconService>();
            services.AddScoped<ICrewService, CrewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IWorkRequestService, WorkRequestService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<IMultimediaService, MultimediaService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IStateChangeService, StateChangeService>();
            services.AddScoped<IResolutionService, ResolutionService>();
            services.AddScoped<IDeviceUsageService, DeviceUsageService>();
            services.AddScoped<ICallService, CallService>();
            services.AddScoped<IMailService, MailingService>();
            services.AddScoped<IAuthHelperService, AuthHelperService>();
            services.AddScoped<IConsumerService, ConsumerService>();
            services.AddScoped<ISafetyDocumentService, SafetyDocumentService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Smart Energy API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SmartEnergyDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
