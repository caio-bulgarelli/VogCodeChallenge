using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VogCodeChallenge.Contracts.Interfaces.Repositories;
using VogCodeChallenge.Contracts.Interfaces.Services;
using VogCodeChallenge.Entities;
using VogCodeChallenge.Repositories;
using VogCodeChallenge.Services;

namespace VogCodeChallenge.API
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
#if DEBUG
            VogContext.InitializeContext();

            services.AddDbContext<VogContext>(opt => opt.UseInMemoryDatabase("Vog"), ServiceLifetime.Singleton);
#else
            services.AddDbContext<VogContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("SqlServerConnString")), ServiceLifetime.Singleton);
#endif

            services.AddSingleton<IGenericRepository<Employee>, EmployeesRepository>();
            services.AddSingleton<IGenericRepository<Department>, DepartmentsRepository>();

            services.AddTransient<IEmployeesService, EmployeesService>();
            services.AddTransient<IDepartmentsService, DepartmentsService>();

            services.AddControllers();

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
