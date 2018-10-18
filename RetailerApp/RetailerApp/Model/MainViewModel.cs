using FFImageLoading.Forms;
using RetailerApp.Data;
using RetailerApp.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RetailerApp.Model
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MemberDatabase memberDatabase;    
        public StoreImageCarsoul storeImage;
        public UserDetail usrdetail;
        string imageurl1 = "";
        string imageurl2 = "";
        string imageurl3 = "";
        string storeUId = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            try
            {
                memberDatabase = new MemberDatabase();
                var members = memberDatabase.GetUserDetail();

                UserDetail userin = (from blog in members
                                            select blog).FirstOrDefault();             

                imageurl1 = "http://elixirct.in/ShopRConservicePublish/Uploads/" + userin.StoreImage1;
                imageurl2 = "http://elixirct.in/ShopRConservicePublish/Uploads/" + userin.StoreImage2;
                imageurl3 = "http://elixirct.in/ShopRConservicePublish/Uploads/" + userin.StoreImage3;

                MyItemsSource = new ObservableCollection<Xamarin.Forms.View>()
            {
                new CachedImage() { Source = imageurl1, DownsampleToViewSize = true, Aspect = Xamarin.Forms.Aspect.AspectFill },
                new CachedImage() { Source = imageurl2, DownsampleToViewSize = true, Aspect = Xamarin.Forms.Aspect.AspectFill },
                new CachedImage() { Source = imageurl3, DownsampleToViewSize = true, Aspect = Xamarin.Forms.Aspect.AspectFill }
            };

                MyCommand = new Xamarin.Forms.Command(() =>
                {
                    Debug.WriteLine("Position selected.");
                });

             //   Bindofr();


            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
  
        }

        public async void Bindofr()
        {
            memberDatabase = new MemberDatabase();
            var members = memberDatabase.GetUserDetail();

            UserDetail userin = (from blog in members
                                 select blog).FirstOrDefault();
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
                            string url = "http://elixirct.in/ShopRConservicePublish/Uploads/" + Itemstemp[2];

                            offer1 = new ObservableCollection<OffersDetails>()
                            {
                                 new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0] }
                            };
                          //  offer1.Add(new OffersDetails { img = ImageSource.FromUri(new System.Uri(url)), Name = Itemstemp[0] });                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }


        ObservableCollection<Xamarin.Forms.View> _myItemsSource;
        ObservableCollection<OffersDetails> _offer1;
        public ObservableCollection<Xamarin.Forms.View> MyItemsSource
        {
            set
            {
                _myItemsSource = value;
               // OnPropertyChanged("MyItemsSource");
            }
            get
            {
                return _myItemsSource;
            }
        }

        public ObservableCollection<OffersDetails> offer1
        {
            set
            {
                _offer1 = value;
                // OnPropertyChanged("MyItemsSource");
            }
            get
            {
                return _offer1;
            }
        }
        public Xamarin.Forms.Command MyCommand { protected set; get; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }

    public class OffersDetails
    {
        public string Id { get; set; }
        public ImageSource img { get; set; }
        public string Name { get; set; }
        public string Dt { get; set; }


    }
}
