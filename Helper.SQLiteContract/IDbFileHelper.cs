using SQLite.Net;

namespace Helper.SQLiteContract
{
    public interface IDbFileHelper
    {
        SQLiteConnection GetConnection();
    }
}