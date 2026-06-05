using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demowpf
{
    static class DB
    {
        // ПОМЕНЯЙ Server= на своё имя сервера из SSMS
        static string con = "Server=WIN-8VH051PFD7P\\SQLEXPRESS;Database=ShoeShop;Trusted_Connection=True;";
        public static SqlConnection Get() => new SqlConnection(con);
    }
}
