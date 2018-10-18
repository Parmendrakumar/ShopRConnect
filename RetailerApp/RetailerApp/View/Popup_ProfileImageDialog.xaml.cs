using Plugin.Media;
using Plugin.Media.Abstractions;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_ProfileImageDialog : PopupPage
    {
        public string FileName = "";
        public string FilePath = "";
        public string p = "";
        //   string uploadedFilename="";
        byte[] byteData;
        public MediaFile file;
        public ImageSource imlocal = "";
        public string imagename = "";
        public MemberDatabase memberDatabase;
        public registrationImages regimages;
        string Imagetype = "";
        string ImagePath = "";
        public Popup_ProfileImageDialog(string imgType)
        {
            InitializeComponent();
            CloseWhenBackgroundIsClicked = true;
            memberDatabase = new MemberDatabase();
            Imagetype = imgType;
        }

        private void PopupPage_BackgroundClicked(object sender, EventArgs e)
        {
           
            Navigation.PopPopupAsync();
        }

        private async void GalleryOption(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();

                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    CompressionQuality = 50,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });

                ImagePath = file.Path;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();
                string Imagenametype = Imagetype + ImagePath;
                MessagingCenter.Send<View.Popup_ProfileImageDialog, string>(this, "blobimagename", Imagenametype);
                await Navigation.PopPopupAsync();
                Actimageupload.IsRunning = false;


            }
          
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Unable to get picture from gallery", "Ok");
                Actimageupload.IsRunning = false;
            }
        }

        private async void CameraOption(object sender, EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                    return;
                }
               
                file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    CompressionQuality = 50,
                    PhotoSize = PhotoSize.Small
                });
                ImagePath = file.Path;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();

                //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(FileName);
                //var filee = await rootFolder.GetFileAsync(filename2);

                //Stream stream = await filee.OpenAsync(FileAccess.Read);

              
                string Imagenametype = Imagetype + ImagePath;
                MessagingCenter.Send<View.Popup_ProfileImageDialog, string>(this, "blobimagename", Imagenametype);
                await Navigation.PopPopupAsync();
                Actimageupload.IsRunning = false;



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Unable to get picture from camera", "Ok");
                Actimageupload.IsRunning = false;
                //   await Navigation.PopPopupAsync();
            }
        }

        private async void GalleryOption2(object sender, EventArgs e)
        {
          //  string imageType = "";

            try
            {

                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {

                    CompressionQuality = 10,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });
               

                p = file.Path;
                FilePath = file.Path;
                FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1);

                regimages = new registrationImages
                {
                    Imagename = FileName,
                    ImagePath = FilePath,
                    UploadingDone = false,

                };

                var query = memberDatabase.GetRegImage(Imagetype);

                if (query.Count() > 0)
                    memberDatabase.UpdateRegImage(regimages);
                else
                    memberDatabase.AddRegImage(regimages);


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                Actimageupload.IsRunning = false;
            }
            await Navigation.PopPopupAsync();
        }

        private async void CameraOption2(object sender, EventArgs e)
        {
            try
            {
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
                    CompressionQuality = 10,
                    PhotoSize = PhotoSize.Small
                });
                FilePath = file.Path;
                FileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();




            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                Actimageupload.IsRunning = false;
                //   await Navigation.PopPopupAsync();
            }
        }
    }
}