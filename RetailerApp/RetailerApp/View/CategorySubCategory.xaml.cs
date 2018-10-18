using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorySubCategory : ContentPage
    {
        public MemberDatabase memberDatabase;
        public CategorySubCat catsubcat;
        public string category = "";
        public string categoryId = "";
        public string subcategory = "";
        public string subcategoryId = "";
        List<NewDataSet> ObjPizzaList { get; set; }
        public ObservableCollection<CategorySubC> CategoryItems { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubAccessories { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubJewellery { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubToys { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubMobile { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubComputer { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubElectoronic { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubHomeAcc { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubHomeLiving { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubBags { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubHealth { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubSports { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubOffice { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubBooks { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubAutomobile { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubWemens { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubMens { get; set; } = new ObservableCollection<CategorySubC>();
        public ObservableCollection<CategorySubC> SubFootwear { get; set; } = new ObservableCollection<CategorySubC>();



        public CategorySubCategory()
        {
            InitializeComponent();

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            BindingContext = this;

            GetJSON();
            
        }


        private void lstView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            try
            {
                
            //   string itm = e.Group.ToString();
                var dataItem = e.Item as CategorySubC;
               
                //  dataItem.TextColor = Color.Red;
                foreach (CategorySubC item in CategoryItems)
                {
                    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Gray;
                    item.OnPropertyChanged();
                }

            
                if (dataItem.CategoryId == "1")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = true;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;

                }
                else if (dataItem.CategoryId == "2")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = true;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;

                }
                else if (dataItem.CategoryId == "3")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = true;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "4")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = true;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "5")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = true;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "6")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = true;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "7")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = true;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "8")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = true;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "9")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = true;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "10")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = true;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "11")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = true;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "12")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = true;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "13")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = true;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "14")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = true;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "15")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = true;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "16")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = true;
                    lstView18.IsVisible = false;
                }
                else if (dataItem.CategoryId == "17")
                {
                    category = "";
                    subcategory = "";
                    subcategoryId = "";
                    lstView2.IsVisible = false;
                    lstView3.IsVisible = false;
                    lstView4.IsVisible = false;
                    lstView5.IsVisible = false;
                    lstView6.IsVisible = false;
                    lstView7.IsVisible = false;
                    lstView8.IsVisible = false;
                    lstView9.IsVisible = false;
                    lstView10.IsVisible = false;
                    lstView11.IsVisible = false;
                    lstView12.IsVisible = false;
                    lstView13.IsVisible = false;
                    lstView14.IsVisible = false;
                    lstView15.IsVisible = false;
                    lstView16.IsVisible = false;
                    lstView17.IsVisible = false;
                    lstView18.IsVisible = true;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
        }

        private void lstView2_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Accessories";
            categoryId = "1";
            var dataItem = e.Item as CategorySubC;

            subcategory = subcategory + "`" + dataItem.SubAccessories;
            subcategoryId = subcategoryId + "`" + dataItem.SubAccessoriesId;

            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();

            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 1;
            catsubcat.CategoryName = category;
       //     catsubcat.CategoryId = categoryId;
       //     catsubcat.SubCategory = subcategory;
        //    catsubcat.SubCategoryId = subcategoryId;
         
            var members = memberDatabase.GetCatSubCa(1);
       
            if(members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }          

        }

        private void lstView3_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Jewellery";
          
            var dataItem = e.Item as CategorySubC;
            //  Sub_Jewellery = Sub_Jewellery + "`" + dataItem.SubJewellery;
            //foreach (Item item in SizeItems)
            //{
            //    item.img = dataItem.Equals(item) ? ImageSource.FromFile("TickRed.png") : ImageSource.FromFile("unselected.png");
            //    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Black;
            //    item.OnPropertyChanged();
            //}
            subcategory = subcategory + "`" + dataItem.SubJewellery;
            subcategoryId = subcategoryId + "`" + dataItem.SubJewelleryId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();

            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 2;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;

            var members = memberDatabase.GetCatSubCa(2);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView4_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Toys, Kids & Babies";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubToys;
            subcategoryId = subcategoryId + "`" + dataItem.SubToysId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 3;
            catsubcat.CategoryName = category;
        //    catsubcat.SubCategory = subcategory;
       //     catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(3);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView5_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Mobiles & Tablets";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubMobile;
            subcategoryId = subcategoryId + "`" + dataItem.SubMobileId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 4;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
            //catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(4);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView6_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Computers & Gaming";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubComputer;
            subcategoryId = subcategoryId + "`" + dataItem.SubComputerId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 5;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(5);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }
        private void lstView7_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Electronics";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubElectoronic;
            subcategoryId = subcategoryId + "`" + dataItem.SubElectoronicId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 6;
            catsubcat.CategoryName = category;
           // catsubcat.SubCategory = subcategory;
            //catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(6);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView8_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Home Appliances";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubHomeAcc;
            subcategoryId = subcategoryId + "`" + dataItem.SubHomeAccId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 7;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(7);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView9_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Home & Living";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubHomeLiving;
            subcategoryId = subcategoryId + "`" + dataItem.SubHomeLivingId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 8;
            catsubcat.CategoryName = category;
         //   catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(8);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }
        private void lstView10_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Bags & Luggage";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubBags;
            subcategoryId = subcategoryId + "`" + dataItem.SubBagsId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 9;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(9);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }
        private void lstView11_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Health & Beauty";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubHealth;
            subcategoryId = subcategoryId + "`" + dataItem.SubHealthId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 10;
            catsubcat.CategoryName = category;
         //   catsubcat.SubCategory = subcategory;
          //  catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(10);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView12_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Sports, Fitness & Outdoor";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubSports;
            subcategoryId = subcategoryId + "`" + dataItem.SubSportsId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 11;
            catsubcat.CategoryName = category;
         //   catsubcat.SubCategory = subcategory;
          //  catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(11);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView16_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Women's Clothing";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubWomensclothing;
            subcategoryId = subcategoryId + "`" + dataItem.SubWomensclothingId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 15;
            catsubcat.CategoryName = category;
         //   catsubcat.SubCategory = subcategory;
          //  catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(15);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }
        private void lstView17_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Men's Clothing";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubMensclothing;
            subcategoryId = subcategoryId + "`" + dataItem.SubMensclothingId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 16;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
           // catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(16);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void Senddata(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        public async void GetJSON()
        {
            try
            {
                ActCat.IsVisible = true;
                ActCat.IsEnabled = true;
                //  var client = new System.Net.Http.HttpClient();
                // var response = await client.GetStringAsync("http://elixirct.in/ShopRConservicePublish/api/Login/GetCatSubCatDataSet");

                Uri geturi = new Uri("http://elixirct.in/ShopRConservicePublish/api/Login/GetCatSubCatDataSet"); //replace your xml url   
                HttpClient client = new HttpClient();
                HttpResponseMessage responseGet = await client.GetAsync(geturi);
                string response = await responseGet.Content.ReadAsStringAsync();//Getting response   
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(response);

                ObjPizzaList = new List<NewDataSet>();
                XDocument doc = XDocument.Parse(result);
                foreach (var item in doc.Descendants("Table"))
                {
                    NewDataSet ObjPizzaItem = new NewDataSet();
                    ObjPizzaItem.CategoryID = item.Element("CategoryID").Value.ToString();
                    ObjPizzaItem.CategoryName = item.Element("CategoryName").Value.ToString();
                    ObjPizzaItem.SubCategoryID = item.Element("SubCategoryID").Value.ToString();
                    ObjPizzaItem.SubCategoryName = item.Element("SubCategoryName").Value.ToString();
                    System.Diagnostics.Debug.WriteLine(item);
                    ObjPizzaList.Add(ObjPizzaItem);
                }

                //foreach (var category in ObjPizzaList.Select(x => x.CategoryID).Distinct())
                //{
                //    CategoryItems.Add(new CategorySubC { CategoryId = category });
                //}

                //  List<NewDataSet> distinct = ObjPizzaList.Distinct().ToList();

                List<NewDataSet> list = ObjPizzaList
                    .GroupBy(a => a.CategoryName)
                    .Select(g => g.First())
                    .ToList();



                foreach (NewDataSet p in list)
                {
                    CategoryItems.Add(new CategorySubC { CategoryId = p.CategoryID, CategoryName = p.CategoryName });
                }           


                foreach (var subcat in ObjPizzaList)
                {
                    if(subcat.CategoryID == "1")
                    {
                        SubAccessories.Add(new CategorySubC { SubAccessories = subcat.SubCategoryName, SubAccessoriesId = subcat.SubCategoryID });
                    }
                   else if (subcat.CategoryID == "2")
                    {
                        SubJewellery.Add(new CategorySubC { SubJewellery = subcat.SubCategoryName, SubJewelleryId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "3")
                    {
                        SubToys.Add(new CategorySubC { SubToys = subcat.SubCategoryName, SubToysId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "4")
                    {
                        SubMobile.Add(new CategorySubC { SubMobile = subcat.SubCategoryName, SubMobileId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "5")
                    {
                        SubComputer.Add(new CategorySubC { SubComputer = subcat.SubCategoryName, SubComputerId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "6")
                    {
                        SubElectoronic.Add(new CategorySubC { SubElectoronic = subcat.SubCategoryName, SubElectoronicId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "7")
                    {
                        SubHomeAcc.Add(new CategorySubC { SubHomeAcc = subcat.SubCategoryName, SubHomeAccId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "8")
                    {
                        SubHomeLiving.Add(new CategorySubC { SubHomeLiving = subcat.SubCategoryName, SubHomeLivingId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "9")
                    {
                        SubBags.Add(new CategorySubC { SubBags = subcat.SubCategoryName, SubBagsId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "10")
                    {
                        SubHealth.Add(new CategorySubC { SubHealth = subcat.SubCategoryName, SubHealthId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "11")
                    {
                        SubSports.Add(new CategorySubC { SubSports = subcat.SubCategoryName, SubSportsId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "12")
                    {
                        SubOffice.Add(new CategorySubC { SubOffice = subcat.SubCategoryName, SubOfficeId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "13")
                    {
                        SubBooks.Add(new CategorySubC { SubBooks = subcat.SubCategoryName, SubBooksId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "14")
                    {
                        SubAutomobile.Add(new CategorySubC { SubAutomobile = subcat.SubCategoryName, SubAutomobileId = subcat.SubCategoryID });
                    }                     

                    else if (subcat.CategoryID == "15")
                    {
                        SubWemens.Add(new CategorySubC { SubWomensclothing = subcat.SubCategoryName, SubWomensclothingId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "16")
                    {
                        SubMens.Add(new CategorySubC { SubMensclothing = subcat.SubCategoryName, SubMensclothingId = subcat.SubCategoryID });
                    }
                    else if (subcat.CategoryID == "17")
                    {
                        SubFootwear.Add(new CategorySubC { SubFootwear = subcat.SubCategoryName, SubFootwearId = subcat.SubCategoryID });
                    }
                  
                }


            }
            catch (InvalidCastException e)
            {
                throw e;
                ActCat.IsVisible = false;
                ActCat.IsEnabled = false;
            }
            ActCat.IsVisible = false;
            ActCat.IsEnabled = false;
        }

        private void lstView13_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Office & Stationery";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubOffice;
            subcategoryId = subcategoryId + "`" + dataItem.SubOfficeId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 12;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
          //  catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(12);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }
        }

        private void lstView14_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Books CD's & DVD";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubBooks;
            subcategoryId = subcategoryId + "`" + dataItem.SubBooksId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 13;
            catsubcat.CategoryName = category;
          //  catsubcat.SubCategory = subcategory;
          //  catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(13);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }
        }

        private void lstView15_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Automobiles";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubAutomobile;
            subcategoryId = subcategoryId + "`" + dataItem.SubAutomobileId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 14;
            catsubcat.CategoryName = category;
         //   catsubcat.SubCategory = subcategory;
         //   catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(14);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }
        }

        private void lstView18_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            category = "Footwear";
            var dataItem = e.Item as CategorySubC;
            // Sub_Toys = Sub_Toys + "`" + dataItem.SubToys;
            //  dataItem.TextColor = Color.Red;
            subcategory = subcategory + "`" + dataItem.SubFootwear;
            subcategoryId = subcategoryId + "`" + dataItem.SubFootwearId;
            dataItem.TextColor = Color.Red;
            dataItem.img = ImageSource.FromFile("TickRed.png");
            dataItem.OnPropertyChanged();


            catsubcat = new CategorySubCat();
            memberDatabase = new MemberDatabase();
            catsubcat.Id = 17;
            catsubcat.CategoryName = category;
        //    catsubcat.SubCategory = subcategory;
        //    catsubcat.SubCategoryId = subcategoryId;
            var members = memberDatabase.GetCatSubCa(17);
            if (members.Any())
            {
                memberDatabase.UpdateCategorySub(catsubcat);
            }
            else
            {
                memberDatabase.AddCategorySub(catsubcat);
            }

        }

        private void lstView19_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void lstView20_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void lstView21_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void lstView22_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void lstView23_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }

    public class CategorySubC : INotifyPropertyChanged
    {
        public string CategoryName { get; set; }    
        public string CategoryId { get; set; }   
        public string SubAccessories { get; set; }
        public string SubAccessoriesId { get; set; }
        public string SubJewellery { get; set; }
        public string SubJewelleryId { get; set; }
        public string SubToys { get; set; }
        public string SubToysId { get; set; }
        public string SubMobile { get; set; }
        public string SubMobileId { get; set; }
        public string SubComputer { get; set; }
        public string SubComputerId { get; set; }
        public string SubElectoronic { get; set; }
        public string SubElectoronicId { get; set; }
        public string SubHomeAcc { get; set; }
        public string SubHomeAccId { get; set; }
        public string SubHomeLiving { get; set; }
        public string SubHomeLivingId { get; set; }
        public string SubBags { get; set; }
        public string SubBagsId { get; set; }
        public string SubHealth { get; set; }
        public string SubHealthId { get; set; }
        public string SubSports { get; set; }
        public string SubSportsId { get; set; }
        public string SubOffice { get; set; }
        public string SubOfficeId { get; set; }
        public string SubBooks { get; set; }
        public string SubBooksId { get; set; }
        public string SubAutomobile { get; set; }
        public string SubAutomobileId { get; set; }
        public string SubWomensclothing { get; set; }
        public string SubWomensclothingId { get; set; }
        public string SubMensclothing { get; set; }
        public string SubMensclothingId { get; set; }
        public string SubFootwear { get; set; }
        public string SubFootwearId { get; set; }
        public Color TextColor { get; set; }
        public ImageSource img { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

    }


   public class NewDataSet
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }

    }

}