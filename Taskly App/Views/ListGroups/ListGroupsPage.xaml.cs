using System.Collections.ObjectModel;

namespace Taskly_App.Views.ListGroups
{
    public partial class ListGroupsPage : ContentPage
    {
        public ObservableCollection<Group> Groups { get; set; }

        public ListGroupsPage()
        {
            InitializeComponent();

            // Simulación de datos de ejemplo
            Groups = new ObservableCollection<Group>
            {
                new Group { Title = "Grupo 1", Description = "Descripción del grupo 1" },
                new Group { Title = "Grupo 2", Description = "Descripción del grupo 2" },
                new Group { Title = "Grupo 3", Description = "Descripción del grupo 3" }
            };

            BindingContext = this;
        }
    }

    public class Group
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
