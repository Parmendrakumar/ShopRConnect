using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using System.Collections;

namespace RetailerApp
{
    public class MemberDatabase
    {
        private SQLiteConnection conn;

        //CREATE
        public MemberDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Member>();
            conn.CreateTable<UserDetail>();
            conn.CreateTable<OfferDetail>();
            conn.CreateTable<registrationImages>();
            conn.CreateTable<CategorySubCat>();
            conn.CreateTable<StoreImageCarsoul>();
            conn.CreateTable<StateandCity>();
        }

        //READ
        public IEnumerable<Member> GetMembers()
        {
            var members = (from mem in conn.Table<Member>() select mem);
            return members.ToList();
        }
        public IEnumerable<StoreImageCarsoul> GetStoreImage()
        {
            var members = (from mem in conn.Table<StoreImageCarsoul>() select mem);
            return members.ToList();
        }

        public IEnumerable<Member> GetMembers(string id)
        {
            var members = (from mem in conn.Table<Member>() where mem.Query_ID == id select mem);
            return members.ToList();
        }

        public IEnumerable<CategorySubCat> GetCatSubCa(int cat)
        {
            var members = (from mem in conn.Table<CategorySubCat>() where mem.Id == cat select mem);
            return members.ToList();
        }
        public IEnumerable<CategorySubCat> GetCatSubCa(string cat)
        {
            var members = (from mem in conn.Table<CategorySubCat>() where mem.SubCategoryID == cat select mem);
            return members.ToList();
        }
        public IEnumerable<CategorySubCat> GetCatSubCa()
        {
            var members = (from mem in conn.Table<CategorySubCat>() select mem);
            return members.ToList();
        }

        public IEnumerable<CategorySubCat> GetCatSubCategory(string category)
        {
            var members = (from mem in conn.Table<CategorySubCat>() select mem);
            return members.ToList();
        }

        //INSERT
        public string AddMember(Member member)
        {
            conn.Insert(member);
            return "success";
        }

        public string AddStoreImage(StoreImageCarsoul member)
        {
            conn.Insert(member);
            return "success";
        }

        public string AddUserDetail(UserDetail user)
        {
            conn.Insert(user);
            return "success";
        }

        public string AddRegImage(registrationImages img)
        {
            conn.Insert(img);
            return "success";
        }
        public string UpdateRegImage(registrationImages img)
        {
            conn.Update(img);
            return "success";
        }

        public string UpdateCategorySub(CategorySubCat catsubcat)
        {
            conn.Update(catsubcat);
            return "success";
        }
        public string AddCategorySub(CategorySubCat catsubcat)
        {
            conn.Insert(catsubcat);
            return "success";
        }
        public string AddStatename(StateandCity catsubcat)
        {
            conn.Insert(catsubcat);
            return "success";
        }
        public IEnumerable<registrationImages> GetRegImage(string id)
        {
            var members = (from mem in conn.Table<registrationImages>() where mem.ImageType == id select mem);
            return members.ToList();
        }
        public IEnumerable<UserDetail> GetUserDetail()
        {
            var user = (from mem in conn.Table<UserDetail>() select mem);
            return user.ToList();
        }

        public string AddOfferDetail(OfferDetail adofr)
        {
            conn.Insert(adofr);
            return "success";
        }
        public IEnumerable<OfferDetail> GetOfferDetail()
        {
            var getofr = (from mem in conn.Table<OfferDetail>() select mem);
            return getofr.ToList();
        }

        public IEnumerable<CategorySubCat> GetCatSubCat()
        {
            var getcat = (from mem in conn.Table<CategorySubCat>() select mem);
            return getcat.ToList();
        }
        public IEnumerable<StateandCity> GetStateCity()
        {
            var getcat = (from mem in conn.Table<StateandCity>() select mem);
            return getcat.ToList();
        }

        //DELETE
        public string DeleteMember(int id)
        {
            conn.Delete<Member>(id);
            return "success";
        }
        public string DeleteUserDetail()
        {
            conn.DeleteAll<UserDetail>();
            return "success";
        }
        public string DeleteCatSubCat()
        {
            conn.DeleteAll<CategorySubCat>();
            return "success";
        }
    }
}