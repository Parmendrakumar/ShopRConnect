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
    public partial class SelectCategory : ContentPage
    {
        public ObservableCollection<SearchCat> categ { get; set; } = new ObservableCollection<SearchCat>();

        public SelectCategory()
        {
            InitializeComponent();

            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            BindingContext = this;
            //  suggestionlistview.ItemsSource = categ;

           
            categ.Add(new SearchCat { Name = "Shirts", TextColor = Color.Red });
            categ.Add(new SearchCat { Name = "Books" });
            categ.Add(new SearchCat { Name = "Mobiles" });
            categ.Add(new SearchCat { Name = "Laptops" });
            categ.Add(new SearchCat { Name = "Home Appliances" });
            categ.Add(new SearchCat { Name = "T-Shirts" });
        }

        private void ColorSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = ColorSearchBar.Text;
            var suggestion = categ.Where(n => n.Name.ToLower().Contains(keyword.ToLower()));
            suggestionlistview.ItemsSource = suggestion;
        }

        public void suggestionlistview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
           // Navigation.PushAsync(new SelectFilters());
        }
    }
    public class SearchCat
    {
        public string Name { get; set; }
        public Color TextColor { get; set; }
    }
}