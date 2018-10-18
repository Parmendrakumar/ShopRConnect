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
    public partial class ViewImageDialog : PopupPage
    {
        string imagepath1 = "";
        public ViewImageDialog(string imagepath)
        {
            InitializeComponent();
            BindingContext = this;
            imagepath1 = imagepath;

            mg.Source = ImageSource.FromFile(imagepath1);
        }
        private async void OnCloseButtonTapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}