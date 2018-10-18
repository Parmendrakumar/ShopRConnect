using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        string UserID = "";
        public Profile()
        {
            InitializeComponent();
          
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
            BindingContext = new ProfleBindingClass();
        }

        private async void activelistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dataItem = e.SelectedItem as ProfileOption;
            string name = dataItem.Id;

            if (name == "1")
            {
                //await Navigation.PushAsync(new Preferences());
                await Navigation.PushAsync(new AddUser());
            }
            else if (name == "2")
            {
                await Navigation.PushAsync(new ContactUs());
            }
            else if (name == "3")
            {

            }
            else if (name == "4")
            {

            }
            else if (name == "5")
            {
                await Navigation.PushAsync(new AboutUs());
            }
            else if (name == "6")
            {
                await Navigation.PushAsync(new TermsOfUse());
            }
            else if (name == "7")
            {
                try
                {
                    App.Current.Properties["IsLoggedIn"] = false;
                    var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
                    if (!isLoggedIn)
                    {
                        memberDatabase = new MemberDatabase();
                        memberDatabase.DeleteUserDetail();
                        await Navigation.PushAsync(new LoginSignup());
                       // Application.Current.MainPage = new LoginSignup();
                    }
                    else
                    {
                       
                    }
                       
                }
                catch (Exception ex)
                {
                   // App.Current.Properties["IsLoggedIn"] = false;
                  //  var isLoggedIn = App.Current.Properties.ContainsKey("IsLoggedIn") ? (bool)App.Current.Properties["IsLoggedIn"] : false;
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
          //  Navigation.PushAsync(new UpdateUserProfile());
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                act.IsRunning = true;
                act.IsVisible = true;
                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetUserDetail();

                UserDetail userin = (from blog in members
                                     select blog).FirstOrDefault();

                UserID = userin.Userid;
                var imlocal = userin.Imagename;
                string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + imlocal;
                //  imlocal = imlocal + ".jpg";
                //  var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imlocal, getGalleryPath());
                //var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, userin.Imagename);
                //var imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));

                userProfile.Source = url;
                Title = userin.Name;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                act.IsRunning = false;
                act.IsVisible = false;
            }
            act.IsRunning = false;
            act.IsVisible = false;
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
    }
    class ProfleBindingClass
    {
        public ObservableCollection<ProfileOption> active { get; set; }

        public ProfleBindingClass()
        {
            active = new ObservableCollection<ProfileOption>();

            active.Add(new ProfileOption { Id = "1", img = "settings.png", Name = "Add User" });
            active.Add(new ProfileOption { Id = "2", img = "call.png", Name = "Contact Us" });
            //active.Add(new ProfileOption { Id = "3", img = "rating1.png", Name = "Rate on Play My Store" });
            //active.Add(new ProfileOption { Id = "4", img = "share.png", Name = "Share App with Friends" });
            active.Add(new ProfileOption { Id = "5", img = "about.png", Name = "About Us" });
            active.Add(new ProfileOption { Id = "6", img = "aboutus.png", Name = "Terms of Use" });
            active.Add(new ProfileOption { Id = "7", img = "logout.png", Name = "Logout" });
           
        }
    }
    public class ProfileOption
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ImageSource img { get; set; }
        


    }
}