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
    public partial class Filter : ContentPage
    {
       
        public ObservableCollection<Item> Items { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> GenderItems { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> SizeItems { get; set; } = new ObservableCollection<Item>();
        public ObservableCollection<Item> StyleItems { get; set; } = new ObservableCollection<Item>();

        public Filter()
        {
            InitializeComponent();

            //CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            //CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));

           
            BindingContext = this;

            Items.Add(new Item { Name = "DISTANCE", TextColor = Color.Red });
            Items.Add(new Item { Name = "MALL" });
            Items.Add(new Item { Name = "RETAILER" });
          

            GenderItems.Add(new Item { GenderName = "Within a km", img = ImageSource.FromFile("unselected.png") });
            GenderItems.Add(new Item { GenderName = "Within 5 km", img = ImageSource.FromFile("unselected.png") });
            GenderItems.Add(new Item { GenderName = "Within 10 km", img = ImageSource.FromFile("unselected.png") });
            GenderItems.Add(new Item { GenderName = "Within 15 km", img = ImageSource.FromFile("unselected.png") });
            GenderItems.Add(new Item { GenderName = "Any distance", img = ImageSource.FromFile("unselected.png") });

            SizeItems.Add(new Item { SizeName = "Select CityWalk" });
        

           // StyleItems.Add(new Item { StyleName = "Casual" });
     



            //  dataItem.TextColor = Color.Red;



        }


        private void lstView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            try
            {


                //   string itm = e.SelectedItem.ToString();
                var dataItem = e.Item as Item;

                //  dataItem.TextColor = Color.Red;
                foreach (Item item in Items)
                {
                    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Gray;
                    item.OnPropertyChanged();
                }

                // update listView
                // dataItem.OnPropertyChanged();


                if (dataItem.Name == "DISTANCE")
                {
                    lstView2.IsVisible = true;
                    lstView3.IsVisible = false;                    
                    lstView4.IsVisible = false;
                }
                else if (dataItem.Name == "MALL")
                {
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = true;
                    lstView4.IsVisible = false;
                }
                else if (dataItem.Name == "RETAILER")
                {
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = true;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
        }

        private void lstView2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as Item;
            //  dataItem.TextColor = Color.Red;
            foreach (Item item in GenderItems)
            {
                item.img = dataItem.Equals(item) ? ImageSource.FromFile("selected.png") : ImageSource.FromFile("unselected.png");
                item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Gray;
                item.OnPropertyChanged();
            }
            dataItem.OnPropertyChanged();
        }

        private void lstView3_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as Item;

            //foreach (Item item in SizeItems)
            //{
            //    item.img = dataItem.Equals(item) ? ImageSource.FromFile("TickRed.png") : ImageSource.FromFile("unselected.png");
            //    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Black;
            //    item.OnPropertyChanged();
            //}

            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();

        }

        private void lstView4_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as Item;

            //  dataItem.TextColor = Color.Red;

            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }

    public class Item : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string GenderName { get; set; }
        public string SizeName { get; set; }
        public string StyleName { get; set; }
        public Color TextColor { get; set; }

        public ImageSource img { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}