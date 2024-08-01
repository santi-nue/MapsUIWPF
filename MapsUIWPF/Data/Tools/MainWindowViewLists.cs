using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsUIWPF.Data.Tools
{
    public class MainWindowViewLists : INotifyPropertyChanged
    {
        public ShapeFilesList ShapeFiles { get; set; }
        public LayersList Layers { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewLists()
        {
            ShapeFiles = new();
            Layers = new();
        }

        

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
