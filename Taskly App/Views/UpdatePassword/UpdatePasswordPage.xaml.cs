using System.Collections.ObjectModel;
using Taskly_App.Models;

namespace Taskly_App.Views.UpdatePassword
{
    public partial class UpdatePasswordPage : ContentPage
    {
        public UpdatePasswordPage()
        {
            InitializeComponent();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            string currentPassword = CurrentPasswordEntry.Text;
            string newPassword = NewPasswordEntry.Text;
            string confirmPassword = ConfirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(currentPassword) ||
                string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            if (newPassword != confirmPassword)
            {
                await DisplayAlert("Error", "La nueva contraseña y su confirmación no coinciden.", "OK");
                return;
            }

            // Aquí puedes implementar la lógica para actualizar la contraseña.
            await DisplayAlert("Éxito", "Contraseña actualizada correctamente.", "OK");
        }

        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
