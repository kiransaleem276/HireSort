using HireSort.Repository.Implementation;
using HireSort.Repository.Interface;

namespace HireSort.Dependencies
{
    public static class ServiceDependency
    {
        public static void AddServiceDependency(this IServiceCollection services)
        {
            services.AddScoped<IResumeParsing, ResumeParsing>();

        }
    }
}
