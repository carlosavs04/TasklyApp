using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Taskly_App.Helpers;
using Taskly_App.Models.Config;
using Taskly_App.Services;
using Taskly_App.ViewModels;
using Taskly_App.Views.ListGroups;
using Taskly_App.Views.Login;
using Taskly_App.Views.Register;
using Taskly_App.Views.Tabs.AllTasks;
using Taskly_App.Views.Tabs.MyTasks;
using Taskly_App.Views.Tabs.Settings;

namespace Taskly_App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("Taskly_App.appsettings.json");

            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

            builder.Services.Configure<ApiServiceConfig>(builder.Configuration.GetSection("ApiService"));
            builder.Services.AddSingleton<UserPreferences>();
            builder.Services.AddSingleton<ConfigurationService>();
            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<ApiRouterService>();
            builder.Services.AddSingleton<NavigationService>();
            builder.Services.AddSingleton<ViewModelLocator>();

            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<TeamsViewModel>();
            builder.Services.AddTransient<AddTeamViewModel>();
            builder.Services.AddTransient<JoinTeamViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<ForgotPasswordViewModel>();
            builder.Services.AddTransient<TeamDetailsViewModel>();
            builder.Services.AddTransient<UpdatePasswordViewModel>();
            builder.Services.AddTransient<MyTasksViewModel>();
            builder.Services.AddTransient<TeamMembersViewModel>();
            builder.Services.AddTransient<TasksViewModel>();
            builder.Services.AddTransient<AddTaskViewModel>();

            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<ListGroupsPage>();
            builder.Services.AddTransient<AllTasksPage>();
            builder.Services.AddTransient<MyTasksPage>();
            builder.Services.AddTransient<SettingsPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
