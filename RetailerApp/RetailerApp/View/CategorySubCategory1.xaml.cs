using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RetailerApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategorySubCategory1 : ContentPage
    {
        public MemberDatabase memberDatabase1;
        public CategorySubCat catsubcat1;
        public string category = "";
        public string subcategory = "";
        List<NewDataSet> ObjPizzaList { get; set; }
        public ObservableCollection<CategorySubC1> CategoryItems { get; set; } = new ObservableCollection<CategorySubC1>();
        public ObservableCollection<CategorySubC1> SubCategory { get; set; } = new ObservableCollection<CategorySubC1>();

        public CategorySubCategory1()
        {
            InitializeComponent();

            CustomNavigationPage.SetGradientColors(this, new Tuple<Color, Color>(Color.FromHex("#17a39d"), Color.FromHex("#17a39d")));

            BindingContext = this;

            BindCategoryfromSqlite();


        }


        private void lstView_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            try
            {
                SubCategory.Clear();

                //   string itm = e.SelectedItem.ToString();
                var dataItem = e.Item as CategorySubC1;

                //  dataItem.TextColor = Color.Red;

                foreach (CategorySubC1 item in CategoryItems)
                {
                    item.TextColor = dataItem.Equals(item) ? Color.Red : Color.Gray;
                    item.OnPropertyChanged();
                }


                catsubcat1 = new CategorySubCat();
                memberDatabase1 = new MemberDatabase();
                var catesubcat = memberDatabase1.GetCatSubCat();

                foreach (var category in catesubcat.Where(x => x.CategoryID == dataItem.CategoryID))
                {
                    SubCategory.Add(new CategorySubC1 { SubCategoryName = category.SubCategoryName, SubCategoryID = category.SubCategoryID, SubTextColor = Color.FromHex(category.SubTextColor), img= ImageSource.FromFile(category.img) });
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);

            }
        }

        private void lstView2_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var dataItem = e.Item as CategorySubC1;
            var CategoryID = dataItem.CategoryID;
            var SubCategory = dataItem.SubCategoryName;
            var SubCategoryID = dataItem.SubCategoryID;

            var clr = dataItem.SubTextColor.ToString();

           
           if(clr == "[Color: A=1, R=0, G=0, B=0, Hue=0, Saturation=0, Luminosity=0]")
            {
                subcategory = subcategory + "`" + dataItem.SubCategoryName;
                dataItem.SubTextColor = Color.Red;
                dataItem.img = ImageSource.FromFile("TickRed.png");
                dataItem.OnPropertyChanged();

                catsubcat1 = new CategorySubCat();
                memberDatabase1 = new MemberDatabase();
                catsubcat1 = new CategorySubCat();
                var members = memberDatabase1.GetCatSubCa(SubCategoryID);

                foreach (var a in members)
                {
                    catsubcat1.Id = a.Id;
                    catsubcat1.CategoryName = a.CategoryName;
                    catsubcat1.CategoryID = a.CategoryID;
                    catsubcat1.SubCategoryName = a.SubCategoryName;
                    catsubcat1.SubCategoryID = a.SubCategoryID;
                    catsubcat1.img = "TickRed.png";
                    catsubcat1.SubTextColor = "#FF0000";
                }

                if (members.Any())
                {
                    memberDatabase1.UpdateCategorySub(catsubcat1);
                }
                else
                {
                    memberDatabase1.AddCategorySub(catsubcat1);
                }

            }
            else if(clr == "[Color: A=1, R=1, G=0, B=0, Hue=1, Saturation=1, Luminosity=0.5]")
            {
                subcategory = subcategory + "`" + dataItem.SubCategoryName;
                dataItem.SubTextColor = Color.Black;
                dataItem.img = ImageSource.FromFile("");
                dataItem.OnPropertyChanged();

                catsubcat1 = new CategorySubCat();
                memberDatabase1 = new MemberDatabase();
                catsubcat1 = new CategorySubCat();
                var members = memberDatabase1.GetCatSubCa(SubCategoryID);

                foreach (var a in members)
                {
                    catsubcat1.Id = a.Id;
                    catsubcat1.CategoryName = a.CategoryName;
                    catsubcat1.CategoryID = a.CategoryID;
                    catsubcat1.SubCategoryName = a.SubCategoryName;
                    catsubcat1.SubCategoryID = a.SubCategoryID;
                    catsubcat1.img = "";
                    catsubcat1.SubTextColor = "#000000";
                }

                if (members.Any())
                {
                    memberDatabase1.UpdateCategorySub(catsubcat1);
                }
                else
                {
                    memberDatabase1.AddCategorySub(catsubcat1);
                }

            }





        }

        public void BindCategoryfromSqlite()
        {
            catsubcat1 = new CategorySubCat();
            memberDatabase1 = new MemberDatabase();

            var catesubcat = memberDatabase1.GetCatSubCat();

            List<CategorySubCat> list = catesubcat
                  .GroupBy(a => a.CategoryName)
                    .Select(g => g.First())
                    .ToList();

            foreach (CategorySubCat var1 in list)
            {
                CategoryItems.Add(new CategorySubC1 { CategoryID = var1.CategoryID, CategoryName = var1.CategoryName });
            }


        }

        private void Senddata(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }


    }

    public class CategorySubC1 : INotifyPropertyChanged
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public Color TextColor { get; set; }
        public ImageSource img { get; set; }
        public Color SubTextColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
        }

    }




}