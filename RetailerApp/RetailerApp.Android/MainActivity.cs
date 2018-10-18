using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using ImageCircle.Forms.Plugin.Droid;
using CarouselView.FormsPlugin.Android;
using FFImageLoading.Forms.Droid;
using Firebase.Analytics;
using System.Threading.Tasks;
using Firebase.Iid;
using Firebase.Messaging;


namespace RetailerApp.Droid
{
    [Activity(Label = "ShopRConnect", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //    private FirebaseAnalytics firebaseAnalytics;
       
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, bundle);
            // firebaseAnalytics = FirebaseAnalytics.GetInstance(this);
            OneSignal.Current.StartInit("dfafb3a8-6d8f-46b5-99ae-d732c049e58b").EndInit();
            OneSignal.Current.SendTag("Retailer", "Noida");

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            ImageCircleRenderer.Init();

            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            CarouselViewRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
         
            

            //#if DEBUG
            //            Task.Run(() =>
            //            {
            //                var instanceID = FirebaseInstanceId.Instance;
            //                instanceID.DeleteInstanceId();
            //                var iid1 = instanceID.Token;
            //                var iid2 = instanceID.GetToken(GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope);
            //                FirebaseMessaging.Instance.SubscribeToTopic("all");
            //            });
            //#else
            //			FirebaseMessaging.Instance.SubscribeToTopic("all");
            //#endif	

            LoadApplication(new App());
         
        }
      
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

     

    }


}

