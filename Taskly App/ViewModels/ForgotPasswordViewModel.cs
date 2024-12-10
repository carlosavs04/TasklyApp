using System.Windows.Input;

namespace Taskly_App.ViewModels
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public ICommand SendEmailCommand { get; }

        public ForgotPasswordViewModel()
        {
            SendEmailCommand = new Command(async () => await SendEmailAsync());
        }

        private async Task SendEmailAsync()
        {
            if (string.IsNullOrWhiteSpace(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor, ingresa tu correo electrónico.", "OK");
                return;
            }

            // Aquí iría la lógica para enviar el correo electrónico
            await Application.Current.MainPage.DisplayAlert("Éxito", "Correo enviado. Revisa tu bandeja de entrada.", "OK");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
