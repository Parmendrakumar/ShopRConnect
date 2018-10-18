using RetailerApp.Data;
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
    public partial class PublishUnpublish_Offer : ContentPage
    {
        string StoreUID = "";
        string OfferID = "";
        string Validfrom = "";
        string Validto = "";
        string imagepath2 = "";
        public PublishUnpublish_Offer(string img, string offer, string Id, string validfrom, string validto, string tc, string status, string storeUid)
        {
            InitializeComponent();

            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            imagepath2 = img;
            string Offer = offer;
            OfferID = Id;
            Validfrom = validfrom;
            Validto = validto;
            string Tc = tc;
            string Status = status;
            StoreUID = storeUid;

            if (imagepath2.StartsWith("File"))
            {
                int index = imagepath2.IndexOf("File");
                var img1 = imagepath2.Substring(index + 6);
                offerimage.Source = img1;
            }
            else if(imagepath2.StartsWith("Uri"))
            {
                int index = imagepath2.IndexOf("Uri");
                var img1 = imagepath2.Substring(index + 5);
                offerimage.Source = ImageSource.FromUri(new System.Uri(img1));
            }

            offer1.Text = Offer;
            tc1.Text = Tc;
            validfrom1.Text = Validfrom;
            validto1.Text = Validto;

            entryoffer1.Text = Offer;
            entrytc1.Text = Tc;
            entryvalidfrom1.Date = Convert.ToDateTime(Validfrom);
            entryvalidto1.Date =Convert.ToDateTime(Validto);

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override void OnDisappearing()
        {
            offer1.Text = string.Empty; ;
            tc1.Text = string.Empty; ;
            validfrom1.Text = string.Empty; ;
            validto1.Text = string.Empty; ;
            imagepath2 = string.Empty;

            entryoffer1.Text = string.Empty; ;
            entrytc1.Text = string.Empty; ;
            entryvalidfrom1.Date = System.DateTime.Now;
            entryvalidto1.Date = System.DateTime.Now;
        }

        public void Update(object sender, EventArgs e)
        {
            entryoffer1.IsVisible = true;
            entrytc1.IsVisible = true;
            entryvalidfrom1.IsVisible = true;
            entryvalidto1.IsVisible = true;
            btnupdate.IsVisible = true;

            offer1.IsVisible = false;
            tc1.IsVisible = false;
            validfrom1.IsVisible = false;
            validto1.IsVisible = false;


        }

        public async void Unpublish(object sender, EventArgs e)
        {
            try
            {
                string Inputs = "StoreUId=" + StoreUID + "&Img=" + "" + "&Description=" + "" + "&ValidTo=" + Validfrom + "&ValidFrom=" + Validto + "&OfferTnC=" + "" +
                                "&ScreenName=" + "Unpublish" + "&OfferStatus=" + "Unpublish" + "&OfferId=" + OfferID;
                string resp = await RestService.UploadOffer(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                if (result == "1")
                {
                    await Navigation.PopAsync();
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void btnupdate_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Inputs = "StoreUId=" + StoreUID + "&Img=" + "" + "&Description=" + entryoffer1.Text + "&ValidTo=" + Validfrom + "&ValidFrom=" + Validto + "&OfferTnC=" + entrytc1.Text +
                                "&ScreenName=" + "Edit" + "&OfferStatus=" + "Update" + "&OfferId=" + OfferID;
                string resp = await RestService.UploadOffer(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                if (result == "1")
                {
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}