using RetailerApp.Data;
using Rg.Plugins.Popup.Extensions;
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
    public partial class SignupVerification : ContentPage
    {
        string useruniqueID = "";
        public SignupVerification(string UserId)
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            useruniqueID = UserId;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Boolean boolisValid = await IsValid();
                string verifycode = verify.Text;

                if (boolisValid)
                {
                    Alogin.IsRunning = true;
                    Alogin.IsVisible = true;

                    string Inputs = "UserUniqueID=" + useruniqueID.ToString() + "&GoogleVerificationCode=" + verifycode.ToString();
                    string resp = await RestService.VerificationInsert(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                    if (result == "1")
                    {
                        string pageName = "setpassword";
                        // await Navigation.PushAsync(new PopupOTPDialog(useruniqueID), false);
                        var _Popupspec = new PopupOTPDialog(useruniqueID, pageName);
                        await Navigation.PushPopupAsync(_Popupspec);
                    }
                    else
                    {
                        await DisplayAlert("Alert", "Network Issue", "Ok");
                    }
                   
                }
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }
        }

        public async Task<Boolean> IsValid()
        {
            if (verify.Text == null)
            {
                await DisplayAlert("Alert", "Please Enter Google verification Code", "Ok");
                verify.Focus();
                return false;
            }

            return true;
        }
    }
}