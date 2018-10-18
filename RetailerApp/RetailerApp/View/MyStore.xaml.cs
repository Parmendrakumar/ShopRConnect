using RetailerApp.Data;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyStore : ContentPage
    {
        MainViewModel _vm;
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        public UserDetail userdetail2;
        public StoreImageCarsoul storeImage;
        string UserID = "";
        string StorID = "";
        string storeUId = "";
        public MyStore()
        {
            InitializeComponent();

                BindingContext = _vm = new MainViewModel();

            //    BindingContext = new MyStoreDetails();

           // BindingContext = this;
        }

        protected override async void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetUserDetail();

                UserDetail userin = (from blog in members
                                     select blog).FirstOrDefault();

                UserID = userin.Userid;
                StorID = userin.StoreId;
                storeUId = userin.StoreUId;

                string Inputs = "StoreUId=" + storeUId;
                string resp = await RestService.GetStoreDetail(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                if (result != "")
                {

                    try
                    {
                        string[] Itemstemp = result.Split('`');
                        string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + Itemstemp[16];
                        //     active.Add(new StoreData { Id = Itemstemp[0], Name = Itemstemp[1], Address = Itemstemp[8] + "" + Itemstemp[17], StoreLogo = url, MyItemsSource = url });

                        DateTime time1 = DateTime.Parse(Itemstemp[14]);
                        DateTime time2 = DateTime.Parse(Itemstemp[15]);

                        StrName.Text = Itemstemp[1];
                        storeLogo.Source = url;
                        Category.Text = Itemstemp[19];
                       // SubCategory.Text= Itemstemp[25];
                        address.Text = Itemstemp[8] + "," + Itemstemp[18] + "," + Itemstemp[3] + "," + Itemstemp[2];
                        upenclosetime.Text = Itemstemp[14] + " To " + Itemstemp[15];
                        contextno.Text = Itemstemp[10];

                     

                        storeImage = new StoreImageCarsoul()
                        {
                            Imagename1 = Itemstemp[22],
                            Imagename2 = Itemstemp[23],
                            Imagename3 = Itemstemp[24]
                        };
                           
                        memberDatabase.AddStoreImage(storeImage);
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
            catch (Exception ex)
            {

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
        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            Debug.WriteLine("Position " + e.NewValue + " selected.");
        }

        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            Debug.WriteLine("Scrolled to " + e.NewValue + " percent.");
            Debug.WriteLine("Direction = " + e.Direction);
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new Popup_MyRating());
            try
            {
                var _Popupspec = new Popup_MyRating();
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RetailerInformation());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddOffer());
        }

        private void OfferMenuClick(object sender, EventArgs e)
        {

        }

       
    }

   
}