using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace RetailerApp
{
    public class Member
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public string Status { get; set; }
        public DateTime Datetime { get; set; }
        public byte imagesqlite { get; set; }
        public string Query_ID { get; set; }
        public string Type { get; set; }

        public Member()

        {
        }
    }
    public class UserDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Userid { get; set; }
        public string Name { get; set; }
        public string StoreId { get; set; }
        public string Email { get; set; }
        public string Imagename { get; set; }
        public string StoreUId { get; set; }
        public string StoreImage1 { get; set; }
        public string StoreImage2 { get; set; }
        public string StoreImage3 { get; set; }

    }
    public class OfferDetail
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Userid { get; set; }
         public string Imagename { get; set; }
        public string ofer { get; set; }
        public string StoreId { get; set; }
    }

    public class StoreImageCarsoul
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }      
        public string Imagename1 { get; set; }
        public string Imagename2 { get; set; }
        public string Imagename3 { get; set; }


    }

    public class registrationImages
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Imagename { get; set; }
        public string ImagePath { get; set; }
        public string ImageType { get; set; }
        public bool UploadingDone { get; set; }

    }
    public class CategorySubCat
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }     
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string img { get; set; }
        public string SubTextColor { get; set; }

    }
    public class StateandCity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
      

    }

}