using Plugin.Connectivity;
using RetailerApp.Data;
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
    public partial class PopupOTPDialog : PopupPage
    {
        string pageinfo = "";
        string userid = "";
        string mobileNumber = "";
        public PopupOTPDialog(string UserID, string pagename)
        {
          

            InitializeComponent();

            pageinfo = pagename;
            userid = UserID;

            otpnumber.Completed += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        SubmitData();
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
            try
            {
                SubmitData();
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async void SubmitData()
        {
            Boolean boolisValid = await IsValid();
            if (boolisValid)
            {
                Alogin.IsRunning = true;
                Alogin.IsVisible = true;

                if (pageinfo == "forgetpassword")
                {

                    try
                    {
                        string OTPNumber = otpnumber.Text;
                        string Inputs = "UserID=" + userid + "&OTP=" + OTPNumber;
                        string resp = await RestService.RegistrationOTP(Inputs);
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);


                        if (result == "1")
                        {
                            await Navigation.PopPopupAsync();
                            await Navigation.PushAsync(new ReSetPassword(userid));
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else if (result == "0")
                        {
                            await DisplayAlert("Alert", "OTP Expired Please Resend", "Ok");
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else if (result == "-1")
                        {
                            await DisplayAlert("Alert", "Wrong OTP", "Ok");
                            // await Navigation.PopPopupAsync();
                            //  await Navigation.PushAsync(new SetPassword(userid));
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Network Issue..!", "Ok");
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
                else if (pageinfo == "setpassword")
                {
                    try
                    {
                        string OTPNumber = otpnumber.Text;
                        string Inputs = "UserID=" + userid + "&OTP=" + OTPNumber;
                        string resp = await RestService.RegistrationOTP(Inputs);
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);


                        if (result == "1")
                        {
                            await Navigation.PopPopupAsync();
                            await Navigation.PushAsync(new SetPassword(userid));
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else if (result == "0")
                        {
                            await DisplayAlert("Alert", "OTP Expired Please Resend", "Ok");
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else if (result == "-1")
                        {
                            await DisplayAlert("Alert", "Wrong OTP", "Ok");
                            // await Navigation.PopPopupAsync();
                            //  await Navigation.PushAsync(new SetPassword(userid));
                            Alogin.IsRunning = false;
                            Alogin.IsVisible = false;
                        }
                        else
                        {
                            await DisplayAlert("Alert", "Network Issue..!", "Ok");
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
            }

        }
        public async Task<Boolean> IsValid()
        {
            if (otpnumber.Text == null || otpnumber.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter OTP Number", "Ok");
                otpnumber.Focus();
                return false;
            }
           

            return true;
        }

        private void otpnumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            int _limit = 4;
            string _text = otpnumber.Text;
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                otpnumber.Text = _text;        //Set the Old value
            }
        }
        private async void reSendOtp(object sender, EventArgs e)
        {
            Alogin.IsRunning = true;
            Alogin.IsVisible = true;
            try
            {
                string Inputs = "UserID=" + userid;
                string resp = await RestService.ResendOTP(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                otpmessage.Text = "OTP sent successfully";
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }
            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }
    }
}