using Plugin.Media;
using Plugin.Media.Abstractions;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfImageEditor.XForms;
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
    public partial class AddOffer : ContentPage
    {
        public string FileName = "";
        public string p = "";
        //   string uploadedFilename="";
        byte[] byteData;
        public MediaFile file;
        public ImageSource imlocal = "";
        public string imagename = "";
        string ImageString = "";
        string TNC = "";
        string Offer = "";
        string ValidTo = "";
        string ValidFrom = "";

        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        string UserID = "";
        string ImageName = "";
        string ImageName2 = "";
        string userId = "";
        string StoreID = "";
        string FilePath = "";

       // SfImageEditor editor = new SfImageEditor();
        public AddOffer()
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();

            UserID = userin.Userid;
            StoreID = userin.StoreId;

           

        }

      
        protected override void OnAppearing()
        {

            base.OnAppearing();
           
            MessagingCenter.Unsubscribe<EditImage, string>(this, "blobimagename");
            MessagingCenter.Unsubscribe<Popup_OfferImageDialog, string>(this, "editimagename");
            MessagingCenter.Subscribe<Popup_OfferImageDialog, string>(this, "blobimagename", (sender, value) =>
            {
                bool ab = true;


                if (ab == true && value != null)
                {
                    FilePath = value;
                 //   int index = ImageName.IndexOf("blobimage");

                 //   ImageName = ImageName.Substring(index + 9);

                   // var byteArray = System.Convert.FromBase64String(ImageName);
                  //  Stream stream3 = new MemoryStream(byteArray);
                  //  var imageSource = ImageSource.FromStream(() => stream3);
                  //  profileimage.Source = imageSource;
                 //   ImageName2 = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteArray.ToArray()));

                    if(FilePath!= null || FilePath!="")
                    {
                        Navigation.PushAsync(new EditImage(FilePath));
                    }

                }

                ab = false;
                value = null;
            });
            MessagingCenter.Subscribe<EditImage, string>(this, "editimagename", (sender, value) =>
            {
                bool ab = true;


                if (ab == true && value != null)
                {
                    FilePath = value;
                    //   int index = ImageName.IndexOf("blobimage");

                    //   ImageName = ImageName.Substring(index + 9);

                    // var byteArray = System.Convert.FromBase64String(ImageName);
                    //  Stream stream3 = new MemoryStream(byteArray);
                    //  var imageSource = ImageSource.FromStream(() => stream3);
                    //  profileimage.Source = imageSource;
                    //   ImageName2 = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteArray.ToArray()));

                    //if (FilePath != null || FilePath != "")
                    //{
                    //    Navigation.PushAsync(new EditImage(FilePath));
                    //}

                }

                ab = false;
                value = null;
            });
        }

        protected override void OnDisappearing()
        {
            //   TextContainer.Clear();
            MessagingCenter.Unsubscribe<Popup_OfferImageDialog, string>(this, "blobimagename");
            MessagingCenter.Unsubscribe<EditImage, string>(this, "editimagename");

        }
        private async void PreviewOfferpopup(object sender, EventArgs e)
        {
            Boolean boolisValid = await IsValid();
            if (boolisValid)
            {
                Offer = offer.Text;
                TNC = tnc.Text;
                // ValidFrom = "24/08/2018";
                // ValidTo = "30/08/2018";
                ValidFrom = fromoffer.Date.ToString("dd/MM/yyyy").Trim();
                ValidTo = tooffer.Date.ToString("dd/MM/yyyy").Trim();

                try
                {
                    var _Popupspec = new Popup_OfferPreview(FilePath, TNC, Offer, ValidFrom, ValidTo, UserID, StoreID);
                    await Navigation.PushPopupAsync(_Popupspec);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        private async void GetImage_Tapped1(object sender, EventArgs e)
        {
            try
            {
                var _Popupspec = new Popup_OfferImageDialog();
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void GetImage_Tapped(object sender, EventArgs e)
        {
            try
            {

                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {

                    CompressionQuality = 50,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
                });
                FilePath = file.Path;
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();
                
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
                //ImageString = base64;
          


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
               // Actimageupload.IsRunning = false;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditImage(FilePath));
        }

        public async Task<Boolean> IsValid()
        {
            if (offer.Text == null || offer.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Offer", "Ok");
                offer.Focus();
                return false;
            }
            if (tnc.Text == null || tnc.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Offer T&C", "Ok");
                tnc.Focus();
                return false;
            }
            return true;
        }
    }
}