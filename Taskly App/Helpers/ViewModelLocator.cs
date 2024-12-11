﻿using System;
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
        public TeamsViewModel TeamsViewModel => _serviceProvider.GetRequiredService<TeamsViewModel>();
        public AddTeamViewModel AddTeamViewModel => _serviceProvider.GetRequiredService<AddTeamViewModel>();
        public JoinTeamViewModel JoinTeamViewModel => _serviceProvider.GetRequiredService<JoinTeamViewModel>();
        public SettingsViewModel SettingsViewModel => _serviceProvider.GetRequiredService<SettingsViewModel>();
        public ForgotPasswordViewModel ForgotPasswordViewModel => _serviceProvider.GetRequiredService<ForgotPasswordViewModel>();
        public TeamDetailsViewModel TeamDetailsViewModel => _serviceProvider.GetRequiredService<TeamDetailsViewModel>();
        public UpdatePasswordViewModel UpdatePasswordViewModel => _serviceProvider.GetRequiredService<UpdatePasswordViewModel>();
        public MyTasksViewModel MyTasksViewModel => _serviceProvider.GetRequiredService<MyTasksViewModel>();
    }
}
