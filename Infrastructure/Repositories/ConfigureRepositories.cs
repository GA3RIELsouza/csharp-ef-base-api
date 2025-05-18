using Base_API.Infrastructure.Interfaces;

namespace Base_API.Infrastructure.Repositories
{
    public static class ConfigureServices
    {
        public static void AddRepositories(this IServiceCollection repositories)
        {
            repositories.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
