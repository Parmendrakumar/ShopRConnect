using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectLocation : ContentPage
    {
        public ObservableCollection<SearchLoc> location { get; set; } = new ObservableCollection<SearchLoc>();
        //     "Sector 29, Gurugram","MG Road, Gurugram","Sector 31, Gurugram"

        public SelectLocation()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            BindingContext = this;
            // suggestionlistview.ItemsSource = categ;

            location.Add(new SearchLoc { Name = "Saket, New Delhi", TextColor = Color.Red, img = ImageSource.FromFile("TickRed.png") });
            location.Add(new SearchLoc { Name = "MG Road, Gurugram", TextColor = Color.FromHex("#16325c") });
            location.Add(new SearchLoc { Name = "Sector 31, Gurugram", TextColor = Color.FromHex("#16325c") });

        }

       public void Slider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / 5);
            mySlider.Value = newStep * 5;
            lblText.Text = mySlider.Value.ToString() + "km";
            lblText.TranslateTo(mySlider.Value * ((mySlider.Width - 40) / mySlider.Maximum), 0, 100);
        }

        private void ColorSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = ColorSearchBar.Text;
            var suggestion = location.Where(n => n.Name.ToLower().Contains(keyword.ToLower()));
            suggestionlistview.ItemsSource = suggestion;
        }

        public void suggestionlistview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PopAsync();
        }
    }

    public class SearchLoc
    {
        public string Name { get; set; }
        public Color TextColor { get; set; }

        public ImageSource img { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
          //  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}