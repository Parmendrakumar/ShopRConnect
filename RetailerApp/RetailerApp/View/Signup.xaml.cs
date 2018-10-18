using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Geolocator.Plugin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCLStorage;
using Plugin.Connectivity;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RetailerApp.Behavior;
using RetailerApp.Data;
using RetailerApp.Model;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Signup : ContentPage
    {
        String UserID = "";
        string pagename = "setpassword";
        string ImageName = "";
        string ProfileImageName = "";
        string LogoImageName = "";
        string StoreImageName = "";
        string StoreImageName2 = "";
        string StoreImageName3 = "";
        string userId = "";
        double lat, lng;
        string GoogleAccount = "";
        string Website = "";
        string StoreLogo = "";
        string StoreImage = "";
        string StoreImage2 = "";
        string StoreImage3 = "";
        string ProfilePhoto = "";
        string SendData = "";
        string CategoryString = "";
        string SubCategorystring = "";     
        Geocoder geoCoder;
        TimePicker opentime;
        TimePicker OpenDuration;
        TimeSpan tt;
        public MemberDatabase memberDatabase;
        public CategorySubCat catsubcat;
        public StateandCity statcity; 

        List<string> imagepath1;
        List<NewDataSet2> ObjPizzaList1 { get; set; }
        List<NewDataSet3> ObjPizzaList3 { get; set; }

        public Signup(string googleacc, string website)
        {
            InitializeComponent();
            try
            {
            CustomNavigationPage.SetTitlePosition(this, CustomNavigationPage.TitleAlignment.Center);
            CustomNavigationPage.SetTitleFont(this, Font.SystemFontOfSize(NamedSize.Large));
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));
            BindingContext = this;
            GoogleAccount = googleacc;
            Website = website;
            geoCoder = new Geocoder();
            if (Website == "Yes")
            {
                storewebsite.IsVisible = true;
            }
            else
            {
                storewebsite.IsVisible = false;
            }

                //GetSignUpDetails();
                if (CrossConnectivity.Current.IsConnected)
                {
                    GetStateService();
                    GetLocationAsync();
                    GetJSON();
                }
                else
                {
                    DisplayAlert("Alert", "Please check your Internet Connection", "Ok");
                }
                
                opentime = new TimePicker() { Time = new TimeSpan(08, 0, 0) };

                OpenDuration = new TimePicker() { Time = new TimeSpan(02, 0, 0) };

                tt = closingTime.Time - openingTime.Time;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
               
                MessagingCenter.Unsubscribe<Popup_ProfileImageDialog, string>(this, "blobimagename");
                MessagingCenter.Subscribe<Popup_ProfileImageDialog, string>(this, "blobimagename", (sender, value) =>
                {
                    bool ab = true;
                    if (ab == true && value != null)
                    {
                        ImageName = value;

                        int index = -1;
                        int index2 = -1;
                        int index3 = -1;
                        int index4 = -1;
                        int index5 = -1;

                        index = ImageName.IndexOf("ProfilePhoto");
                        index2 = ImageName.IndexOf("OneStorePhoto");
                        index3 = ImageName.IndexOf("StoreLogo");
                        index4 = ImageName.IndexOf("TwoStorePhoto");
                        index5 = ImageName.IndexOf("ThreeStorePhoto");


                        if (index == 0)
                        {
                            ProfileImageName = ImageName.Substring(ImageName.LastIndexOf("/") + 1);
                            ProfilePhoto = ImageName.Substring(index + 12);
                            profileimage.Source = ProfilePhoto;
                        }
                        else if (index2 == 0)
                        {
                            StoreImageName = ImageName.Substring(ImageName.LastIndexOf("/") + 1);
                            StoreImage = ImageName.Substring(index + 14);
                            storephoto.Source = StoreImage;
                        }
                        else if (index3 == 0)
                        {
                            LogoImageName = ImageName.Substring(ImageName.LastIndexOf("/") + 1);
                            StoreLogo = ImageName.Substring(index + 10);
                            logo.Source = StoreLogo;
                        }
                        else if (index4 == 0)
                        {
                            StoreImageName2 = ImageName.Substring(ImageName.LastIndexOf("/") + 1);
                            StoreImage2 = ImageName.Substring(index + 14);
                            storephoto2.Source = StoreImage2;
                        }
                        else if (index5 == 0)
                        {
                            StoreImageName3 = ImageName.Substring(ImageName.LastIndexOf("/") + 1);
                            StoreImage3 = ImageName.Substring(index + 16);
                            storephoto3.Source = StoreImage3;
                        }



                    //  ImageName2 = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteArray.ToArray()));

                }

                    ab = false;
                    value = null;
                });
                businesscategory.Text = string.Empty;
                subcategory.Text = string.Empty;
                CategoryString = string.Empty;
                SubCategorystring = string.Empty;
                SendData = "";

                //memberDatabase = new MemberDatabase();
                //var members = memberDatabase.GetCatSubCa();
                //foreach (CategorySubCat var1 in members)
                //{
                //    SendData = SendData + var1.Id + "|" + var1.SubCategoryId + "@";
                //    CategoryString = CategoryString + " " + var1.CategoryName;
                //    SubCategorystring = SubCategorystring + " " + var1.SubCategory;
                //}

                //businesscategory.Text = CategoryString;
                //subcategory.Text = SubCategorystring;
                businesscategory.Text = string.Empty;
                subcategory.Text = string.Empty;
                CategoryString = string.Empty;
                SubCategorystring = string.Empty;

                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetCatSubCa();

                string catname = "";

                foreach (CategorySubCat var1 in members.Where(c => c.SubTextColor == "#FF0000"))
                {
                    catname = var1.CategoryName;
                    if (SendData.Contains(var1.CategoryID))
                    {
                        SendData = SendData + "`" + var1.SubCategoryID;
                    }
                    else
                    {
                        SendData = SendData + "@" + var1.CategoryID + "|" + "`" + var1.SubCategoryID;
                    }

                    SubCategorystring = SubCategorystring + " " + var1.SubCategoryName;

                    if (!CategoryString.Contains(catname))
                    {
                        CategoryString = CategoryString + " " + var1.CategoryName;
                    }
                }

                businesscategory.Text = CategoryString;
                subcategory.Text = SubCategorystring;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        protected override void OnDisappearing()
        {
            //   TextContainer.Clear();
            MessagingCenter.Unsubscribe<Popup_ProfileImageDialog, string>(this, "blobimagename");

        }

        public async void uploadImage()
        {
            try
            {
                imagepath1 = new List<string>();
                imagepath1.Add(StoreLogo);
                imagepath1.Add(ProfilePhoto);
                imagepath1.Add(StoreImage);
                imagepath1.Add(StoreImage2);
                imagepath1.Add(StoreImage3);
               
                foreach (string str in imagepath1)
                {
                    if (str != "")
                    {
                        var Filepath2 = System.IO.Path.GetDirectoryName(str);
                        var content = new MultipartFormDataContent();
                        await Task.Delay(TimeSpan.FromSeconds(0.01));
                        var fileName = str.Substring(str.LastIndexOf("/") + 1);

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
                    }
                }
            }
            catch(Exception ex)
            {
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
        private async void AddPhoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "OneStorePhoto";
                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void AddPhoto_Clicked2(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "TwoStorePhoto";
                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void AddPhoto_Clicked3(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "ThreeStorePhoto";
                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void AddProfile_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "ProfilePhoto";
                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void AddLogo_Clicked(object sender, EventArgs e)
        {
            try
            {
                string Imagetype = "StoreLogo";
                var _Popupspec = new Popup_ProfileImageDialog(Imagetype);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        #region "GetLocationAsync Function [Use to Get Lat Lng]"
       public async void GetLocationAsync()
        {
           
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Location);

                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Plugin.Permissions.Abstractions.Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Plugin.Permissions.Abstractions.Permission.Location))
                        status = results[Plugin.Permissions.Abstractions.Permission.Location];
                }
                if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync(10000);
                    // LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;

                    lat = results.Latitude;
                    lng = results.Longitude;

                    var position = new Position(lat, lng);
                    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                    var address1 = (from s in possibleAddresses select s).FirstOrDefault();
                    List<string> addressParse = address1.Split(",".ToCharArray()).ToList<string>();
                    System.Diagnostics.Debug.WriteLine(addressParse);
                    storeaddress.Text = string.Empty;
                    storeaddress.Text = address1;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }

                //if (CrossGeolocator.Current.IsGeolocationAvailable && CrossGeolocator.Current.IsGeolocationEnabled)
                //{
                //    CrossGeolocator.Current.DesiredAccuracy = 100;
                //    var getLatLng = await CrossGeolocator.Current.GetPositionAsync(20000, null, true);
                   
                //    lat = getLatLng.Latitude;
                //    lng = getLatLng.Longitude;

                //    var position = new Position(lat, lng);
                //    var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(position);

                //    var address1 = (from s in possibleAddresses select s).FirstOrDefault();                    
                //    List<string> addressParse = address1.Split(",".ToCharArray()).ToList<string>();
                //    System.Diagnostics.Debug.WriteLine(addressParse);
                //    storeaddress.Text = address1;
                //    //foreach (var address in possibleAddresses)
                //    //{
                //    //    string addressString = address.ToString();
                //    //    List<string> addressParse = addressString.Split("\n".ToCharArray()).ToList<string>();
                //    //    System.Diagnostics.Debug.WriteLine(addressParse[0]);

                //    //}
                
            }
            catch (Exception ex)
            {
                //     textLocation.Text = "Unable to get location: " + ex.ToString();
                //System.Diagnostics.Debug.WriteLine("DemoService", "2GetLocationAsync:" + ex.InnerException.ToString() + DateTime.Now.ToString());
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        #endregion
        private async void CreateAccount(object sender, EventArgs e)
        {
           var isMobileValid = App.Current.Properties.ContainsKey("IsMobileValid") ? (bool)App.Current.Properties["IsMobileValid"] : false;
           try
            {
                Boolean boolisValid = await IsValid();
                if (boolisValid && isMobileValid)
                {
                  
                    IsBusy = true;
                    uploadImage();
                    string MobileNumber = mobilenumber.Text;
                    string Storename = storename.Text;
                    string StoreAddress = storeaddress.Text;

                    if(StoreAddress.Contains("&"))
                    {
                        StoreAddress =  StoreAddress.Replace("&", "");
                    }

                    string storeAddress2 = storeaddress2.Text;
                    string StoreCity = storestate.Items[storestate.SelectedIndex].ToString() + ":" + othercity.Text;
                    if (StoreCity.Contains("&"))
                    {
                        StoreCity = StoreCity.Replace("&", "");
                    }
                    string StoreState = storecity.Items[storecity.SelectedIndex].ToString();
                    if (StoreState.Contains("&"))
                    {
                        StoreState = StoreState.Replace("&", "");
                    }
                    string StoreZip = storezip.Text;
                    string StorePhone = storephone.Text;
                    string StoreEmail = storeemail.Text;
                    string StoreWebsite = storewebsite.Text;
                    string StoreArea = storearea.Text;
                    string Businesscategory = businesscategory.Text;
                    string SubCategory = subcategory.Text;
                    string openingHour = openingTime.Time.ToString();
                    string ClosingTime = closingTime.Time.ToString();

                     string StoreImageAll = "Store" + ":" + StoreImageName + "`" + StoreImageName2 + "`" + StoreImageName3;
                  //  string StoreImageAll = "Store:" + StoreImageName;
                    string StoreLogoAll = "Logo:" + LogoImageName;
                    string StoreProfile = "Profile:" + ProfileImageName;
                    //  string Verificationcode = verificationcode.Text;                

                    // Navigation.PushAsync(new SetPassword());
                    PlayerIds deviceId = await OneSignal.Current.IdsAvailableAsync();
                    string deviceid = deviceId.PlayerId;
                    MobileNumber = mobilenumber.Text;

                    //if (SendData.Contains("&"))
                    //{
                    //    SendData = SendData.Replace("&", "_");
                    //}

                    //  string Inputs = "Mobile=" + MobileNumber + "&UserType=" + "Retailer" + "&DeviceID=" + deviceid;
                    string Inputs = "StoreName=" + Storename + "&StoreAddress=" + StoreAddress + "&City=" + StoreCity + "&State=" + StoreState + "&MainArea=" + StoreArea +
                        "&ZipCode=" + StoreZip + "&Phone=" + StorePhone + "&Mobile=" + MobileNumber + "&Email=" + StoreEmail + "&Website=" + StoreWebsite + "&Latitude=" + lat + "&Longitude=" + lng +
                        "&StoreImage=" + StoreImageAll.ToString() + "&StoreAddress2=" + storeAddress2 + "&BusinessCategory=" + SendData +
                        "&StoreLogo=" + StoreLogoAll + "&UserProfilePhoto=" + StoreProfile.ToString() + "&IsGoogleBusinessAccount=" + GoogleAccount + "&IsWebsite=" + Website + "&DeviceId=" + deviceid.ToString()
                        + "&BusinessSubCategory=" + "SubCategory" + "&StoreOpeningTime=" + openingHour + "&StoreClosingTime=" + ClosingTime.ToString();

                    string resp = await RestService.PostRegistration(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                    if (result != "0")
                    {
                        UserID = result;
                    }

                    if (UserID != "0" && UserID !="")
                    {
                        await DisplayAlert("Alert", "Submit successfully...!", "Ok");
                        // var _Popupspec = new PopupOTPDialog(pagename, UserID, MobileNumber);
                        if (GoogleAccount == "Yes")
                        {
                            string pageName = "setpassword";
                            var _Popupspec = new PopupOTPDialog(UserID, pageName);
                            await Navigation.PushPopupAsync(_Popupspec);
                        }
                        else if (GoogleAccount == "No")
                        {
                            await Navigation.PopToRootAsync();
                        }
                    }
                    else if (UserID == "0" || UserID == "")
                    {                       
                        await DisplayAlert("Alert", "Mobile number already exist", "Ok");
                    }                   

                    else 
                    {
                        await DisplayAlert("Alert", "Network Issue", "Ok");
                    }

                   
                    IsBusy = false;
                }
                else
                {
                    //   await DisplayAlert("Alert", "Invalid Mobile Number", "Ok");
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                Alogin.IsRunning = false;
                Alogin.IsVisible = false;
                IsBusy = false;
            }


        }

      
        public async void GetStateService1()
        {
            memberDatabase = new MemberDatabase();
            try
            {
                statcity = new StateandCity();
                
                //  string resp = await RestService.GetStateCity1(Inputs);
                Uri geturi = new Uri("http://elixirct.in/ShopRConservicePublish/api/Login/GetStateAndCity"); //replace your xml url            
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();//Getting response   
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(response);
                ObjPizzaList3 = new List<NewDataSet3>();
                XDocument doc = XDocument.Parse(result);
                foreach (var item in doc.Descendants("Table"))
                {
                    NewDataSet3 ObjPizzaItem1 = new NewDataSet3();
                    ObjPizzaItem1.StateId = item.Element("StateId").Value.ToString();
                    ObjPizzaItem1.StateName = item.Element("StateName").Value.ToString();
                    ObjPizzaItem1.CityId = item.Element("CityId").Value.ToString();
                    ObjPizzaItem1.CityName = item.Element("CityName").Value.ToString();
                    System.Diagnostics.Debug.WriteLine(item);
                    ObjPizzaList3.Add(ObjPizzaItem1);

                    statcity.StateId = ObjPizzaItem1.StateId;
                    statcity.StateName = ObjPizzaItem1.StateName;
                    statcity.CityId = ObjPizzaItem1.CityId;
                    statcity.CityName = ObjPizzaItem1.CityName;                  
                    memberDatabase.AddStatename(statcity);


                }
                BindStateCity();

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                BindStateCity();
            }
        }

        public async void GetStateService()
        {
            string Inputs = "ScreenName=" + "State" + "&StateId=" + "";
            string resp = await RestService.GetStateCity(Inputs);
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
                            storestate.Items.Add(Itemstemp[1]);
                           

                        }

                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                   
                }
            }
        }
        public async void BindStateCity()
        {
            try
            {
                var catesubcat = memberDatabase.GetStateCity();

                var list = catesubcat
                      .GroupBy(a => a.StateName)
                        .Select(g => g.First())
                        .ToList();

                foreach (var var1 in list)
                {
                    storestate.Items.Add(var1.StateName);
                }
            }
            catch
            {

            }
        }
        public async void GetSignUpDetails()
        {
            try
            {
                PlayerIds deviceId = await OneSignal.Current.IdsAvailableAsync();
                string deviceid = deviceId.PlayerId;
                string Inputs = "ScreenName=" + "State" + "&StateId=" + "";
                string resp = await RestService.GetStateCity(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                if (result != "")
                {

                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Unable to find signup data");
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        private async void ToolLogin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            int _limit = 10;
            string _text = mobilenumber.Text;
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                mobilenumber.Text = _text;        //Set the Old value
            }
        }
        private void Entry_TextChangedPhone(object sender, TextChangedEventArgs e)
        {
            int _limit = 15;
            string _text = storephone.Text;
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                storephone.Text = _text;        //Set the Old value
            }
        }

     
        private void businesscategory_Focused(object sender, FocusEventArgs e)
        {
            Navigation.PushAsync(new CategorySubCategory1());
        }

        private void storezip_TextChanged(object sender, TextChangedEventArgs e)
        {
            int _limit = 6;
            string _text = storezip.Text;
            if (_text.Length > _limit)       //If it is more than your character restriction
            {
                _text = _text.Remove(_text.Length - 1);  // Remove Last character
                storezip.Text = _text;        //Set the Old value


            }
        }

        private async void storestate_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            try
            {
             
                CityIndicator.IsEnabled = true;
                CityIndicator.IsVisible = true;
                CityIndicator.IsRunning = true;
                //int index = ((Picker)sender).SelectedIndex;
                //int stateid = index;
                storecity.Items.Clear();
                storecity.Items.Add("City");
                storecity.SelectedIndex = 0;

                int index = ((Picker)sender).SelectedIndex;
               // int stateid = index + 1;
                string Inputs = "ScreenName=" + "City" + "&StateId=" + index;
                string resp = await RestService.GetCity(Inputs);
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                if (result != "")
                {
                    
                    string[] arrSepr2 = result.Split('|');
                    if (arrSepr2.Length > 1)
                    {
                        foreach (string word in arrSepr2)
                        {
                            if (word != "")
                            {
                                try
                                {
                                    string[] Itemstemp = word.Split('`');
                                    storecity.Items.Add(Itemstemp[1]);
                                   
                                }
                                catch(Exception ex)
                                {
                                    System.Diagnostics.Debug.WriteLine(ex);
                                    CityIndicator.IsEnabled = false;
                                    CityIndicator.IsVisible = false;
                                    CityIndicator.IsRunning = false;
                                }
                            }                            
                        }
                       
                        storecity.Items.Add("Other");
                        CityIndicator.IsEnabled = false;
                        CityIndicator.IsVisible = false;
                        CityIndicator.IsRunning = false;
                    }


                }
                else
                {
                    storecity.Items.Add("Other");
                    CityIndicator.IsEnabled = false;
                    CityIndicator.IsVisible = false;
                    CityIndicator.IsRunning = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                CityIndicator.IsEnabled = false;
                CityIndicator.IsVisible = false;
                CityIndicator.IsRunning = false;
            }
        }

        private async void storestate_SelectedIndexChanged2(object sender, EventArgs e)
        {
            try
            {
                storecity.Items.Clear();
                CityIndicator.IsEnabled = true;
                CityIndicator.IsVisible = true;
                CityIndicator.IsRunning = true;

                var catesubcat = memberDatabase.GetStateCity();

                string pp = storestate.SelectedItem.ToString();

                foreach (var category in catesubcat.Where(x => x.StateName == pp))
                {
                    storecity.Items.Add(category.CityName);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                CityIndicator.IsEnabled = false;
                CityIndicator.IsVisible = false;
                CityIndicator.IsRunning = false;
            }
            CityIndicator.IsEnabled = false;
            CityIndicator.IsVisible = false;
            CityIndicator.IsRunning = false;
        }
        public async void storecity_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string value1 = storecity.Items[storecity.SelectedIndex].ToString();

                if (value1 == "Other")
                {
                    othercity.IsVisible = true;
                }
                else
                {
                    othercity.IsVisible = false;
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async Task<Boolean> IsValid()
        {
          
      

            if (storename.Text == null || storename.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Store Name","Ok");
                storename.Focus();
                return false;
            }
           
            else if ( storeaddress.Text == null || storeaddress.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Store Address", "Ok");
                storeaddress.Focus();
                return false;
            }
            else if (storearea.Text == null || storearea.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Store Main Area", "Ok");
                storearea.Focus();
                return false;
            }

            else if (storestate.SelectedIndex == 0 || storestate.SelectedIndex == -1)
            {
                var answer = await DisplayAlert("", "Please Select State", null, "Ok");               
                return false;
            }
            else if (storecity.SelectedIndex == -1)
            {
                var answer = await DisplayAlert("", "Please Select City", null, "Ok");
                return false;
            }

            else if(othercity.IsVisible == true && othercity.Text == null || othercity.Text == "")
            {
                var answer = await DisplayAlert("", "Please Enter Other City", null, "Ok");
                return false;
            }

            else if (storezip.Text == null || storezip.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Store Zip code", "Ok");
                storezip.Focus();
                return false;
            }
            else if (storezip.Text.Trim() == "000000")
            {
                await DisplayAlert("Alert", "Please Enter Valid Store Zip code", "Ok");
                storezip.Focus();
                return false;
            }
            else if (!Regex.Match(storezip.Text, @"^\d{6,}$").Success)
            {
                await DisplayAlert("", "Please enter valid Zip code", null, "Ok");
                return false;
            }

            else if (businesscategory.Text == null || businesscategory.Text == "")
            {
                await DisplayAlert("Alert", "Please add Business category", "Ok");
                businesscategory.Focus();
                return false;
            }
           
           
            else if (mobilenumber.Text == null || mobilenumber.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Mobile Number", "Ok");
                mobilenumber.Focus();
                return false;
            }
            else if (!Regex.Match(mobilenumber.Text, @"^[6-9][0-9]{9}$").Success)
            {
                await DisplayAlert("", "Please enter valid Mobile number", null, "Ok");
                mobilenumber.Focus();
                return false;
            }
            else if (businesscategory.Text == null || businesscategory.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter Category", "Ok");
             
                return false;
            }
            else if (storeemail.Text == null || storeemail.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Enter EmailId", "Ok");
                storeemail.Focus();
                return false;
            }
            else if (!Regex.Match(storeemail.Text, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$").Success)
            {
                await DisplayAlert("", "Please enter Valid Email Id", null, "Ok");
                storeemail.Focus();
                return false;
            }

            else if (openingTime.Time.ToString() == "00:00:00")
            {
                var answer = await DisplayAlert("", "Please Select Opening Time", null, "Ok");
                return false;
            }
            else if (closingTime.Time.ToString() == "00:00:00")
            {
                var answer = await DisplayAlert("", "Please Select Closing Time", null, "Ok");
                return false;
            }
           

          

            else if (openingTime.Time > closingTime.Time)
            {
               await DisplayAlert("Alert", "Opening time can't be greater than closing time", "Ok");
               return false;
            }
            else if (opentime.Time > openingTime.Time)
            {
                await DisplayAlert("Alert", "Opening time should be greater than 8AM", "Ok");
                return false;
            }


            else if (ProfileImageName == "")
            {
                var answer = await DisplayAlert("", "Please Select Profile Picture", null, "Ok");
                return false;
            }
            else if (LogoImageName == "")
            {
                var answer = await DisplayAlert("", "Please Select Store Logo", null, "Ok");
                return false;
            }
            else if (StoreImageName == "")
            {
                var answer = await DisplayAlert("", "Please Select Store Picture One", null, "Ok");
                return false;
            }
            else if (StoreImageName2 == "")
            {
                var answer = await DisplayAlert("", "Please Select Store Picture Two", null, "Ok");
                return false;
            }
            else if (StoreImageName3 == "")
            {
                var answer = await DisplayAlert("", "Please Select Store Picture Three", null, "Ok");
                return false;
            }

            else if (Website == "Yes")
            {
                string webs = "";
                webs = storewebsite.Text;
                string website1 = @"^(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})$";
                if (string.IsNullOrEmpty(webs))
                {
                    var answer = await DisplayAlert("", "Please Enter Website", null, "Ok");
                    storewebsite.Focus();
                    return false;
                }
                else
                {
                    Match result = Regex.Match(webs, website1);
                    if (!result.Success)
                    {
                        await DisplayAlert("Alert", "Please Enter Valid Website", "Ok");
                        return false;
                    }

                }
               
            }


            return true;
        }

        public async void GetJSON()
        {
            try
            {
                catsubcat = new CategorySubCat();
                memberDatabase = new MemberDatabase();

                memberDatabase.DeleteCatSubCat();
                //  var client = new System.Net.Http.HttpClient();
                // var response = await client.GetStringAsync("http://elixirct.in/ShopRConservicePublish/api/Login/GetCatSubCatDataSet");

                Uri geturi = new Uri("http://elixirct.in/ShopRConservicePublish/api/Login/GetCatSubCatDataSet"); //replace your xml url   
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();//Getting response   
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(response);

                ObjPizzaList1 = new List<NewDataSet2>();
                XDocument doc = XDocument.Parse(result);
                foreach (var item in doc.Descendants("Table"))
                {
                    NewDataSet2 ObjPizzaItem1 = new NewDataSet2();
                    ObjPizzaItem1.CategoryID = item.Element("CategoryID").Value.ToString();
                    ObjPizzaItem1.CategoryName = item.Element("CategoryName").Value.ToString();
                    ObjPizzaItem1.SubCategoryID = item.Element("SubCategoryID").Value.ToString();
                    ObjPizzaItem1.SubCategoryName = item.Element("SubCategoryName").Value.ToString();
                    System.Diagnostics.Debug.WriteLine(item);
                    ObjPizzaList1.Add(ObjPizzaItem1);


                    catsubcat.CategoryID = ObjPizzaItem1.CategoryID;
                    catsubcat.CategoryName = ObjPizzaItem1.CategoryName;
                    catsubcat.SubCategoryID = ObjPizzaItem1.SubCategoryID;
                    catsubcat.SubCategoryName = ObjPizzaItem1.SubCategoryName;
                    catsubcat.SubTextColor = "#000000";
                    memberDatabase.AddCategorySub(catsubcat);



                    //if (members.Any())
                    //{
                    //    memberDatabase.UpdateCategorySub(catsubcat);
                    //}
                    //else
                    //{
                    //    memberDatabase.AddCategorySub(catsubcat);
                    //}
                }

                //foreach (var category in ObjPizzaList.Select(x => x.CategoryName).Distinct())
                //{
                //    CategoryItems.Add(new CategorySubC { CategoryName = category });
                //}



            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

        }

    }
    public class NewDataSet2
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

    }
    public class NewDataSet3
    {
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }

    }

}