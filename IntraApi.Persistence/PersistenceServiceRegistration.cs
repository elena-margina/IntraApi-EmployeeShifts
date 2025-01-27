using IntraApi.Application.Contracts.Persistence;
using IntraApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntraApi.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IntraApiDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IntraApiConnectionString"),
                    b => b.MigrationsAssembly("IntraApi.Persistence"))); 


            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
            services.AddScoped<IEmployeeShiftRepository, EmployeeShiftRepository>();
            services.AddScoped<IEmployeeShiftViewRepository, EmployeeShiftViewRepository>();
            services.AddScoped<IUserRepository, UserRepositoy>();

            return services;
        }
    }
}
