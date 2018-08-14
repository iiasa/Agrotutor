namespace Helper.Realm
{
    using System.Diagnostics;
    using Realms;

    public class DbContext
    {
        private static ulong realmVersion = 2;
        private static Realm Realm { get; set; }

        public static Realm GetConnection()
        {
            var con = new RealmConfiguration("cimmyt.realm")
            {
                SchemaVersion = DbContext.realmVersion,
                MigrationCallback = (migration, oldSchemaVersion) =>
                {
                    if (DbContext.realmVersion == 1 && oldSchemaVersion == 0)
                    {
                        // no action needed
                    }
                }
            };
            Debug.WriteLine(con.DatabasePath);
            Realm = Realm.GetInstance(con);
            return Realm;
        }
    }
}