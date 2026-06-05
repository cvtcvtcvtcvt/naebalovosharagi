using System.Data.SqlClient;
namespace demo
{
    static class DB
    {
        // строка подключения к БД (Server= имя твоего сервера из SSMS)
        static string con = "Server=DESKTOP-41RFB3E;Database=ShoeShop;Trusted_Connection=True;";
        public static SqlConnection Get() => new SqlConnection(con);
    }
}