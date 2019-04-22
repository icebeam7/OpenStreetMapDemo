namespace OpenStreetMapDemo.Views
{
    public class MapsUIView : Xamarin.Forms.View
    {
        public Mapsui.Map NativeMap { get; }

        protected internal MapsUIView()
        {
            NativeMap = new Mapsui.Map();
            NativeMap.BackColor = Mapsui.Styles.Color.Black;
        }
    }
}
