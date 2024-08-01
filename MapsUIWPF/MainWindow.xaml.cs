using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Wpf;
using MapsUIWPF.Data.Tools;
using MapsUIWPF.Windows;
using System.Windows;

namespace MapsUIWPF
{
    public partial class MainWindow : Window
    {
        public ShapeFilesList ShapeFiles { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ShapeFiles = new();
            DataContext = ShapeFiles;

            ShapeFiles.ShapeFiles.Add("Test");
            ShapeFiles.ShapeFiles.Add("Test2");

            InitializeMap();
        }

        private void InitializeMap()
        {
            // Create the map and set the projection
            Map map = new Map
            {
                CRS = "EPSG:3857" // Spherical Mercator projection
            };

            
            // Add OpenStreetMap layer
            var osmLayer = OpenStreetMap.CreateTileLayer();
            map.Layers.Add(osmLayer);

            // Set the map on the MapControl
            MapView.Map = map;

            // Event to update mouse position in the status bar
            MapView.MouseMove += MapView_MouseMove;
            SetZoomLock(map);
        }

        private void MapView_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var mousePosition = e.GetPosition(MapView);

            var viewport = MapView.Map.Navigator.Viewport.ScreenToWorld(mousePosition.X, mousePosition.Y);

            var lonLat = SphericalMercator.ToLonLat(viewport.X, viewport.Y);

            MousePosition.Text = $"Latitude: {lonLat.lat:F6}, Longitude: {lonLat.lon:F6}, Zoom {MapView.Map.Navigator.Viewport.Resolution}";
        }

        private void MapView_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(MapView);

            var viewport = MapView.Map.Navigator.Viewport.ScreenToWorld(mousePosition.X, mousePosition.Y);

            var lonLat = SphericalMercator.ToLonLat(viewport.X, viewport.Y);

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

            //
            //var aboutWindow = new AboutWindow();
            //aboutWindow.ShowDialog(); // Show the About window
        }

        private void Debugger_Click(object sender, RoutedEventArgs e)
        {
            //var debuggerWindow = new DebuggerWindow();
            //debuggerWindow.ShowDialog(); // Show the Debugger window
        }

        private void AddShapefile_Click(object sender, RoutedEventArgs e)
        {
            // Code to handle adding shapefiles
            MessageBox.Show("Add Shapefile functionality to be implemented.");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void SetZoomLock(Map map)
        {
            // Define the full world bounds in EPSG:3857 (Spherical Mercator)
            var worldBounds = new MRect(
                -20037508.3427892, // Min X (left)
                -20037508.3427892, // Min Y (bottom)
                20037508.3427892,  // Max X (right)
                20037508.3427892   // Max Y (top)
            );

            // Define the zoom bounds (minimum and maximum zoom levels)
            var zoomBounds = new MMinMax(1, 18); // Example zoom levels, can be adjusted

            // Set the limiter with pan bounds and zoom bounds
            map.Navigator.Limiter.Limit(map.Navigator.Viewport, worldBounds, zoomBounds);

        }
    }
}
