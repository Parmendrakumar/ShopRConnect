using Plugin.Connectivity;
using RetailerApp.Data;
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
    public partial class SetPassword : ContentPage
    {
        string userId = "";
        public SetPassword(string userid)
        {
            userId = userid;

            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            password.Completed += async (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        Boolean boolisValid = await IsValid();
                        if (boolisValid)
                        {
                            DoneClick();
                        }
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
            };
            cpassword.Completed += async (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        Boolean boolisValid = await IsValid();
                        if (boolisValid)
                        {
                            DoneClick();
                        }
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
            };
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Boolean boolisValid = await IsValid();
                if (boolisValid)
                {
                    DoneClick();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        public async void DoneClick()
        {
            Alogin.IsRunning = true;
            Alogin.IsVisible = true;
            var isPasswordValid = App.Current.Properties.ContainsKey("IsPasswordValid") ? (bool)App.Current.Properties["IsPasswordValid"] : false;
            var isCPasswordValid = App.Current.Properties.ContainsKey("IsCPasswordValid") ? (bool)App.Current.Properties["IsCPasswordValid"] : false;

            if (isPasswordValid)
            {
                if (isCPasswordValid)
                {
                    try
                    {

                        string password1 = password.Text;
                        string Inputs = "UserID=" + userId + "&Password=" + password1;
                        string resp = await RestService.RegistrationSetPassword(Inputs);
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                        if (result == "1")
                        {
                            //  await Navigation.PushAsync(new UserProfile(userId));
                            await Navigation.PopToRootAsync();
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Something wrong", "Ok");
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        Alogin.IsRunning = false;
                        Alogin.IsVisible = false;
                    }

                }
                else
                {
                    await DisplayAlert("", "Incorrect Confirm Password", "Ok");
                    Alogin.IsRunning = false;
                    Alogin.IsVisible = false;
                }
            }
            else
            {
                await DisplayAlert("", "Password can't be Empty", "Ok");
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }

            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }

        public async Task<Boolean> IsValid()
        {
            if (password.Text == null || password.Text == "")
            {
                await DisplayAlert("Alert", "Please Enter Password", "Ok");
                password.Focus();
                return false;
            }

            else if (cpassword.Text == null || cpassword.Text == "")
            {
                await DisplayAlert("Alert", "Please Enter Conform Password", "Ok");
                password.Focus();
                return false;
            }

            return true;
        }

        private async void ToolSignup_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new Signup1());
        }

       
    }
}