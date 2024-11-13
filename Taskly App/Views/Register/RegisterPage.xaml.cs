using Taskly_App.Views.JoinGroup;

namespace Taskly_App.Views.Register
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        
        
        private async void OnGoToJoinGroup(object sender, EventArgs e){
        await Navigation.PushAsync(new JoinGroupPage());
        }
    }
}