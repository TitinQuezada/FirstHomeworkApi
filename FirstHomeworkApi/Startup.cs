using Boundaries.Persistence;
using Boundaries.Persistence.Repositories;
using Core.Contracts;
using Core.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirstHomeworkApi
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

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddDbContext<FristHomeworkDbContext>((options) =>
           options.UseSqlServer(Configuration.GetConnectionString("dbConnection"),
           (op) => op.MigrationsAssembly("Boundaries.Persistence")));

            InitializeRepositories(services);
            InitializeManagers(services);
        }

        private void InitializeRepositories(IServiceCollection services)
        {
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IUserPhoneRepository, UserPhoneRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();
        }

        private void InitializeManagers(IServiceCollection services)
        {
            services.AddScoped<ApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<MunicipityManager, MunicipityManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");


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
