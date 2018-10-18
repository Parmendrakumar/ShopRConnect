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
    public partial class SelectOpeningHours : ContentPage
    {
        public ObservableCollection<ItemOpening> Items { get; set; } = new ObservableCollection<ItemOpening>();
        public SelectOpeningHours()
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
            BindingContext = this;

            Items.Add(new ItemOpening { Name = "Sunday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });
            Items.Add(new ItemOpening { Name = "Monday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });
            Items.Add(new ItemOpening { Name = "Tuesday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });
            Items.Add(new ItemOpening { Name = "Wednesday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });
            Items.Add(new ItemOpening { Name = "Thursday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });
            Items.Add(new ItemOpening { Name = "Friday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png")});
            Items.Add(new ItemOpening { Name = "Saturday", TextColor = Color.FromHex("#16325c"), img = ImageSource.FromFile("TickGray.png") });

        }

        private void lstView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var dataItem = e.Item as ItemOpening;
                foreach (ItemOpening item in Items)
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
    public class ItemOpening : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public Color TextColor { get; set; }
        public ImageSource img { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged()
        {
           // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }
    }
}