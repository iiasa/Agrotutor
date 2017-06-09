namespace Helper.SQLiteContract
{
    using SQLite.Net;

    public interface IDbFileHelper
    {
        SQLiteConnection GetConnection();
    }
}