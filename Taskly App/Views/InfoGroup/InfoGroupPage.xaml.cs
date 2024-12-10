using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Taskly_App.Views.InfoGroup
{
    public partial class InfoGroupPage : ContentPage, INotifyPropertyChanged
    {
        private bool _isEditing;

        public string TeamName { get; set; } = "Equipo Alpha";
        public string TeamDescription { get; set; } = "Este es el mejor equipo.";
        public string JoinCode { get; set; } = "ABC123";

        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                OnPropertyChanged();
            }
        }

        public InfoGroupPage()
        {
            InitializeComponent();
            IsEditing = false;
            BindingContext = this;
        }

        // Editar
        private void OnEditClicked(object sender, EventArgs e)
        {
            IsEditing = true;
        }

        // Guardar
        private void OnSaveClicked(object sender, EventArgs e)
        {
            // Aquí podrías guardar los datos, por ejemplo, en un servidor o base de datos.
            IsEditing = false;
        }

        // Cancelar
        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Restaurar los valores originales
            TeamNameEntry.Text = TeamName;
            DescriptionEntry.Text = TeamDescription;
            JoinCodeEntry.Text = JoinCode;
            IsEditing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
