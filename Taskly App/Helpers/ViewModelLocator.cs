using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskly_App.ViewModels;

namespace Taskly_App.Helpers
{
    public class ViewModelLocator
    {
        private readonly IServiceProvider _serviceProvider;

        public ViewModelLocator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public RegisterViewModel RegisterViewModel => _serviceProvider.GetRequiredService<RegisterViewModel>();
        public LoginViewModel LoginViewModel => _serviceProvider.GetRequiredService<LoginViewModel>();
    }
}
