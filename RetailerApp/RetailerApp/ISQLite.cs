using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailerApp
{
    public interface ISQLite
    {
        SQLite.SQLiteConnection GetConnection();

    }
}
