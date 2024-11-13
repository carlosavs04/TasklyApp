using Taskly_App.Views.Login;
using Taskly_App.Views.Register;

namespace Taskly_App.Views.Login
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

         private async void OnRegisterClicked(object sender, EventArgs e){
            await Navigation.PushAsync(new RegisterPage());
         }

         private async void OnLoginClicked(object sender, EventArgs e){
            Application.Current.MainPage = new AppShell();    
         }
    }

}
