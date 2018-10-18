using Geolocator.Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        string lction = "";
        string lat = "";
        string lng = "";
        Pin pin;
        public MapPage(string location)
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
            lction = location;
            searchQueryEntry.Text = lction;
          
            
        }

        private async void OnSearch(object sender, EventArgs e)
        {
            var theEnteredAdress = searchQueryEntry.Text;

            Geocoder gc = new Geocoder();
            IEnumerable<Position> result =
                await gc.GetPositionsForAddressAsync(theEnteredAdress);

            foreach (Position pos in result)
            {
                System.Diagnostics.Debug.WriteLine("Lat: {0}, Lng: {1}", pos.Latitude, pos.Longitude);

                gmapsView.MoveToRegion(
              MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(pos.Latitude, pos.Longitude), Distance.FromMiles(1)));

              
                pin = new Pin
                {
                    Type = PinType.Place,
                    Position = new Position(pos.Latitude, pos.Longitude),
                    Address = "ElixirCT",
                    Label = "ShopRConnect"

                };
                gmapsView.Pins.Add(pin);
            }

          

           

        }

        private void OnSearchQueryChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}