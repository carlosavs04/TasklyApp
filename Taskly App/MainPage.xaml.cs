using Taskly_App.Views.Register;

namespace Taskly_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e){
        await Navigation.PushAsync(new RegisterPage());
        }
    }

}
