
namespace Helper.Realm
{
    using Realms;

    public class DbContext
    {
        private static Realm Realm { get; set; }

        public static Realm GetConnection()
        {
            var con = new RealmConfiguration("cimmyt.realm");
            System.Diagnostics.Debug.WriteLine(con.DatabasePath);
            Realm = Realm.GetInstance(con);
            return Realm;
        }
    }
}
