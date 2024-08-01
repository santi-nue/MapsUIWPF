using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Limiting;
using Mapsui.Projections;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Tiling;
using Mapsui.UI.Wpf;
using MapsUIWPF.Data.Tools;
using MapsUIWPF.Windows;
using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MapsUIWPF
{
    public partial class MainWindow : Window
    {
        public ShapeFilesList ShapeFiles { get; set; } = new();
        public LayersList Layers { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewLists();


            // Initialize the map asynchronously
            InitializeMapAsync();

            MapView.Map.Refresh();
        }

        public Task<Map> CreateMapAsync()
        {
            Map map = new();

            // Add the tile layer (base map)
            var tileLayer = OpenStreetMap.CreateTileLayer();
            map.Layers.Add(tileLayer);

            // Define pan and zoom limits here if necessary
            var (minX, minY) = SphericalMercator.FromLonLat(-180, -85.0511);
            var (maxX, maxY) = SphericalMercator.FromLonLat(180, 85.0511);
            MRect worldBounds = new MRect(minX, minY, maxX, maxY);

            map.Navigator.Limiter = new ViewportLimiterKeepWithinExtent();
            map.Navigator.RotationLock = true;
            map.Navigator.OverridePanBounds = worldBounds;
            map.Navigator.ZoomToBox(worldBounds);

            
            // Add layer names to the LayersList
            foreach(var layer in map.Layers)
            {
                MessageBox.Show($"Found layer: {layer.Name}");

                if(layer.Name != null)
                    Layers.Layers.Add(layer.Name);
            }
            
            // Attach event handler for map zoom changes
            map.Navigator.ViewportChanged += Event_ViewPortChanged;

            return Task.FromResult(map);
        }

        private async void InitializeMapAsync()
        {
            // Create the map asynchronously
            Map map = await CreateMapAsync();

            // Set the map on the MapControl
            MapView.Map = map;

            // Event to update mouse position in the status bar
            MapView.MouseMove += MapView_MouseMove;
            MapView.MouseWheel += MapView_MouseWheel;
        }

        private void Event_ViewPortChanged(object? sender, EventArgs e)
        {

        }

        private void MapView_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            UpdateStatusText(e);
        }

        private void MapView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            UpdateStatusText(e);
        }

        private void UpdateStatusText(MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(MapView);
            var viewPort = MapView.Map.Navigator.Viewport.ScreenToWorld(mousePosition.X, mousePosition.Y);
            var lonLat = SphericalMercator.ToLonLat(viewPort.X, viewPort.Y);

            MousePosition.Text = $"Latitude: {lonLat.lat:F6}, Longitude: {lonLat.lon:F6}, Zoom {MapView.Map.Navigator.Viewport.Resolution}";
        }

        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            MapView.Map.Navigator.ZoomIn();
        }

        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            MapView.Map.Navigator.ZoomOut();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new();
            aboutWindow.ShowDialog();
        }

        private void Debugger_Click(object sender, RoutedEventArgs e)
        {
            //var debuggerWindow = new DebuggerWindow();
            //debuggerWindow.ShowDialog(); // Show the Debugger window
        }

        private void AddShapefile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new();
            ofd.Filter = "*.shp,*.shape|shape files";
            ofd.Title = "Find Shape File";
            ofd.Multiselect = false;
            ofd.InitialDirectory = @"C:\";
            
//            DialogResult response = ofd.ShowDialog();

            // Code to handle adding shapefiles
            MessageBox.Show("Add Shapefile functionality to be implemented.");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }


    }
}
