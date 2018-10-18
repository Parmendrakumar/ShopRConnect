using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Plugin.Connectivity;
using RetailerApp.Data;
using RetailerApp.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public MemberDatabase memberDatabase;
        public UserDetail userdetail;
        public static App Current;
        public Login()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            // App.Current.Properties["IsPasswordValid"] = false;
            //  App.Current.Properties["IsMobileValid"] = false;

            mobile.Completed += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        LoginMethod();
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                    }
                }
                else
                {
                    DisplayAlert("Alert", "Please check your internet connection", "OK");
                }

            };

            password.Completed += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        LoginMethod();
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }

        public async void LoginMethod()
        {
            try
            {
                Boolean boolisValid = await IsValid();

                //  var isMobileValid = App.Current.Properties.ContainsKey("IsMobileValid") ? (bool)App.Current.Properties["IsMobileValid"] : false;
                //  var isPasswordValid = App.Current.Properties.ContainsKey("IsPasswordValid") ? (bool)App.Current.Properties["IsPasswordValid"] : false;

                if (boolisValid)
                {
                    Alogin.IsRunning = true;
                    Alogin.IsVisible = true;

                    memberDatabase = new MemberDatabase();
                    memberDatabase.DeleteUserDetail();

                    string password1 = password.Text;
                    string mobilenumber = mobile.Text;
                    string Inputs = "Mobile=" + mobilenumber + "&Password=" + password1;
                    string resp = await RestService.Login(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                    if (result == "Invalid Username/Password")
                    {
                        await DisplayAlert("Alert", "Invalid Username Password", "Ok");
                    }

                    else if (result != "Invalid Username/Password" && result != "" && result != null)
                    {
                        string[] arrSepr1 = resp.Split('^');
                        Application.Current.Properties["UserName"] = arrSepr1[1];
                        Application.Current.Properties["Password"] = arrSepr1[2];

                        try
                        {
                            string outList = arrSepr1[3];
                            string storeList = outList.Split('$')[0];
                            string[] arrSepr2 = storeList.Split('`');

                            if (arrSepr2.Length > 1)
                            {
                                userdetail = new UserDetail
                                {
                                    Userid = arrSepr2[0],
                                    Name = arrSepr2[1],
                                    StoreId = arrSepr2[2],
                                    // Email = arrSepr2[3],
                                    Imagename = arrSepr2[3],
                                    StoreUId = arrSepr2[4],
                                    StoreImage1 = arrSepr2[5],
                                    StoreImage2 = arrSepr2[6],
                                    StoreImage3 = arrSepr2[7],
                                };

                                memberDatabase = new MemberDatabase();

                                if (userdetail != null)
                                {
                                    memberDatabase.AddUserDetail(userdetail);

                                    App.Current.Properties["IsLoggedIn"] = true;
                                    //  var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : true;
                                    //var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, userdetail.Imagename);
                                    //var imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));

                                    //var imagestore2 = userdetail.Imagename + ".jpg";
                                    //Stream stream = new MemoryStream(imageData);
                                    //DependencyService.Get<IFileService>().SavePicture(imagestore2, stream, getGalleryPath());
                                    //var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());

                                      await Navigation.PushAsync(new MainPage(), false);
                                   //    Navigation.InsertPageBefore(new MainPage(), new LoginSignup());
                                    //  await Navigation.PopAsync().ConfigureAwait(true);

                                   
                                }
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
                        await DisplayAlert("Alert", "Something Wrong..! Please contect to Admin", "Ok");
                        //  await DisplayAlert("Alert", "Network Issue", "Ok");
                        //  await DisplayAlert("Alert", "Please complete registration process...!", "Ok");
                    }
                }


            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "Network Issue", "Ok");
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
            }

            Alogin.IsRunning = false;
            Alogin.IsVisible = false;
        }

        private  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    LoginMethod();
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

        }
        String getGalleryPath()
        {
            Java.IO.File Connect = Android.OS.Environment.GetExternalStoragePublicDirectory("ShopRConnect");

            if (!Connect.Exists())
            {

                Connect.Mkdir();
            }

            //  return Connect.getAbsolutePath();
            return Connect.AbsolutePath;

        }
        public async Task<Boolean> IsValid()
        {
            if (mobile.Text == null || mobile.Text == "")
            {
                await DisplayAlert("Alert", "Please Enter Mobile Number", "Ok");
                mobile.Focus();
                return false;
            }
            else if (!Regex.Match(mobile.Text, @"^[6-9][0-9]{9}$").Success)
            {
                await DisplayAlert("", "Please enter valid Mobile number", null, "Ok");
                mobile.Focus();
                return false;
            }
           else if (password.Text == null || password.Text == "")
            {
                await DisplayAlert("Alert", "Please Enter Password", "Ok");
                password.Focus();
                return false;               
            }

            return true;
        }
        private void BackButton(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void ForgotPassword(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }

        private async void ToolSignup_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new Signup1());
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
                    await Navigation.PushAsync(new Signup("Yes","Yes"), false);
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
    }
}