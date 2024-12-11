using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Taskly_App.Views.Tasks.Detail
{
    public partial class DetailTaskPage : ContentPage
    {
        public string NombreTarea { get; set; }
        public string DescripcionTarea { get; set; }
        public string Responsable { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaFinalizacion { get; set; }
        public string CreadaPor { get; set; }

        public ICommand EliminarTareaCommand { get; }

        public DetailTaskPage()
        {
            InitializeComponent();

            // Datos de ejemplo
            NombreTarea = "Diseñar la interfaz de usuario";
            DescripcionTarea = "Crear prototipos para la interfaz de usuario del módulo principal.";
            Responsable = "Bob Smith";
            FechaCreacion = "01/12/2024";
            FechaFinalizacion = "15/12/2024";
            CreadaPor = "Admin";

            // Comando para eliminar la tarea
            EliminarTareaCommand = new Command(async () =>
            {
                bool confirm = await DisplayAlert("Confirmar eliminación", 
                                                  "¿Estás seguro de que deseas eliminar esta tarea?", 
                                                  "Sí", "No");
                if (confirm)
                {
                    // Aquí puedes implementar la lógica para eliminar la tarea
                    await DisplayAlert("Tarea eliminada", "La tarea ha sido eliminada con éxito.", "OK");
                    await Navigation.PopAsync();
                }
            });

            BindingContext = this;
        }
    }
}
