using Taskly_App.Views.Tabs.MyTasks;
using System.Collections.ObjectModel;

namespace Taskly_App.Views.Tabs.MyTasks
{
    public partial class MyTasksPage : ContentPage
    {
        // Lista observable de tareas
        public ObservableCollection<Tarea> Tareas { get; set; }

        public MyTasksPage()
        {
            InitializeComponent();

            // Crear datos ficticios para las tareas
            Tareas = new ObservableCollection<Tarea>
            {
                new Tarea { IsChecked = false, Titulo = "Comprar víveres", Descripcion = "Ir al supermercado a comprar frutas y verduras." },
                new Tarea { IsChecked = true, Titulo = "Llamar a Juan", Descripcion = "Confirmar la reunión con Juan." },
                new Tarea { IsChecked = false, Titulo = "Estudiar matemáticas", Descripcion = "Repasar ejercicios de álgebra y geometría." },
                new Tarea { IsChecked = false, Titulo = "Hacer ejercicio", Descripcion = "Ir al gimnasio a hacer ejercicios de cardio." },
                new Tarea { IsChecked = false, Titulo = "Leer un libro", Descripcion = "Leer el libro de ciencia ficción que compré." },
                new Tarea { IsChecked = false, Titulo = "Hacer la tarea", Descripcion = "Hacer la tarea de matemáticas y entregarla mañana." },
                new Tarea { IsChecked = false, Titulo = "Lavar el auto", Descripcion = "Lavar el auto y aspirar el interior." },
                new Tarea { IsChecked = false, Titulo = "Cocinar la cena", Descripcion = "Preparar la cena para la familia." }
            };

            // Establecer el contexto de datos para la vista
            BindingContext = this;
        }
    }

    // Clase que representa una tarea
    public class Tarea
    {
        public bool IsChecked { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }
}
