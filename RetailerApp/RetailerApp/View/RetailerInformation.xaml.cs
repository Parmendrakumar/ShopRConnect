using Geolocator.Plugin;
using RetailerApp.Model;
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
    public partial class RetailerInformation : ContentPage
    {

        string lat = "";
        string lng = "";
        Pin pin;
        public RetailerInformation()
        {

            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            //  RetrieveLocation();
            Getmap();



        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
             
        }

        private async void Getmap()
        {
            Geocoder gc = new Geocoder();
            IEnumerable<Position> result =
                await gc.GetPositionsForAddressAsync(address.Text);

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
        private async Task RetrieveLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);

            lat = position.Latitude.ToString();
            lng = position.Longitude.ToString();

            gmapsView.MoveToRegion(
               MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));

            double lt = System.Convert.ToDouble(lat);
            double lg = System.Convert.ToDouble(lng);

            pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(lt, lg),
                Address = "ElixirCT",
                Label = ""
                   
                };
                gmapsView.Pins.Add(pin);
           

          

        }


        private async void Facilities_Focused(object sender, FocusEventArgs e)
        {
            await Navigation.PushAsync(new SelectFacilities());
        }

        private async void Category_Focused(object sender, FocusEventArgs e)
        {
            await Navigation.PushAsync(new SelectCategory());
        }

        private async void Sub_Category_Focused(object sender, FocusEventArgs e)
        {
            await Navigation.PushAsync(new SelectSubCategory());
        }

        private async void OpeningHours_Focused(object sender, FocusEventArgs e)
        {
            await Navigation.PushAsync(new SelectOpeningHours());
        }
      
        private void address_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new MapPage(address.Text));
        }
    }
}