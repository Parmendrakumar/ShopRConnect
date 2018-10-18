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
    public partial class PublishUnpublish_OfferSave : ContentPage
    {       
            string StoreUID = "";
            string OfferID = "";
            string Validfrom = "";
            string Validto = "";
            public PublishUnpublish_OfferSave(string img, string offer, string Id, string validfrom, string validto, string tc, string status, string storeUid)
            {
                InitializeComponent();

                CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
                CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
                CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

                string imagepath2 = img;
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
                else if (imagepath2.StartsWith("Uri"))
                {
                    int index = imagepath2.IndexOf("Uri");
                    var img1 = imagepath2.Substring(index + 5);
                    offerimage.Source = ImageSource.FromUri(new System.Uri(img1));
                }

                offer1.Text = Offer;
                tc1.Text = Tc;
                validfrom1.Text = Validfrom;
                validto1.Text = Validto;

            }

        public async void Delete(object sender, EventArgs e)
        {
            try
            {
                string Inputs = "StoreUId=" + StoreUID + "&Img=" + "" + "&Description=" + "" + "&ValidTo=" + Validfrom + "&ValidFrom=" + Validto + "&OfferTnC=" + "" +
                                "&ScreenName=" + "Delete" + "&OfferStatus=" + "Deleted" + "&OfferId=" + OfferID;
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

        public async void Publish(object sender, EventArgs e)
            {
                try
                {
                    string Inputs = "StoreUId=" + StoreUID + "&Img=" + "" + "&Description=" + "" + "&ValidTo=" + Validfrom + "&ValidFrom=" + Validto + "&OfferTnC=" + "" +
                                    "&ScreenName=" + "Publish" + "&OfferStatus=" + "Publish" + "&OfferId=" + OfferID;
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