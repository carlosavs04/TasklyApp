using Taskly_App.Services;
using Taskly_App.Views.ListGroups;
using Taskly_App.Views.Login;
namespace Taskly_App
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            var configService = serviceProvider.GetRequiredService<ConfigurationService>();
            var token = configService.GetAuthToken();

            if (string.IsNullOrEmpty(token) || token == "auth_token")
                MainPage = new NavigationPage(new LoginPage(_serviceProvider));
            else
                MainPage = new NavigationPage(new ListGroupsPage(_serviceProvider));
        }
    }
}
