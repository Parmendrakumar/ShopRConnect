using Plugin.Connectivity;
using RetailerApp.AzureData;
using RetailerApp.Data;
using RetailerApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        string UserID = "";
        string StoreId = "";
        string StoreuID = "";
      
        public Home()
        {
            InitializeComponent();

            BindingContext = new DemoHandsetPendingRPTViewModel();
           
          
            activelistview.RefreshCommand = new Command(() => {

                if (CrossConnectivity.Current.IsConnected)
                {

                    BindingContext = new DemoHandsetPendingRPTViewModel();
                    activelistview.IsRefreshing = false;
                }
                else
                {
                    DisplayAlert("Alert", "Please check your internet connection", "OK");
                }
            });

           
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
        protected override void OnAppearing()
        {

            base.OnAppearing();

            try
            {
                act.IsRunning = true;
                act.IsVisible = true;
                BindingContext = new DemoHandsetPendingRPTViewModel();
                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetUserDetail();

                UserDetail userin = (from blog in members
                                     select blog).FirstOrDefault();
                if (userin != null)
                {
                    UserID = userin.Userid;
                    var imlocal = userin.Imagename;
                    string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + imlocal;
                    StoreId = userin.StoreId;
                    profileimage2.Source = url;             
                    
                }
                else
                {
                    //  Navigation.PushAsync(new MainPage());
                }


                act.IsRunning = false;
                act.IsVisible = false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                act.IsRunning = false;
                act.IsVisible = false;
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           

            activelistview.IsVisible = true;
            inactivelistview.IsVisible = false;

            box1.Color = Color.White;
            box2.Color = Color.FromHex("#011c1a");

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
         

            activelistview.IsVisible = false;
            inactivelistview.IsVisible = true;
            box2.Color = Color.White;
            box1.Color = Color.FromHex("#011c1a");


        }
        public async void activelistview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dataItem = e.Item as HistoryName;
            string Id = dataItem.Id;
            string name = dataItem.Name;
            string ConsumerID = dataItem.ConsumerUID;
            string StoreuId = dataItem.StoreUID;
            string CName = dataItem.CustomerName;


            await Navigation.PushAsync(new Chat(Id, name, ConsumerID, StoreuId, CName));
        }
        public async void Location_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SelectLocation());
        }

        private void NotificationClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Notification());
        }

        private void ProfileClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile());
        }

        private async void activelistview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dataItem = e.SelectedItem as HistoryName;
            string Id = dataItem.Id;
            string name = dataItem.Name;
            string ConsumerID = dataItem.ConsumerUID;
            string StoreuId = dataItem.StoreUID;
            string CName = dataItem.CustomerName;

            await Navigation.PushAsync(new Chat(Id, name, ConsumerID, StoreuId, CName));
        }
    }
   public class DemoHandsetPendingRPTViewModel
    {
        public MemberDatabase memberDatabase;
        public UserInfo userdetail;
        public UserDetail userdetail2;
        string UserID = "";
        string StorID = "";
        string storeUId = "";

        public ObservableCollection<HistoryName> active { get; set; }
        public ObservableCollection<HistoryName> inactive { get; set; }

        public ObservableCollection<UserInfo> Userdetail { get; set; }
        public DemoHandsetPendingRPTViewModel()
        {
          
            active = new ObservableCollection<HistoryName>();
            inactive = new ObservableCollection<HistoryName>();
            Userdetail = new ObservableCollection<UserInfo>();

            if (CrossConnectivity.Current.IsConnected)
            {
                GetQuery();
            }

           
          Device.StartTimer(TimeSpan.FromMinutes(5), () =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        GetQuery();
                    }
                    catch(Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Auto Refresh Error");
                    }
                }
                return true;
            });

            
        }



        public async void GetQuery()
        {
            try { 
                memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();

            UserID = userin.Userid;
            StorID = userin.StoreId;
            storeUId = userin.StoreUId;
             }
            catch(Exception ex)
            {
                App.Current.Properties["IsLoggedIn"] = false;
            }
                string Inputs = "StoreUId=" + storeUId;
                string resp = await RestService.GetQuery(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
            
            if (result != "")
            {
             //   string[] arrSepr1 = resp.Split('^');
               // Application.Current.Properties["UserName"] = arrSepr1[1];
               // Application.Current.Properties["Password"] = arrSepr1[2];

                try
                {
                 //   string outList = arrSepr1[3];
                  //  string storeList = result.Split('$')[0];
                    string[] arrSepr2 = result.Split('|');
                  
                    if (arrSepr2.Length > 1)
                    {
                      //  active.Clear();
                      //  inactive.Clear();
                        foreach (string word in arrSepr2)
                        {
                            string[] Itemstemp = word.Split('`');
                            if (Itemstemp[9] == "1")
                            {
                                if (Itemstemp[10] != "0")
                                {
                                    active.Add(new HistoryName
                                    {
                                        Id = Itemstemp[0],
                                        Dt = Itemstemp[4],
                                        Name = Itemstemp[1] + " " + Itemstemp[2], CustomerName = Itemstemp[6],
                                        StoreUID = Itemstemp[7],
                                        ConsumerUID = Itemstemp[8],
                                        unread = Itemstemp[10],
                                        bcolor = Color.Red });
                                 }
                                else
                                {
                                    active.Add(new HistoryName
                                    {
                                        Id = Itemstemp[0],
                                        Dt = Itemstemp[4],
                                        Name = Itemstemp[1] + " " + Itemstemp[2],
                                        CustomerName = Itemstemp[6],
                                        StoreUID = Itemstemp[7],
                                        ConsumerUID = Itemstemp[8],
                                        unread = Itemstemp[10],
                                        bcolor = Color.White
                                    });
                                }
                            }
                           else if (Itemstemp[9] == "0")
                            {
                                inactive.Add(new HistoryName { Id = Itemstemp[0], Dt2 = Itemstemp[4], Name2 = Itemstemp[1] + " " + Itemstemp[2], CustomerName = Itemstemp[6], StoreUID = Itemstemp[7], ConsumerUID = Itemstemp[8], unread = Itemstemp[10] });
                            }
                        }
                       // activelistview.IsRefreshing = false;

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
    public class HistoryName
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Dt { get; set; }
        public string Name2 { get; set; }
        public string Dt2 { get; set; }
        public Color TextColorA { get; set; }
        public Color TextColorI { get; set; }
        public string CustomerName { get; set; }
        public string StoreUID { get; set; }
        public string ConsumerUID { get; set; }
        public string unread { get; set; }
        public Color bcolor { get; set; }
      
       



    }

    public class UserInfo
    {
        public string userId { get; set; }
        public string Name { get; set; }
        public string Retailer { get; set; }
        public string Email { get; set; }
        public string Mobileid { get; set; }

    }

}