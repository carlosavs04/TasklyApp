namespace Taskly_App.Views.JoinGroup
{
    public partial class JoinGroupPage : ContentPage
    {
        public JoinGroupPage()
        {
            InitializeComponent();
        }

        private async void OnJoinGroup(object sender, EventArgs e){
         await Navigation.PushAsync(new JoinGroupPage());
        }

        private async void OnGoToCreateGroup(object sender, EventArgs e){
         await Navigation.PushAsync(new JoinGroupPage());
        }
    }
}