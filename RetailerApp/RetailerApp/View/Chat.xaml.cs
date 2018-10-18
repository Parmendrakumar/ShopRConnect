using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.Net.Http;
using RetailerApp.Model;
using Plugin.Media.Abstractions;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using RetailerApp.AzureData;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Plugin.Connectivity;
using RetailerApp.Data;
using PCLStorage;
using FFImageLoading.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Chat : ContentPage
    {
       //  public SignalRClient SignalRClient = new SignalRClient("http://elixirct.in/ShopRConnectChatService");
        public SignalRClient SignalRClient = new SignalRClient("http://savechatdata.azurewebsites.net/");


        public MemberDatabase memberDatabase;
        public Member member;
        public ObservableCollection<MessageText> TextContainer { get; set; }
        public ObservableCollection<Xamarin.Forms.View> TextContainer2 { get; set; }

        public ListView listView;
        TodoItemManager manager1;     
        public event OnMessageSent MessageSent;
        public delegate void OnMessageSent(string message);
        ItemManager manager;
        public string FileName = "";
        //   string uploadedFilename="";
        byte[] byteData;
        public MediaFile file;
        public ImageSource imlocal = "";
        public string QueryId = "";
        public string QueryName = "";
        public string Consumerid = "";
        public string Storeuid = "";
        public string CustName = "Ashutosh";
        public string UserNameSentMessage = "";
        public Chat(string id, string name, string consumerid, string storeuid, string Customername)
        {   
            TextContainer = new ObservableCollection<MessageText>();
            listView = new ListView();
            QueryId = id;
            QueryName = name;
            Consumerid = consumerid;
            Storeuid = storeuid;
            CustName = Customername;
        
            BindingContext = this;

            InitializeComponent();
            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            Title = QueryName;
            // BindData();
            //   CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            manager1 = TodoItemManager.DefaultManager;

            SignalRClient.Start().ContinueWith(task => {
                if (task.IsFaulted)
                    DisplayAlert("Error", "An error occurred when trying to connect to SignalR: " + task.Exception.InnerExceptions[0].Message, "OK");
            });

            memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();
            if (userin != null)
            {
                UserNameSentMessage = userin.Name;
                UsernameTextbox.Text = userin.Name;
            }

              
              //  BindData();

                //  BindFromAzure();
                if (CrossConnectivity.Current.IsConnected)
                {
                    BindFromAWS();
                }
              

           
            else
            {
                BindData();
            }

            SignalRClient.Start();

            //try to reconnect every 10 seconds, just in case
            Device.StartTimer(TimeSpan.FromSeconds(5), () => {
                if (!SignalRClient.IsConnectedOrConnecting)
                    SignalRClient.Start();

                return true;
            });

            try
            {
                MessageSent += async (uploadedFilename) =>
               {
                   try
                   {
                       var tt = DateTime.Now;
                       var text = uploadedFilename;                  
                       

                       if (text.StartsWith("awsimage"))
                       {
                           var sendMessage = text.Substring(text.LastIndexOf("/") + 1);
                           sendMessage = "awsimage" + sendMessage;
                           SignalRClient.SendMessage(UsernameTextbox.Text, sendMessage);

                           int index = text.IndexOf("awsimage");
                           uploadedFilename = text.Substring(index + 8);
                           
                           // string images = "https://maudit.elixirct.net/UploadFileToServer/Uploads/" + uploadedFilename;
                          // var images = DependencyService.Get<IFileService>().GetPictureFromDisk(uploadedFilename, getGalleryPath());
                           TextContainer.Add(new MessageText { imgsource = uploadedFilename, imgheight = "200", imgwidth = "200", Status = "Sent", DateSent = DateTime.Now, StoreNameMessage = UserNameSentMessage });

                           var last4 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                           ListView.ScrollTo(last4, ScrollToPosition.MakeVisible, false);

                           uploadedFilename = uploadedFilename.Substring(uploadedFilename.LastIndexOf("/") + 1);

                           try
                           {    
                                   MemoryStream ms = new MemoryStream();
                                   member = new Member();
                                   memberDatabase = new MemberDatabase();
                                   member.Text = uploadedFilename;
                                   member.Status = "Sent";
                                   member.Datetime = DateTime.Now;
                                   member.Query_ID = QueryId;
                                   member.Type = "IMG";

                                   if (member != null)
                                   {
                                       memberDatabase.AddMember(member);
                                   }

                               string Inputs = "QueryId=" + QueryId + "&ChatText=" + uploadedFilename + "&StoreUID=" + Storeuid + "&ConumerUniqueID=" + Consumerid +
                        "&ChatType=" + "IMG" + "&IsActive=" + "1" + "&Status=" + "Sent" + "&Rating=" + "";
                               string resp = await RestService.SaveChat(Inputs);
                               var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                           }
                           catch(Exception ex)
                           {
                               System.Diagnostics.Debug.WriteLine(ex);
                           }
                           
                          
                       }
                       else
                       {

                       SignalRClient.SendMessage(UsernameTextbox.Text, uploadedFilename);
                         

                       TextContainer.Add(new MessageText { Text = text, imgheight = "10", imgwidth = "10", imgsource = imlocal, Status = "Sent", DateSent = DateTime.Now, StoreNameMessage = UserNameSentMessage });
                       var last2 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                       ListView.ScrollTo(last2, ScrollToPosition.MakeVisible, true);

                           try
                           {
                               string Inputs = "QueryId=" + QueryId + "&ChatText=" + text + "&StoreUID=" + Storeuid + "&ConumerUniqueID=" + Consumerid +
                               "&ChatType=" + "Text" + "&IsActive=" + "1" + "&Status=" + "Sent" + "&Rating=" + "";
                               string resp = await RestService.SaveChat(Inputs);
                               var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);

                               if(result != "0")
                               {
                                   MemoryStream ms = new MemoryStream();
                                   member = new Member();
                                   memberDatabase = new MemberDatabase();
                                   member.Text = uploadedFilename;
                                   member.Status = "Sent";
                                   member.Datetime = DateTime.Now;
                                   member.Query_ID = QueryId;
                                   member.Type = "Text";

                                   if (member != null)
                                   {
                                       memberDatabase.AddMember(member);
                                   }
                               }
                           }
                           catch(Exception ex)
                           {
                               System.Diagnostics.Debug.WriteLine(ex);
                           }
                           
                       }          
                       
                       var last = ListView.ItemsSource.Cast<object>().LastOrDefault();
                       ListView.ScrollTo(last, ScrollToPosition.MakeVisible, false);                    
                       imlocal = null;                                         

                   }
                   catch (Exception ex)
                   {
                       await DisplayAlert("Alert", "Please wait 5 second, The connection has not been established", "Ok");
                       System.Diagnostics.Debug.WriteLine(ex);
                   }

               };

                SignalRClient.OnMessageReceived += (username, uploadedFilename) =>
               {
                   if (username != UserNameSentMessage)
                   {
                       var cname = username;
                       var cc = uploadedFilename;

                       if (cc.StartsWith("awsimage"))
                       {
                           int index = cc.IndexOf("awsimage");

                           uploadedFilename = cc.Substring(index + 8);

                           //var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, uploadedFilename);
                           //imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));
                           //var imagestore2 = uploadedFilename + ".jpg";
                           //Stream stream = new MemoryStream(imageData);
                           //DependencyService.Get<IFileService>().SavePicture(imagestore2, stream, getGalleryPath());
                           string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + uploadedFilename;
                           // string url = "http://elixirct.in/ShopRConservicePublish/Uploads" + uploadedFilename;
                        
                           try
                           {
                               DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
                               var images = DependencyService.Get<IFileService>().GetPictureFromDisk(uploadedFilename, getGalleryPath());
                             //  var client = new HttpClient();
                            //   var uri = new Uri(Uri.EscapeUriString(url));
                           //    byte[] urlContents = await client.GetByteArrayAsync(uri);
                          //     var stream1 = new MemoryStream(urlContents);

                               TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = "Received", DateSent = DateTime.Now, CustomerChatName = cname });

                           }
                          
                           catch (Exception ex) { System.Diagnostics.Debug.WriteLine(ex); }
                           Task.Delay(TimeSpan.FromSeconds(1));
                         //  var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                         //  ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, true);

                           member = new Member();
                           memberDatabase = new MemberDatabase();
                           member.Text = uploadedFilename;
                           member.Status = "Received";
                           member.Datetime = DateTime.Now;
                           member.Query_ID = QueryId;
                           member.Type = "IMG";

                           if (member != null)
                           {
                               memberDatabase.AddMember(member);

                           }
                       }

                       //else if (cc.StartsWith("pdf"))
                       //{
                       //    int index = cc.IndexOf("pdf");

                       //    uploadedFilename = cc.Substring(index + 3);

                       //    var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, uploadedFilename);
                       //    imlocal = ImageSource.FromStream(() => new MemoryStream(imageData));

                       //    var imagestore2 = uploadedFilename + ".pdf";
                       //    Stream stream = new MemoryStream(imageData);
                       //    DependencyService.Get<IFileService>().SavePicture(imagestore2, stream, getGalleryPath());
                       //    var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());
                       //    // TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = "Sent", DateSent = DateTime.Now });
                       //    TextContainer.Add(new MessageText { Text = images, imgheight = "10", imgwidth = "10", Status = "Sent", DateSent = DateTime.Now });

                        //   var last21 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        //   ListView.ScrollTo(last21, ScrollToPosition.MakeVisible, true);
                       //}
                     

                       else
                       {
                           TextContainer.Add(new MessageText { Text = cc, imgheight = "10", imgwidth = "10", Status = "Received", DateSent = DateTime.Now, CustomerChatName = cname });
                           var last2 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                           ListView.ScrollTo(last2, ScrollToPosition.MakeVisible, true);

                           member = new Member();
                           memberDatabase = new MemberDatabase();
                           member.Text = cc;
                           member.Status = "Received";
                           member.Datetime = DateTime.Now;
                           member.Query_ID = QueryId;
                           member.Type = "Text";

                           if (member != null)
                           {
                               memberDatabase.AddMember(member);
                               var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                               ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, true);

                           }
                       }

                       var last32 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                       ListView.ScrollTo(last32, ScrollToPosition.MakeVisible, true);

                   }
                   var last31 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                   ListView.ScrollTo(last31, ScrollToPosition.MakeVisible, true);
               };
               

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                DisplayAlert("Alert", "Some Network Problem", "OK");
            }


            Messagebox.Completed += (s, e) =>
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var messageSent = MessageSent;
                    if (messageSent != null)
                        messageSent(Messagebox.Text);
                    Messagebox.Text = string.Empty;
                }
                else
                {
                   // DisplayAlert("Alert", "Please check your internet connection", "OK");
                }

            };

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

       
    public void BindData()
        {

            try
            {
                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetMembers(QueryId);

                foreach (Member var1 in members)
                {
                    if (var1.Type == "IMG")
                    {
                        var text = var1.Text;
                        //int index = text.IndexOf("awsimage");
                        //text = text.Substring(index + 8);

                        var images = DependencyService.Get<IFileService>().GetPictureFromDisk(text, getGalleryPath());
                        TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = var1.Status, DateSent = var1.Datetime });
                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }
                    else if (var1.Text.StartsWith("pdf"))
                    {
                        int index = var1.Text.IndexOf("pdf");
                        var img1 = var1.Text.Substring(index + 3);
                        var imagestore2 = img1 + ".pdf";
                        var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());
                        // TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = var1.Status, DateSent = var1.Datetime });
                        TextContainer.Add(new MessageText { Text = images, imgheight = "10", imgwidth = "10", Status = var1.Status, DateSent = var1.Datetime });

                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }
                    else
                    {
                        TextContainer.Add(new MessageText { Text = var1.Text, imgheight = "10", imgwidth = "10", Status = var1.Status, DateSent = var1.Datetime });
                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            //memberDatabase = new MemberDatabase();
            //// var members = memberDatabase.GetMembers();
            //var sb = manager1.GetTodoItemsAsync().Result;
            //var a = sb.Count();
            ////   var b = members.Count();

            //if (a > 0)
            //{
            //    foreach (TodoItem var2 in sb)
            //    {
            //        //  TextContainer.Add(new MessageText { Text = var2.Message, imgheight = "10", imgwidth = "10", Status = var2.Status });
            //        member = new Member();
            //        memberDatabase = new MemberDatabase();
            //        member.Text = var2.Message;
            //        member.Status = var2.Status;
            //        member.Datetime = DateTime.Now;
            //        member.Query_ID = var2.QueryID;
            //        //  member.imagesqlite = System.Convert.ToByte(imlocal);
            //        memberDatabase.AddMember(member);
            //    }
               
                    
            // }


        }

        public async void BindFromAzure()
        {
            try
            {
                syncIndicator.IsRunning = true;
                memberDatabase = new MemberDatabase();
                // var members = memberDatabase.GetMembers();
                var sb = await manager1.GetTodoItemsAsync();
                //  var a = sb.Count();
                //   var b = members.Count();
                TextContainer.Clear();
                foreach (TodoItem var2 in sb)
                {
                    if (var2.Message.StartsWith("blobimage"))
                    {
                        int index = var2.Message.IndexOf("blobimage");
                        var img1 = var2.Message.Substring(index + 9);
                        var imagestore2 = img1 + ".jpg";
                        var images = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());
                        TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = var2.Status });
                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }

                    else if(var2.Message.StartsWith("pdf"))
                    {
                        int index = var2.Message.IndexOf("pdf");
                        var img1 = var2.Message.Substring(index + 3);
                        var imagestore2 = img1 + ".pdf";
                        var pdffile = DependencyService.Get<IFileService>().GetPictureFromDisk(imagestore2, getGalleryPath());
                        //  TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = var2.Status });
                        TextContainer.Add(new MessageText { Text = pdffile, imgheight = "10", imgwidth = "10", Status = var2.Status});

                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }

                    else
                    {
                        TextContainer.Add(new MessageText { Text = var2.Message, imgheight = "10", imgwidth = "10", Status = var2.Status });
                        var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                        ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                    }
                }

                syncIndicator.IsRunning = false;
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                syncIndicator.IsRunning = false;
            }
        }
        public async void BindFromAWS()
        {
            try
            {
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
                if (status == PermissionStatus.Granted)
                {
                    string Inputs = "QueryId=" + QueryId + "&StoreUID=" + Storeuid;
                    string resp = await RestService.GetChat(Inputs);
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(resp);
                    if (result != "")
                    {
                        try
                        {
                            string[] arrSepr2 = result.Split('|');
                            if (arrSepr2.Length > 0)
                            {
                                //  TextContainer.Clear();
                                foreach (string word in arrSepr2)
                                {
                                    string[] Itemstemp = word.Split('`');

                                    if (Itemstemp[4] == "Text")
                                    {
                                        TextContainer.Add(new MessageText { Text = Itemstemp[2], imgheight = "10", imgwidth = "10", Status = Itemstemp[6], CustomerChatName = Itemstemp[8], StoreNameMessage = UserNameSentMessage, DateSent = DateTime.Parse(Itemstemp[9]) });
                                        //var last3 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                                        //ListView.ScrollTo(last3, ScrollToPosition.MakeVisible, false);
                                        // await Task.Delay(TimeSpan.FromSeconds(.05));
                                    }
                                    else if (Itemstemp[4] == "IMG")
                                    {
                                        //  string images = "https://maudit.elixirct.net/UploadFileToServer/Uploads/" + Itemstemp[2];
                                        var images = DependencyService.Get<IFileService>().GetPictureFromDisk(Itemstemp[2], getGalleryPath());
                                        ExistenceCheckResult imagepresent = await FileSystem.Current.LocalStorage.CheckExistsAsync(images);

                                        if (imagepresent == ExistenceCheckResult.FileExists)
                                        {
                                            TextContainer.Add(new MessageText { imgsource = images, imgheight = "200", imgwidth = "200", Status = Itemstemp[6], DateSent = DateTime.Parse(Itemstemp[9]), CustomerChatName = Itemstemp[8], StoreNameMessage = UserNameSentMessage });

                                            var last4 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                                            ListView.ScrollTo(last4, ScrollToPosition.MakeVisible, true);
                                        }
                                        else
                                        {
                                            //ImageSource.FromUri(new System.Uri(url))                                  
                                            string imagename = Itemstemp[2];
                                            string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + imagename;
                                            try
                                            {
                                                DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
                                                await Task.Delay(TimeSpan.FromSeconds(2));
                                                var images3 = DependencyService.Get<IFileService>().GetPictureFromDisk(imagename, getGalleryPath());
                                                TextContainer.Add(new MessageText { imgsource = images3, imgheight = "200", imgwidth = "200", Status = Itemstemp[6], DateSent = DateTime.Parse(Itemstemp[9]), CustomerChatName = Itemstemp[8], StoreNameMessage = UserNameSentMessage });

                                            }
                                            catch (Exception ex)
                                            {
                                                System.Diagnostics.Debug.WriteLine(ex);
                                            }


                                            //   TextContainer.Add(new MessageText { imgsource = images3, imgheight = "200", imgwidth = "200", Status = Itemstemp[6], DateSent = DateTime.Parse(Itemstemp[9]), CustomerChatName = Itemstemp[8], StoreNameMessage = UserNameSentMessage  });

                                            //  var client = new HttpClient();
                                            //   var uri = new Uri(Uri.EscapeUriString(url));
                                            //   byte[] urlContents = await client.GetByteArrayAsync(uri);
                                            //    var stream1 = new MemoryStream(urlContents);

                                            //   IncidentImageData.Source = ImageSource.FromStream(() => stream1);

                                            //await Task.Run(() =>
                                            // {
                                            //     TextContainer.Add(new MessageText { imgsource = ImageSource.FromStream(()=>new MemoryStream(urlContents) ) , imgheight = "200", imgwidth = "200", Status = Itemstemp[6], DateSent = DateTime.Parse(Itemstemp[9]), CustomerChatName = Itemstemp[8], StoreNameMessage = UserNameSentMessage });

                                            //     // For simplicity setting data on the adapter is omitted here
                                            // });
                                            //try
                                            //{
                                            //    DependencyService.Get<IFileService>().DownloadFile(url, getGalleryPath());
                                            //}
                                            //catch (Exception ex)
                                            //{ System.Diagnostics.Debug.WriteLine(ex); }

                                            // await Task.Delay(100);
                                            //  await Task.Delay(TimeSpan.FromSeconds(5));
                                            var last7 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                                            ListView.ScrollTo(last7, ScrollToPosition.MakeVisible, false);
                                        }

                                        // await Task.Delay(TimeSpan.FromSeconds(1));

                                    }
                                    // TextContainer.Add(new MessageText { Id = Itemstemp[0], Dt = Itemstemp[3], Name = Itemstemp[1] + " " + Itemstemp[2] + " " + Itemstemp[4], CustomerName = Itemstemp[5] });
                                    // await Task.Delay(TimeSpan.FromSeconds(1));
                                }
                                var last6 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                                ListView.ScrollTo(last6, ScrollToPosition.MakeVisible, false);
                            }
                            var last5 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                            ListView.ScrollTo(last5, ScrollToPosition.MakeVisible, false);
                        }
                        catch (Exception ex)
                        {
                            //System.Diagnostics.Debug.WriteLine(ex);
                            var last5 = ListView.ItemsSource.Cast<object>().LastOrDefault();
                            ListView.ScrollTo(last5, ScrollToPosition.MakeVisible, false);
                        }


                    }
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Storage Denied", "Can not continue, try again.", "OK");
                }

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("Position 1");
            MessagingCenter.Unsubscribe<Popup_ImageDialog, string>(this, "awsimagename");
            MessagingCenter.Subscribe<Popup_ImageDialog, string>(this, "awsimagename", (sender, value) =>
            {
                bool ab = true;

                var messageSent = MessageSent;
                if (ab == true && value != null)
                {
                    messageSent(value);
                }
                //  MessagingCenter.Unsubscribe<Popup_ImageDialog, string>(this, "blobimagename");
                System.Diagnostics.Debug.WriteLine("Position 2");
                ab = false;
                value = null;
            });           

        }

        protected override void OnDisappearing()
        {
         //   TextContainer.Clear();
            MessagingCenter.Unsubscribe<Popup_ImageDialog, string>(this, "awsimagename");

        }
    

     
        public async void EnterButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Boolean boolisValid = await IsValid();
                if(boolisValid)
                { 
                var messageSent = MessageSent;
                if (messageSent != null)
                    messageSent(Messagebox.Text); 
                Messagebox.Text = string.Empty;
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await DisplayAlert("Alert", "Please wait, Try to connect to server", "Ok");
            }
        }

        public async Task<Boolean> IsValid()
        {
            if (Messagebox.Text == null || Messagebox.Text.Trim() == "")
            {
                await DisplayAlert("Alert", "Please Type a message..!", "Ok");
                Messagebox.Focus();
                return false;
            }
            return true;
        }
        public async void Image_Clicked(object sender, EventArgs e)
        {
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    CompressionQuality = 30,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
                });
                FileName = file.Path;
                FileName = FileName.Substring(FileName.LastIndexOf("/") + 1);

                var content = new MultipartFormDataContent();

                //IFolder rootFolder = await FileSystem.Current.GetFolderFromPathAsync(FileName);
                //var filee = await rootFolder.GetFileAsync(filename2);

                //Stream stream = await filee.OpenAsync(FileAccess.Read);

                content.Add(new StreamContent(file.GetStream()),
                    "\"file\"",
                    $"\"{file.Path}\"");

                byteData = Model.Convert.ToByteArray(FileName);

                imlocal = ImageSource.FromStream(this.file.GetStream);

                using (var memoryStream = new MemoryStream())
                {
                   // activityIndicator2.IsRunning = true;
                    Sendbtn.IsEnabled = true;
                    file.GetStream().CopyTo(memoryStream);
                    file.Dispose();
                    // return memoryStream.ToArray();
                    Messagebox.Text = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(memoryStream.ToArray()));
                //    activityIndicator2.IsRunning = false;
                    Sendbtn.IsEnabled = true;
                }

                //var messageSent = MessageSent;
                //if (messageSent != null)

                //    messageSent(uploadedFilename);

                //  Messagebox.Text = string.Empty;

                //if (!string.IsNullOrWhiteSpace(uploadedFilename))
                //{
                //    var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, uploadedFilename);

                //    var img = ImageSource.FromStream(() => new MemoryStream(imageData));

                //    TextContainer.Add(new MessageText { imgsource = img });
                //    activityIndicator.IsRunning = false;


                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
               // await DisplayAlert("Alert", "Please check your Internet connection", "Ok");
               // activityIndicator2.IsRunning = false;
                Sendbtn.IsEnabled = true;
            }

        }

        void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
           => ((ListView)sender).SelectedItem = null;

        private async void lstView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // IsBusy = true;

          

                string imageurl2 = "";
                string pdfurl2 = "";

                if (e.SelectedItem == null)
                    return;

                var dtnhd = e.SelectedItem as MessageText;
            try
            {
                imageurl2 = dtnhd.imgsource.ToString();
                string bbb2 = imageurl2.Substring(6);
                var _Popupspec = new ViewImageDialog(bbb2);
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch
            { }
            try
            {
                pdfurl2 = dtnhd.Text.ToString();
                Device.OpenUri(new Uri(pdfurl2));
            }
            catch { }
                 
            
                
           
        }

        public void toolbarclick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomerProfile());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new Popup_RequestRating());

            try
            {
                var _Popupspec = new Popup_RequestRating();
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            try
            {
                var _Popupspec = new Popup_ImageDialog();
                await Navigation.PushPopupAsync(_Popupspec);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void Imageview(object sender, EventArgs e)
        {
           
          
        }

        private void Messagebox_TextChanged(object sender, TextChangedEventArgs e)
        {
           // string str = "parm";
          //  SignalRClient.IsTyping("Parmendra");
           
        }
    }

    public class MessageText
    {
        [JsonProperty("ID")]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        public ImageSource imgsource { get; set; }       
        public string Status { get; set; }
        public DateTime DateSent { get; set; }
        public ImageSource deliverimage { get; set; }
        public string imgheight { get; set; }
        public string imgwidth { get; set; }     
        public string Query_ID { get; set; }
        public string CustomerChatName { get; set; }
        public string StoreNameMessage { get; set; }


    }

   

  

}