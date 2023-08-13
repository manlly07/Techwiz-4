using SoccerClub.Services;

namespace SoccerClub.Configurations
{
    public static class ServicesConfiguration
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFeedbackService, FeedbackService>();

            services.AddControllersWithViews();

        }
    }
}
