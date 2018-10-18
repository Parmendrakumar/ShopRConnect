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
    public partial class UpdateUserProfile : ContentPage
    {
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        string UserID = "";
        public UpdateUserProfile()
        {
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

        }

        protected override void OnAppearing()
        {

            base.OnAppearing();

           
            memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();

            UserID = userin.Userid;
            var imlocal = userin.Imagename;
            imlocal = imlocal + ".jpg";
            var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imlocal, getGalleryPath());
            //var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, userin.Imagename);
            //var imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));

            userProfile.Source = images;
            Title = userin.Name;
            name.Text = userin.Name;
            email.Text = userin.Email;

          


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

        private async void AddPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "ProfilePhoto";

                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navigation.InsertPageBefore(new MainPage(), this);
            //  await Navigation.PopAsync().ConfigureAwait(false);
            //  Application.Current.MainPage = new MainPage();
            await Navigation.PopAsync();

        }
    }
}