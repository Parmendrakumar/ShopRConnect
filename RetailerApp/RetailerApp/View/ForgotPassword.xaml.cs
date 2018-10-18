using Plugin.Connectivity;
using RetailerApp.Data;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPassword : ContentPage
    {
        string Userid = "";
        string MobileNumber = "";
        string pagename2 = "forgetpassword";
        public ForgotPassword()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            mobilenumber.Completed += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        ClickSubmit();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Please check your internet connection", "OK");
                }

            };
        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // ClickSubmit();
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    ClickSubmit();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                await DisplayAlert("Alert", "Please check your internet connection", "OK");
            }
        }
        public async void ClickSubmit()
        {
            Alogin.IsRunning = true;
            Alogin.IsVisible = true;
          
            try
            {
             //   var isMobileValid = App.Current.Properties.ContainsKey("IsMobileValid") ? (bool)App.Current.Properties["IsMobileValid"] : false;
                Boolean boolisValid = await IsValid();
                MobileNumber = mobilenumber.Text;

                if (boolisValid)
                {
                    string Inputs = "Mobile=" + MobileNumber;
                    string resp = await RestService.ForgotPassword(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                    Userid = result;

                    if (Userid == "0")
                    {                       
                        Alogin.IsRunning = false;
                        Alogin.IsVisible = false;
                        await DisplayAlert("Alert", "Mobile number doesn't exist", "Ok");
                       // await Navigation.PopToRootAsync();
                    }
                    else if (Userid == "-1")
                    {
                        Alogin.IsRunning = false;
                        Alogin.IsVisible = false;
                        await DisplayAlert("Alert", "Please verify your google account", "Ok");
                        await Navigation.PopToRootAsync();
                    }
                    else
                    {
                        string pageName = "forgetpassword";
                        var _Popupspec = new PopupOTPDialog(Userid, pageName);
                        await Navigation.PushPopupAsync(_Popupspec);

                    }
                }
                else
                {
                    Alogin.IsRunning = false;
                    Alogin.IsVisible = false;
                   // await DisplayAlert("Alert","Invalid Mobile Number","Ok");
                }
                            
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }
            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }

        public async Task<Boolean> IsValid()
        {
            if (mobilenumber.Text == null || mobilenumber.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Mobile Number", "Ok");
                mobilenumber.Focus();
                return false;
            }
            else if (!Regex.Match(mobilenumber.Text, @"^[6-9][0-9]{9}$").Success)
            {
                await DisplayAlert("", "Please enter valid Mobile number", null, "Ok");
                mobilenumber.Focus();
                return false;
            }

            return true;
        }
        private async void ToolLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void mobilenumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int _limit = 10;
            string _text = mobilenumber.Text;
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                mobilenumber.Text = _text;        //Set the Old value
            }
        }
    }
}