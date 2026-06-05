using System.Data.SqlClient;

namespace demowpf
{
    static class DB
    {
        // ПОМЕНЯЙ Server= на своё имя сервера из SSMS
        static string con = "Server=DESKTOP-41RFB3E;Database=ShoeShop;Trusted_Connection=True;";
        public static SqlConnection Get() => new SqlConnection(con);
    }
}