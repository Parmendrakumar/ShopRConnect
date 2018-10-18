
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RetailerApp
{
    public partial class App : Application
    {


        public App()
        {
            Current.Resources = new ResourceDictionary();
            Current.Resources.Add("UlycesColor", Color.FromRgb(121, 248, 81));
            var navigationStyle = new Style(typeof(NavigationPage));
            var barTextColorSetter = new Setter { Property = NavigationPage.BarTextColorProperty, Value = Color.White };
            var barBackgroundColorSetter = new Setter { Property = NavigationPage.BarBackgroundColorProperty, Value = Color.Black };

            navigationStyle.Setters.Add(barTextColorSetter);
            navigationStyle.Setters.Add(barBackgroundColorSetter);

            Current.Resources.Add(navigationStyle);
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjAyMTVAMzEzNjJlMzIyZTMwWWJzOHJoWmt4KzhwUHpiV21IL0hkN3Q1UkJ6bWVxV1pqR3pnN2dSRDhMdz0=");
            InitializeComponent();
            Current = this;
    
            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;
            // MainPage = new NavigationPage(new View.LoginSignup());
            //  MainPage = new CustomNavigationPage(new View.LoginSignup());
            try
            {
                if (isLoggedIn)
                    MainPage = new CustomNavigationPage(new MainPage());
                else
                    MainPage = new CustomNavigationPage(new View.LoginSignup());

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                MainPage = new CustomNavigationPage(new View.LoginSignup());
            }

               OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b").HandleNotificationOpened(HandleNotificationOpened).InFocusDisplaying(OSInFocusDisplayOption.None).EndInit();

            //OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b")
            //            .HandleNotificationReceived(HandleNotificationReceived)
            //            .HandleNotificationOpened(HandleNotificationOpened)
            //            .EndInit();

        }

        //private static void HandleNotificationReceived(OSNotification notification)
        //{
        //    OSNotificationPayload payload = notification.payload;
        //    string message = payload.body;
        //    Dictionary<string, object> additionalData = payload.additionalData;

        //    if (additionalData != null)
        //    {
        //        if (additionalData.ContainsKey("screenName"))
        //        {
        //           var pushScreenName = Convert.ToString(additionalData["screenName"]);
        //        }
        //        else
        //        {
        //           var pushScreenName = "";
        //        }
        //    }
        //}
        //private static void HandleNotificationOpened(OSNotificationOpenedResult result)
        //{
        //    OSNotificationPayload payload = result.notification.payload;
        //    Dictionary<string, object> additionalData = payload.additionalData;
        //    string message = payload.body;
        //    string actionID = result.action.actionID;

        //    if (additionalData != null)
        //    {
        //        if (additionalData.ContainsKey("screenName"))
        //        {
        //           var pushScreenName = Convert.ToString(additionalData["screenName"]);
        //        }
        //        else
        //        {
        //           var pushScreenName = "";
        //        }
        //    }
        //}

        public async void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            await Task.Delay(1000);
            //await MainPage.Navigation.PushAsync(new MainPage());
            //  MainPage = new CustomNavigationPage(new MainPage());

            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;

            try
            {
                if (isLoggedIn)
                    MainPage = new CustomNavigationPage(new MainPage());
                else
                    MainPage = new CustomNavigationPage(new View.LoginSignup());
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }


        protected override  void OnStart()
        {
            //   OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b").HandleNotificationOpened(HandleNotificationOpened).InFocusDisplaying(OSInFocusDisplayOption.None).EndInit();
           // string userid = Application.Current.Properties["UserName"].ToString();
           // txtPassword.Text = Application.Current.Properties["Password"].ToString();
        }

        protected override void OnSleep()
        {
        //    OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b").HandleNotificationOpened(HandleNotificationOpened).InFocusDisplaying(OSInFocusDisplayOption.None).EndInit();

        }

        protected override void OnResume()
        {
         //   OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b").HandleNotificationOpened(HandleNotificationOpened).InFocusDisplaying(OSInFocusDisplayOption.None).EndInit();

        }
    }
}
