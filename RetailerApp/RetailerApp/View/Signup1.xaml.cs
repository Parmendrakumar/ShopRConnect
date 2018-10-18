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
    public partial class Signup1 : ContentPage
    {
        public Signup1()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {

                Boolean boolisValid = await IsValid();

                if (boolisValid)
                {
                    Alogin.IsRunning = true;
                    Alogin.IsVisible = true;

                    string goofleacc = businessaccount.Items[businessaccount.SelectedIndex].ToString();
                    string websit = website.Items[website.SelectedIndex].ToString();

                    await Navigation.PushAsync(new Signup(goofleacc, websit));
                    Alogin.IsRunning = false;
                    Alogin.IsVisible = false;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }
        }
        private async void ToolLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }
        public async Task<Boolean> IsValid()
        {
            if (businessaccount.SelectedIndex == 0)
            {
                var answer = await DisplayAlert("", "Please Select Google Business Account", null, "Ok");
                businessaccount.Focus();
                return false;
            }
            if (website.SelectedIndex == 0)
            {
                var answer = await DisplayAlert("", "Please Select Website", null, "Ok");
                website.Focus();
                return false;
            }
            return true;
        }

    }
}