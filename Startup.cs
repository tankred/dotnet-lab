using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace dotnet_lab
{
    public class Startup
    {
        private string _moviesApiKey = null;
	private string _connection = null;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            _moviesApiKey = Configuration["Movies:ServiceApiKey"];
            var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("Movies"));
            builder.Password = Configuration["DbPassword"];
            _connection = builder.ConnectionString;

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
                await context.Response.WriteAsync(" - ");
                var result = string.IsNullOrEmpty(_moviesApiKey) ? "Null" : "Not Null";
                await context.Response.WriteAsync(" - ");
                await context.Response.WriteAsync($"Secret is {result}");
                await context.Response.WriteAsync(" - ");
                await context.Response.WriteAsync($"DB Connection: {_connection}");
                await context.Response.WriteAsync(" - ");
            });
        }
    }
}
