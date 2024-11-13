using System.Collections.ObjectModel;

namespace Taskly_App.Views.Tabs.AllTasks
{
    public partial class AllTasksPage : ContentPage
    {
        // Lista observable de tareas
        public ObservableCollection<Task> Tareas { get; set; }

        public AllTasksPage()
        {
            InitializeComponent();

            // Crear datos ficticios para las tareas
            Tareas = new ObservableCollection<Task>
            {
                new Task { Titulo = "Tarea 1", Descripcion = "Descripción de la tarea 1", IsCompleted = true },
                new Task { Titulo = "Tarea 2", Descripcion = "Descripción de la tarea 2", IsCompleted = false },
                new Task { Titulo = "Tarea 3", Descripcion = "Descripción de la tarea 3", IsCompleted = false },
                new Task { Titulo = "Tarea 4", Descripcion = "Descripción de la tarea 4", IsCompleted = true },
                new Task { Titulo = "Tarea 5", Descripcion = "Descripción de la tarea 5", IsCompleted = false }
            };

            // Establecer el contexto de datos para la vista
            BindingContext = this;
        }
    }

    // Clase que representa una tarea
    public class Task
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool IsCompleted { get; set; }  // Propiedad que indica si la tarea está terminada
    }
}
