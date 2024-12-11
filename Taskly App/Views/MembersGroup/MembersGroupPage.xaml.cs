using System.Collections.ObjectModel;
using System.Windows.Input;
using Taskly_App.Models;

namespace Taskly_App.Views.MembersGroup
{
    public partial class MembersGroupPage : ContentPage
    {
        public ObservableCollection<User> Users { get; set; }
        public ICommand DeleteUserCommand { get; }

        public MembersGroupPage()
        {
            InitializeComponent();

            // Datos falsos
            Users = new ObservableCollection<User>
            {
                new User { Id = 1, Username = "John Doe", Email = "johndoe@example.com" },
                new User { Id = 2, Username = "Jane Smith", Email = "janesmith@example.com" },
                new User { Id = 3, Username = "Carlos Rivera", Email = "carlosrivera@example.com" },
                new User { Id = 4, Username = "Alicia González", Email = "alicia@example.com" }
            };

            DeleteUserCommand = new Command<User>(async (user) =>
            {
                bool confirm = await DisplayAlert("Confirmar eliminación",
                                                  $"¿Estás seguro de que deseas eliminar a {user.Username}?",
                                                  "Sí", "No");
                if (confirm)
                {
                    Users.Remove(user);
                }
            });

            BindingContext = this;
        }
    }
}
