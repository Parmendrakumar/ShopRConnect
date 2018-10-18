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
    public partial class Notification : ContentPage
    {
        public Notification()
        {
            InitializeComponent();
            BindingContext = new NotificationBindingClass();
           
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            if ((activelistview.ItemsSource as ObservableCollection<NotificationName>).Count == 0)
            {
                activelistview.IsVisible = false;
                emptylist.IsVisible = true;
            }
            else
            {
                activelistview.IsVisible = true;
                emptylist.IsVisible = false;
            }



        }
    }

   class NotificationBindingClass
    {
        public ObservableCollection<NotificationName> active { get; set; }
      
        public NotificationBindingClass()
        {
            active = new ObservableCollection<NotificationName>();

           // active.Add(new NotificationName { Id = "1", Dt = "Today, 1h", Name = "Customer is looking for Men Shirts XS Nike Black" });
          //  active.Add(new NotificationName { Id = "2", Dt = "Today, 2h", Name = "Customer is looking for Women Shirts XS Nike Black" });


        }


    }
    public class NotificationName
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dt { get; set; }
      
        public Color TextColorA { get; set; }
        public Color TextColorI { get; set; }


    }
}