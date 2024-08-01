using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MapsUIWPF.Data.Tools
{
    public class ShapeFilesList : INotifyPropertyChanged
    {
        private ObservableCollection<string> _shapeFiles = new ObservableCollection<string>();

        public ObservableCollection<string> ShapeFiles
        {
            get => _shapeFiles;
            set
            {
                _shapeFiles = value;
                OnPropertyChanged(nameof(ShapeFiles));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
