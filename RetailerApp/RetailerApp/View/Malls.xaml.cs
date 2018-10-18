using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Malls : ContentPage
    {
        public ObservableCollection<SearchMall2> malls { get; set; } = new ObservableCollection<SearchMall2>();
        public Malls()
        {
            InitializeComponent();
            BindingContext = this;


            malls.Add(new SearchMall2 { Name = "Select CityWalk Mall", TextColor = Color.Red, img = ImageSource.FromFile("TickRed.png"), TextBtn = "800m" });
            malls.Add(new SearchMall2 { Name = "DLF Place Saket", TextColor = Color.FromHex("#16325c"),  TextBtn = "2km" });
            malls.Add(new SearchMall2 { Name = "DLF Courtyard Mall", TextColor = Color.FromHex("#16325c"),  TextBtn = "3km" });
        }
        private void ColorSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = ColorSearchBar.Text;
            var suggestion = malls.Where(n => n.Name.ToLower().Contains(keyword.ToLower()));
            suggestionlistview.ItemsSource = suggestion;
        }

        public void suggestionlistview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new MallOffer());
        }

        public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //  await Navigation.PushAsync(new SelectLocation());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectLocation());
        }
    }
    public class SearchMall2
    {
        public string Name { get; set; }
        public Color TextColor { get; set; }

        public ImageSource img { get; set; }

        public string TextBtn { get; set; }
    }
}