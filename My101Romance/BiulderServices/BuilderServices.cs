using My101Romance.DAL.Interfaces;
using My101Romance.DAL.Repositories;
using My101Romance.Services.Implementations;
using My101Romance.Services.Interfaces;

public static class BuilderServices
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IQuizService, QuizService>();
    }
    
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
    }
}