using PCLStorage;
using Plugin.Connectivity;
using Plugin.Permissions;
using RetailerApp.Data;
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
    public partial class Offer : ContentPage
    {
      
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        string UserID = "";
        string StoreID = "";
        public Offer()
        {
            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

           
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = new OfferAddingBinding();

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Storage);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Storage))
                {
                    await DisplayAlert("Need location", "Gunna need that Storage", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Storage);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Storage))
                    status = results[Plugin.Permissions.Abstractions.Permission.Storage];
            }

        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Alert!", "Do you really want to close the application?", "Yes", "No");
                if (result)
                {
                    if (Device.OS == TargetPlatform.Android)
                    {
                        Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
                    }
                }
            });
            return true;
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
        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            offerlistview.IsVisible = false;
            allofferlistview.IsVisible = true;
            filterimg.IsVisible = false;
            addimg.IsVisible = false;
       
            box2.Color = Color.White;
            box1.Color = Color.FromHex("#011c1a");

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            offerlistview.IsVisible = true;
            allofferlistview.IsVisible = false;
            filterimg.IsVisible = false;
            addimg.IsVisible = true;

            box1.Color = Color.White;
            box2.Color = Color.FromHex("#011c1a");


        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Filter());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOffer());
        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
         => ((ListView)sender).SelectedItem = null;


        private void Delete(object sender, EventArgs e)
        {

        }
        private void Edit(object sender, EventArgs e)
        {

        }

        //public async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    try
        //    {
        //        var dataItem = e.Item as OffersDetails;
        //        var img = Convert.ToString(dataItem.img);
        //        await Navigation.PushAsync(new PublishUnpublish_Offer(img));
        //    }
        //    catch(Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex);
        //    }
        //}
        private async void offerlistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var myItem = e.SelectedItem as OffersDetails;
                //var myListView = (ListView)sender;
                //OffersDetails myItem = (OffersDetails)myListView.SelectedItem;

                var img = Convert.ToString(myItem.img);
                string offer = myItem.Name;
                string Id = myItem.Id;
                string validto = myItem.validto;
                string validfrom = myItem.validfrom;
                string tc = myItem.tc;
                string status = myItem.Status;
                string storeUid = myItem.storeUID;

                if (status == "Publish")
                {
                    await Navigation.PushAsync(new PublishUnpublish_Offer(img, offer, Id,validfrom,validto,tc,status,storeUid));
                }
                else if(status == "Save" || status == "Unpublish")
                {
                    await Navigation.PushAsync(new PublishUnpublish_OfferSave(img, offer, Id, validfrom, validto, tc, status, storeUid));
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
           
        }
    }

    public class OfferAddingBinding
    {
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        public UserDetail userdetail2;
        string UserID = "";
        string StorID = "";
        string storeUId = "";

       public ObservableCollection<OffersDetails> offer1 { get; set; }
       public ObservableCollection<OffersDetails> offer2 { get; set; }
        public OfferAddingBinding()
        {

            offer1 = new ObservableCollection<OffersDetails>();
            offer2 = new ObservableCollection<OffersDetails>();

            if (CrossConnectivity.Current.IsConnected)
            {
                GetOffer();
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
        public async void GetOffer()
        {
            memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();

            UserID = userin.Userid;
            StorID = userin.StoreId;
            storeUId = userin.StoreUId;

            string Inputs = "StoreUId=" + storeUId;
            string resp = await RestService.GetOffers(Inputs);
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

            if (result != "")
            {              

                try
                {
                    string[] arrSepr2 = result.Split('|');

                    if (arrSepr2.Length > 1)
                    {
                        foreach (string word in arrSepr2)
                        {
                            string[] Itemstemp = word.Split('`');

                            var images = DependencyService.Get<IFileService>().GetPictureFromDisk(Itemstemp[2], getGalleryPath());
                            ExistenceCheckResult imagepresent = await FileSystem.Current.LocalStorage.CheckExistsAsync(images);

                            if (imagepresent == ExistenceCheckResult.FileExists)
                            {
                                if (Itemstemp[5] == "Publish")
                                {
                                    offer1.Add(new OffersDetails { img = images, Name = Itemstemp[0], Id = Itemstemp[6], publishIcon = "Publishicon.png", tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                    offer2.Add(new OffersDetails { img = images, Name = Itemstemp[0], Id = Itemstemp[6], publishIcon = "Publishicon.png", tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                }
                                else if(Itemstemp[5] == "Save" || Itemstemp[5] == "Unpublish")
                                {
                                    offer1.Add(new OffersDetails { img = images, Name = Itemstemp[0], Id = Itemstemp[6], tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                   // offer2.Add(new OffersDetails { img = images, Name = Itemstemp[0], Id = Itemstemp[6], tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                }
                            }
                            else
                            {
                                if (Itemstemp[5] == "Publish")
                                {
                                    string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + Itemstemp[2];
                                    offer1.Add(new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0], Id = Itemstemp[6], publishIcon = "Publishicon.png", tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                    offer2.Add(new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0], Id = Itemstemp[6], publishIcon = "Publishicon.png", tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                }
                                else if (Itemstemp[5] == "Save" || Itemstemp[5] == "Unpublish")
                                {
                                    string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + Itemstemp[2];
                                    offer1.Add(new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0], Id = Itemstemp[6], tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                  //  offer2.Add(new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0], Id = Itemstemp[6], tc = Itemstemp[1], validfrom = Itemstemp[3], validto = Itemstemp[4], Status = Itemstemp[5], storeUID = storeUId });
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }


            }
            else
            {

            }

        }
    }

  
    public class OffersDetails
    {
        public string Id { get; set; }
        public ImageSource img { get; set; }
        public string Name { get; set; }
        public string Dt { get; set; }
        public ImageSource publishIcon { get; set; }
        public string tc { get; set; }
        public string validfrom { get; set; }
        public string validto { get; set; }
        public string Status { get; set; }
        public string storeUID { get; set; }

        
    }
}