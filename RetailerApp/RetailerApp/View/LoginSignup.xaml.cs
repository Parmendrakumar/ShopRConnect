using Com.OneSignal;
using Com.OneSignal.Abstractions;
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
    public partial class LoginSignup : ContentPage
    {
        public LoginSignup()
        {
            InitializeComponent();
        }
        public async void Login_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login(), false);
            //   Navigation.RemovePage(this);
            // await Navigation.PushAsync(new TitlePositionPage());
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.Current.Properties["IsLoggedIn"] = false;
            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Alert!", "Do you really want to close the application?", "Yes", "No");
                if (result)
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }
                }
            });
            return true;
        }

        private async void Signup_Clicked(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    Alogin.IsRunning = true;
                    Alogin.IsVisible = true;

                    PlayerIds deviceId = await OneSignal.Current.IdsAvailableAsync();
                    string deviceid = deviceId.PlayerId;

                    string Inputs = "deviceId=" + deviceid.ToString();
                    string resp = await RestService.VerificationCode(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                    string userUniqueId = result;

                    if (result == "Yes")
                    {
                        await DisplayAlert("Alert", "You are already registered", "Ok");
                        Alogin.IsRunning = false;
                        Alogin.IsVisible = false;
                    }
                    else if (result == "0")
                    {
                        //  await Navigation.PushAsync(new Signup1(), false);

                        await Navigation.PushAsync(new Signup("Yes", "Yes"), false);
                        Alogin.IsRunning = false;
                        Alogin.IsVisible = false;
                    }
                    else if (result == "-1")
                    {
                        await Navigation.PushAsync(new ForgotPassword());
                    }
                    else
                    {
                       // await Navigation.PushAsync(new SignupVerification(userUniqueId), false);
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
               await DisplayAlert("Alert", "Please check your Internet Connection", "Ok");
            }
        }
        
        

    }
}