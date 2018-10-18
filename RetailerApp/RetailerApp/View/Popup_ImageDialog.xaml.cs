using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Net.Http;
using System.IO;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Plugin.FilePicker;
using System.Net;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Popup_ImageDialog : PopupPage
    {
        public string FileName = "";
        public string p = "";
        public string FileNamePre = "";
        //   string uploadedFilename="";
        string FilePathPre = "";
        byte[] byteData;
        public MediaFile file;
        public ImageSource imlocal = "";
        public string imagename = "";

      //  IDownloader download = DependencyService.Get<IDownloader>();
        public Popup_ImageDialog()
        {
            InitializeComponent();
            
        }
     
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {        
            try
            {  
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    CompressionQuality = 20,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small,
                
                });
                p = file.Path;
                FilePathPre = "awsimage" + p;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);
                FileNamePre = "awsimage" + FileName;
                var content = new MultipartFormDataContent();

                Actimageupload.IsRunning = true;
                await Task.Delay(TimeSpan.FromSeconds(0.01));

                content.Add(new StreamContent(file.GetStream()),
                    "\"file\"",
                    $"\"{file.Path}\"");

                var httpClient = new System.Net.Http.HttpClient();
                var uploadServiceBaseAddress = "http://elixirct.in/ShopRConservicePublish/api/Files/Upload";
                var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

                if (httpResponseMessage.ReasonPhrase.Equals("OK"))
                {
                    MessagingCenter.Send<View.Popup_ImageDialog, string>(this, "awsimagename", FilePathPre);

                    string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + FileName;
                    await Task.Delay(TimeSpan.FromSeconds(0.01));
                    DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
              
                   
                    await Navigation.PopPopupAsync();
                    Actimageupload.IsRunning = false;
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                //await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                Actimageupload.IsRunning = false;
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
        private async void CameraOption(object sender, EventArgs e)
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
                 
                    CompressionQuality = 20,
                    PhotoSize = PhotoSize.Small
                });
                p = file.Path;
                FilePathPre = "awsimage" + p;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);
                FileNamePre = "awsimage" + FileName;
                var content = new MultipartFormDataContent();

                Actimageupload.IsRunning = true;
                await Task.Delay(TimeSpan.FromSeconds(0.01));

                content.Add(new StreamContent(file.GetStream()),
                    "\"file\"",
                    $"\"{file.Path}\"");

                var httpClient = new System.Net.Http.HttpClient();

                // var uploadServiceBaseAddress = "http://maudit.elixirct.net/Uploadfiletoserver/api/Files/Upload";
                var uploadServiceBaseAddress = "http://elixirct.in/ShopRConservicePublish/api/Files/Upload";
                var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

                if (httpResponseMessage.ReasonPhrase.Equals("OK"))
                {
                    MessagingCenter.Send<View.Popup_ImageDialog, string>(this, "awsimagename", FilePathPre);
                   
                    string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + FileName;
                    await Task.Delay(TimeSpan.FromSeconds(0.01));                    
                    DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());

                    // download.DownloadFile(url, "ShopRConnect");
                    // MessagingCenter.Send<View.Popup_ImageDialog, string>(this, "awsimagename", FileNamePre);

                    await Navigation.PopPopupAsync();
                    Actimageupload.IsRunning = false;

                }
                else
                {
                   await DisplayAlert("Alert", "Network Issue", "OK");
                    Actimageupload.IsRunning = false;
                }





            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            //    await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
                Actimageupload.IsRunning = false;
                //   await Navigation.PopPopupAsync();
            }
        }
        private async void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

        }
        private async void Document(object sender, EventArgs e)
        {
            try
            {
                var file = await CrossFilePicker.Current.PickFile();

                if (file != null)
                {
                    Actimageupload.IsRunning = true;
                    await Task.Delay(TimeSpan.FromSeconds(0.01));
                    var filename = file.FileName;
                    var byteData = file.DataArray;
                    var imagename =  await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteData.ToArray()));
                   // var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, imagename);
                    string ImageName = "pdf" + imagename;
                    //string ImageName = "blobimage" + p;
                    MessagingCenter.Send<View.Popup_ImageDialog, string>(this, "blobimagename", ImageName);
                    await Navigation.PopPopupAsync();
                    Actimageupload.IsRunning = false;
                }
           
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}