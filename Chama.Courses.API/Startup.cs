using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chama.Courses.Application.Implementations;
using Chama.Courses.Application.Interfaces;
using Chama.Courses.Domain.Configuration;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Infrastructure.ServiceBus;
using Chama.Courses.Persistence.Context;
using Chama.Courses.Persistence.Repository.Implementation;
using Chama.Courses.Persistence.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Chama.Courses.API
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

            services.AddDbContext<EfDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ChamaConnectionString"));
            });

            var serviceBusConfiguration = new ServiceBusConfiguration();
            new ConfigureFromConfigurationOptions<ServiceBusConfiguration>(
                Configuration.GetSection("ServiceBus"))
                .Configure(serviceBusConfiguration);

            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins",
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            
            services.AddSingleton(serviceBusConfiguration);
            services.AddScoped<ISignupCourseApplication, SignupCourseApplication>();
            services.AddScoped<ISignupCourseRepository, SignupCourseRepository>();
            services.AddScoped<IServiceBusInfrastructure, ServiceBusInfrastructure>();
            services.AddSingleton<IReceiveMessageHandler, ReceiveMessageHandler>();
            services.AddApplicationInsightsTelemetry(Configuration);


            var serviceProvider = services.BuildServiceProvider();
            var receiveMessage = serviceProvider.GetService<IReceiveMessageHandler>();

            var queueClient = new QueueClient(serviceBusConfiguration.ConnectionString, serviceBusConfiguration.QueueName);

            receiveMessage.ReceiveMessageAsync(queueClient);
            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Courses", Version = "v1" });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courses V1");
            });

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigins");
            app.UseMvc();

            



        }
    }
}
