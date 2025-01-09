using labs_ud.Application.Create;
using labs_ud.Application.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace labs_ud.Application;

public static class Inject
{
    // public static IServiceCollection AddInfrastructure(
    //     this IServiceCollection services,
    //     IConfiguration configuration)
    // {
    //     services.AddScoped<ApplicationDbContext>();
    //     services.AddScoped<CourseRepository,CourseRepository>();
    //     return services;
    // }
    
    // public static IServiceCollection AddApplication(this IServiceCollection services)
    // {
    //     services.AddScoped<CreateCourseService>();
    //
    //     return services;
    // }
}