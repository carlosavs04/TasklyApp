using Taskly_App.Views.Tasks.Edit;
using System.Collections.ObjectModel;

namespace Taskly_App.Views.Tasks.Edit
{
    public partial class EditTaskPage : ContentPage
    {
        public ObservableCollection<Encargado> Encargados { get; set; }
        public Encargado EncargadoSeleccionado { get; set; }

        public EditTaskPage()
        {
            InitializeComponent();

            Encargados = new ObservableCollection<Encargado>
            {
                new Encargado { Nombre = "Ninguno", Icono = "icono_ninguno.png" },
                new Encargado { Nombre = "Bob Smith", Icono = "icono_persona.png" },
                new Encargado { Nombre = "Carol Williams", Icono = "icono_persona.png" },
                new Encargado { Nombre = "David Johnson", Icono = "icono_persona.png" },
                new Encargado { Nombre = "Emily Brown", Icono = "icono_persona.png" }
            };

            EncargadoSeleccionado = Encargados[0];
            BindingContext = this;
        }

        private async void OnActualizarTareaClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Tarea Actualizada", $"Encargado seleccionado: {EncargadoSeleccionado?.Nombre ?? "Ninguno"}", "OK");
            await Navigation.PopAsync();
        }
    }

    public class Encargado
    {
        public string Nombre { get; set; }
        public string Icono { get; set; }
    }
}