using PCLStorage;
using RetailerApp.Data;
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
    public partial class Popup_OfferPreview : PopupPage
    {
        string ImageString = "";
        string TnC = "";
        string Offer1 = "";
        string ValidTo = "";
        string ValidFrom = "";
        string ImageName2 = "";
        string UserId = "";
        string StoreId = "";
        string FilePath = "";
        string storeUId = "";
        public MemberDatabase memberDatabase;
        public OfferDetail offerdetails;
        public Popup_OfferPreview(string filepath,string tnc, string offer, string validto, string validfrom, string userid, string storeid)
        {
            FilePath = filepath;
            TnC = tnc;
            Offer1 = offer;
            ValidTo = validto;
            ValidFrom = validfrom;
            UserId = userid;
            StoreId = storeid;

            InitializeComponent();

           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
                       
            Offerpreview2.Source = FilePath;
            txtoffer.Text = Offer1;


        }
        private void btnno_Clicked(object sender, EventArgs e)
        {
            btnno.BackgroundColor = Color.FromHex("#f24245");
           // btnno.TextColor = Color.White;
            Navigation.PopPopupAsync();
        }

        private async void btnyes_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var text = btn.Text;
            try
            {               
                Alogin.IsRunning = true;
                Alogin.IsVisible = true;
               

                if(text == "Publish")
                {
                    btnyes.BackgroundColor = Color.FromHex("#f24245");
                }
                else if(text == "Save")
                {
                    btnyes1.BackgroundColor = Color.FromHex("#f24245");
                }

                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetUserDetail();

                UserDetail userin = (from blog in members
                                     select blog).FirstOrDefault();
               
                storeUId = userin.StoreUId;

                //var byteArray = System.Convert.FromBase64String(ImageString);
                //ImageName2 = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteArray.ToArray()));
                var Filepath2 = System.IO.Path.GetDirectoryName(FilePath);
                var content = new MultipartFormDataContent();
                await Task.Delay(TimeSpan.FromSeconds(0.01));
                var fileName = FilePath.Substring(FilePath.LastIndexOf("/") + 1);

                IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(Filepath2);
                var filee = await rootFolder.GetFileAsync(fileName);

                Stream stream = await filee.OpenAsync(FileAccess.Read);
                // image.Source = ImageSource.FromStream(() => stream);

                content.Add(new StreamContent(stream),
                        "\"filee\"",
                        $"\"{filee.Path}\"");

                var httpClient = new System.Net.Http.HttpClient();
                var uploadServiceBaseAddress = "http://elixirct.in/ShopRConservicePublish/api/Files/Upload";
                var httpResponseMessage = await httpClient.PostAsync(uploadServiceBaseAddress, content);

                if (httpResponseMessage.ReasonPhrase.Equals("OK"))
                {
                    if (text == "Publish")
                    {
                        string Inputs = "StoreUId=" + storeUId + "&Img=" + fileName + "&Description=" + Offer1 + "&ValidTo=" + ValidTo + "&ValidFrom=" + ValidFrom + "&OfferTnC=" + TnC + 
                            "&ScreenName=" + "Insertoffer" + "&OfferStatus=" + "Publish" + "&OfferId=" + "0";

                        string resp = await RestService.UploadOffer(Inputs);
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                        if (result != "0")
                        {
                            string imagename = fileName;
                            string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + imagename;
                            DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
                            //  DependencyService.Get<IFileService>().SavePicture(fileName, stream, getGalleryPath());
                            // var images = DependencyService.Get<IFileService>().GetPictureFromDisk(fileName, getGalleryPath());
                            //offerdetails = new OfferDetail();
                            //memberDatabase = new MemberDatabase();
                            //offerdetails.Imagename = imagestore2;
                            //offerdetails.ofer = Offer1;
                            //memberDatabase.AddOfferDetail(offerdetails);
                            await DisplayAlert("Alert", "Offer Upload Successfully..!", "Ok");
                            await Navigation.PopAllPopupAsync();
                            //await Navigation.PushAsync(new Offer());
                            await Navigation.PopAsync();
                        }
                    }
                    else if(text == "Save")
                    {
                        string Inputs = "StoreUId=" + storeUId + "&Img=" + fileName + "&Description=" + Offer1 + "&ValidTo=" + ValidTo + "&ValidFrom=" + ValidFrom + "&OfferTnC=" + TnC +
                           "&ScreenName=" + "Insertoffer" + "&OfferStatus=" + "Save" + "&OfferId=" + "0";
                        string resp = await RestService.UploadOffer(Inputs);
                        var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                        if (result != "0")
                        {
                            string imagename = fileName;
                            string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + imagename;
                            DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
                            // DependencyService.Get<IFileService>().SavePicture(fileName, stream, getGalleryPath());
                            //var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());

                            //offerdetails = new OfferDetail();
                            //memberDatabase = new MemberDatabase();
                            //offerdetails.Imagename = imagestore2;
                            //offerdetails.ofer = Offer1;
                            //memberDatabase.AddOfferDetail(offerdetails);
                            await DisplayAlert("Alert", "Offer Upload Successfully..!", "Ok");
                            await Navigation.PopAllPopupAsync();
                            //await Navigation.PushAsync(new Offer());
                            await Navigation.PopAsync();
                        }
                    }
                }
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;              
            }
            catch(Exception ex)
            {
               // await DisplayAlert("Alert", "Offer is not ", "OK");
                System.Diagnostics.Debug.WriteLine(ex);
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

    }
}