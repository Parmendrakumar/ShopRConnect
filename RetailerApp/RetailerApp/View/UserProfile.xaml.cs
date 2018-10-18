using RetailerApp.Data;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserProfile : ContentPage
    {
        string ImageName = "";
        string ImageName2 = "";
        string userId = "";
        public UserProfile(string userid)
        {
            userId = userid;
            InitializeComponent();
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
          
        }

        protected override void OnAppearing()
        {
           
            base.OnAppearing();
            MessagingCenter.Unsubscribe<Popup_ProfileImageDialog, string>(this, "blobimagename");
            MessagingCenter.Subscribe<Popup_ProfileImageDialog, string>(this, "blobimagename", async (sender, value) =>
            {
                bool ab = true;


                if (ab == true && value != null)
                {
                    ImageName = value;
                    int index = ImageName.IndexOf("blobimage");

                    ImageName = ImageName.Substring(index + 9);

                    var byteArray = System.Convert.FromBase64String(ImageName);
                    Stream stream3 = new MemoryStream(byteArray);
                    var imageSource = ImageSource.FromStream(() => stream3);
                    profileimage.Source = imageSource;
                    ImageName2 = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteArray.ToArray()));

                }

                ab = false;
                value = null;
            });
        }
        protected override void OnDisappearing()
        {
            //   TextContainer.Clear();
            MessagingCenter.Unsubscribe<Popup_ProfileImageDialog, string>(this, "blobimagename");

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
            try
            {

                string Name = name.Text;
                string Email = email.Text;
               
                string Inputs = "UserID=" + userId + "&Name=" + Name + "&Email=" + Email + "&Img=" + ImageName2;
                string resp = await RestService.RegistrationOtherDetail(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                if (result == "1")
                {
                    //  await Navigation.PushAsync(new MainPage());

                    if(ImageName2!="")
                    {
                        var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, ImageName2);
                        var imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));

                        var imagestore2 = ImageName2 + ".jpg";
                        Stream stream = new MemoryStream(imageData);
                        DependencyService.Get<IFileService>().SavePicture(imagestore2, stream, getGalleryPath());
                        var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());
                        await Navigation.PopToRootAsync();

                    }
                    else
                    {
                        await Navigation.PopToRootAsync();
                    }

                   
                }
                else
                {
                    await DisplayAlert("Alert", "Unable to save data, check Internet connection", "Ok");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }


           
           
        }
    }
}