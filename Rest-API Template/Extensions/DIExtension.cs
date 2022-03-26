using Repositories.UnitOfWork;

namespace Rest_API_Template.Extensions
{
    public static class DIExtension
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
