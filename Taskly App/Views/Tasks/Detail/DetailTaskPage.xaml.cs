using Taskly_App.Views.Tasks.Detail;
using System.Collections.ObjectModel;

namespace Taskly_App.Views.Tasks.Detail
{
    public partial class DetailTaskPage : ContentPage
    {
        public ObservableCollection<Encargado> Encargados { get; set; }
        public Encargado EncargadoSeleccionado { get; set; }

        public DetailTaskPage()
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

    }

    public class Encargado
    {
        public string Nombre { get; set; }
        public string Icono { get; set; }
    }
}