using System.Collections.ObjectModel;
using Taskly_App.Models;

namespace Taskly_App.Views.MembersGroup
{
    public partial class MembersGroupPage : ContentPage
    {
        public ObservableCollection<User> Users { get; set; }

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

            BindingContext = this;
        }
    }
}
