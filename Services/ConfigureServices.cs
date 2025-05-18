namespace Base_API.Services;

public static class ConfigureServices
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<AuthService>();
    }
}
