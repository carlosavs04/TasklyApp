using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskly_App.Services
{

    public class NavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public async Task NavigateToAsync<TPage>() where TPage : Page
        {
            var page = _serviceProvider.GetRequiredService<TPage>();

            if (Application.Current != null && Application.Current.MainPage is NavigationPage navigationPage) 
            { 
                await navigationPage.PushAsync(page);
            }
        }
    }
}
