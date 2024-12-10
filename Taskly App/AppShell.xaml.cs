using Taskly_App.Services;
using Taskly_App.Views.Login;
using Taskly_App.Views.Tabs.AllTasks;
using Taskly_App.Views.Tabs.MyTasks;
using Taskly_App.Views.Tabs.Settings;

namespace Taskly_App
{
    public partial class AppShell : Shell
    {
        private readonly IServiceProvider _serviceProvider;
        public AppShell(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;
            InitTabs();
        }

        public void InitTabs()
        {
            var myTasksPage = _serviceProvider.GetRequiredService<MyTasksPage>();
            var allTasksPage = _serviceProvider.GetRequiredService<AllTasksPage>();
            var settingsPage = _serviceProvider.GetRequiredService<SettingsPage>();

            myTasksTab.Items.Add(new ShellContent { Content = myTasksPage });
            allTasksTab.Items.Add(new ShellContent { Content = allTasksPage });
            settingsTab.Items.Add(new ShellContent { Content = settingsPage });

            CurrentItem = myTasksTab;
        }
    }
}
