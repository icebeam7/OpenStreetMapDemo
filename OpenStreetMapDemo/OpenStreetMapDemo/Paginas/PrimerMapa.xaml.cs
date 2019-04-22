using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Mapsui.Utilities;
using Mapsui.Widgets;
using Mapsui.Widgets.ScaleBar;
using Mapsui.Widgets.Zoom;
using OpenStreetMapDemo.Models;
using OpenStreetMapDemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Color = Mapsui.Styles.Color;
using Point = Mapsui.Geometries.Point;

namespace OpenStreetMapDemo.Paginas
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrimerMapa : ContentPage
	{
        List<Lugar> lugares = Servicios.ServicioBD.ObtenerLugares();

        public PrimerMapa ()
		{
			InitializeComponent ();

            var mapControl = new MapsUIView();
            mapControl.NativeMap.Layers.Add(OpenStreetMap.CreateTileLayer());

            mapControl.NativeMap.Widgets.Add(new ScaleBarWidget(mapControl.NativeMap)
            {
                TextAlignment = Alignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            });

            mapControl.NativeMap.Widgets.Add(new ZoomInOutWidget(mapControl.NativeMap)
            {
                MarginX = 20, MarginY = 40
            });

            var gdl = lugares.First(x => x.Nombre == "Guadalajara");

            var coordenada = new Point(gdl.Longitud, gdl.Latitud);
            var coordenadaMercator = SphericalMercator.FromLonLat(coordenada.X, coordenada.Y);
            mapControl.NativeMap.NavigateTo(coordenadaMercator);
            mapControl.NativeMap.NavigateTo(mapControl.NativeMap.Resolutions[9]);

            var layer = GenerateIconLayer();
            mapControl.NativeMap.Layers.Add(layer);
            mapControl.NativeMap.InfoLayers.Add(layer);

            mapControl.NativeMap.Info += (sender, args) =>
            {
                var layername = args.MapInfo.Layer?.Name;
                var featureLabel = args.MapInfo.Feature?["Label"]?.ToString();
                var featureType = args.MapInfo.Feature?["Type"]?.ToString();

                if (!string.IsNullOrWhiteSpace(featureLabel))
                {
                    ShowPopup(featureLabel, featureType);
                }

                Debug.WriteLine("Info Event was invoked.");
                Debug.WriteLine("Layername: " + layername);
                Debug.WriteLine("Feature Label: " + featureLabel);
                Debug.WriteLine("Feature Type: " + featureType);

                Debug.WriteLine("World Postion: {0:F4} , {1:F4}", args.MapInfo.WorldPosition?.X, args.MapInfo.WorldPosition?.Y);
                Debug.WriteLine("Screen Postion: {0:F4} , {1:F4}", args.MapInfo.ScreenPosition?.X, args.MapInfo.ScreenPosition?.Y);
            };

            ContentGrid.Children.Add(mapControl);
        }

        async void ShowPopup(string feature, string type)
        {
            await DisplayAlert("Información", $"Hiciste click en: {feature} - {type}", "OK");
        }

        private ILayer GenerateIconLayer()
        {
            var layername = "Capa Local";

            return new Layer(layername)
            {
                Name = layername,
                DataSource = new MemoryProvider(GetIconFeatures()),
                Style = new SymbolStyle
                {
                    SymbolScale = 0.8,
                    Fill = new Brush(Color.Green),
                    Outline = { Color = Color.Black, Width = 1 }
                }
            };
        }

        private Features GetIconFeatures()
        {
            var features = new Features();

            var ctz = lugares.First(x => x.Nombre == "Cortazar");
            var vil = lugares.First(x => x.Nombre == "Villagran");
            var val = lugares.First(x => x.Nombre == "Valle de Santiago");

            var feature_Gto = new Feature
            {
                Geometry = new Polygon(new LinearRing(new[] 
                {
                    SphericalMercator.FromLonLat(ctz.Longitud, ctz.Latitud),
                    SphericalMercator.FromLonLat(vil.Longitud, vil.Latitud),
                    SphericalMercator.FromLonLat(val.Longitud, val.Latitud),
                    SphericalMercator.FromLonLat(ctz.Longitud, ctz.Latitud),
                })),
                ["Label"] = "Guanajuato",
                ["Type"] = "Estado"
            };

            var usa = lugares.Where(x => x.Nombre.Contains("Estados Unidos"));
            var Points = new List<Point>();

            foreach (var point in usa)
                Points.Add(SphericalMercator.FromLonLat(point.Longitud, point.Latitud));

            var feature_USA = new Feature
            {
                Geometry = new Polygon(new LinearRing(Points)),
                ["Label"] = "USA",
                ["Type"] = "Pais"
            };

            features.Add(feature_Gto);
            features.Add(feature_USA);

            return features;
        }
    }
}