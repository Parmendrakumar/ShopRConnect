using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_OfferImageDialog : PopupPage
    {
        public string FileName = "";
        public string p = "";
        //   string uploadedFilename="";
        byte[] byteData;
        public MediaFile file;
        public ImageSource imlocal = "";
        public string imagename = "";
        string ImagePath = "";
        public Popup_OfferImageDialog()
        {
            InitializeComponent();
        }

        private async void GalleryOption(object sender, EventArgs e)
        {

            try
            {
                Actimageupload.IsRunning = true;

                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {

                    CompressionQuality = 70,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                ImagePath = file.Path;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();

                //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(FileName);
                //var filee = await rootFolder.GetFileAsync(filename2);

                //Stream stream = await filee.OpenAsync(FileAccess.Read);



                //content.Add(new StreamContent(file.GetStream()),
                //      "\"file\"",
                //      $"\"{file.Path}\"");

                //byteData = Model.Convert.ToByteArray(FileName);

                //imlocal = ImageSource.FromStream(this.file.GetStream);

                //var stream = file.GetStream();
                //file.Dispose();
                //var bytes = new byte[stream.Length];
                //await stream.ReadAsync(bytes, 0, (int)stream.Length);
                //string base64 = System.Convert.ToBase64String(bytes);
               // string base65 = "blobimage" + base64;
                MessagingCenter.Send<View.Popup_OfferImageDialog, string>(this, "blobimagename", ImagePath);
                await Navigation.PopPopupAsync();
                Actimageupload.IsRunning = false;


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                //  await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                await DisplayAlert("Alert", "Unable to get picture from gallery", "Ok");
                Actimageupload.IsRunning = false;
            }
        }
        private async void CameraOption(object sender, EventArgs e)
        {
            try
            {
                Actimageupload.IsRunning = true;
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "ShopRConnect",
                    Name = $"{DateTime.UtcNow}.jpg"
                };

                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    CompressionQuality = 40,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                ImagePath = file.Path;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();

                //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(FileName);
                //var filee = await rootFolder.GetFileAsync(filename2);

                //Stream stream = await filee.OpenAsync(FileAccess.Read);

                //content.Add(new StreamContent(file.GetStream()),
                //    "\"file\"",
                //    $"\"{file.Path}\"");

                //byteData = Model.Convert.ToByteArray(FileName);

                //imlocal = ImageSource.FromStream(this.file.GetStream);

                //var stream = file.GetStream();
                //file.Dispose();
                //var bytes = new byte[stream.Length];
                //await stream.ReadAsync(bytes, 0, (int)stream.Length);
                //string base64 = System.Convert.ToBase64String(bytes);
             //   string base65 = "blobimage" + base64;
                MessagingCenter.Send<View.Popup_OfferImageDialog, string>(this, "blobimagename", ImagePath);
                await Navigation.PopPopupAsync();
                Actimageupload.IsRunning = false;



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
             //   await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                await DisplayAlert("Alert", "Unable to get picture from camera", "Ok");
                Actimageupload.IsRunning = false;
                //   await Navigation.PopPopupAsync();
            }
        }
    }
}