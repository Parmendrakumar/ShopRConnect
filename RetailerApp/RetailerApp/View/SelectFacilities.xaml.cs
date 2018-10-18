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
    public partial class SelectFacilities : ContentPage
    {
        public string Gender_Item = "";
        public string Size_Item = "";
        public string Style_Item = "";
        public string SendData = "";       
        public ObservableCollection<Item2> Items { get; set; } = new ObservableCollection<Item2>();     
     
        public SelectFacilities()
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
            BindingContext = this;

            Items.Add(new Item2 { Name = "Wifi", TextColor=Color.FromHex("#16325c"), img=ImageSource.FromFile("TickGray.png"), img2 = ImageSource.FromFile("Wifi.png") });
            Items.Add(new Item2 { Name = "Washrooms", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png"), img2 = ImageSource.FromFile("Washrooms.png") });
            Items.Add(new Item2 { Name = "First-Aids", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png"), img2 = ImageSource.FromFile("medical.png") });
            Items.Add(new Item2 { Name = "Wheelchair Access", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png"), img2 = ImageSource.FromFile("WheelchairAccess.png") });
            Items.Add(new Item2 { Name = "ATM", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png"), img2 = ImageSource.FromFile("ATM.png") });
                      
        }
        private void lstView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            try
            {                              
                var dataItem = e.Item as Item2;              
                foreach (Item2 item in Items)
                {
                    item.img = dataItem.Equals(item) ? ImageSource.FromFile("TickRed.png") : ImageSource.FromFile("TickGray.png");
                    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.FromHex("#16325c");
                    item.OnPropertyChanged();
                }                       

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
        }

   
    }
    public class Item2 : INotifyPropertyChanged
    {
        public string Name { get; set; }
     
        public Color TextColor { get; set; }

        public ImageSource img { get; set; }
        public ImageSource img2 { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
           // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}