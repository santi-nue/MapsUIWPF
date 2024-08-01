using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MapsUIWPF.Data.Tools
{
    public class LayersList : INotifyPropertyChanged
    {
        private ObservableCollection<string> _layers = new ObservableCollection<string>();

        public ObservableCollection<string> Layers
        {
            get => _layers;
            set
            {
                _layers = value;
                OnPropertyChanged(nameof(_layers));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
