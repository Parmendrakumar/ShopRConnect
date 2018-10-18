using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_RequestRating : PopupPage
    {
        public Popup_RequestRating()
        {
            InitializeComponent();
        }

        public void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            btnno.BackgroundColor = Color.FromHex("#f24245");
            btnno.TextColor = Color.White;
            Navigation.PopPopupAsync();
            //  return false;
        }
        public void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            btnyes.BackgroundColor = Color.FromHex("#f24245");
            btnyes.TextColor = Color.White;
            Navigation.PopPopupAsync();
            //  return false;
        }
    }
}