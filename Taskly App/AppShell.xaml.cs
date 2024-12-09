using Taskly_App.Services;
using Taskly_App.Views.Login;

namespace Taskly_App
{
    public partial class AppShell : Shell
    {
        private readonly IServiceProvider _serviceProvider;
        public AppShell(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
        }
    }
}
