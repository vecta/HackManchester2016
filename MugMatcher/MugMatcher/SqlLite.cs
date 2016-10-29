using System;
using System.IO;
using Neurotec.Biometrics.Client;

namespace MugMatcher
{
    public class SqlLite
    {
        public static void Register(NBiometricClient client)
        {
            string databasePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            databasePath = Path.Combine(databasePath, "MugMatcher");
            if (!Directory.Exists(databasePath))
            {
                Directory.CreateDirectory(databasePath);
            }
            databasePath = Path.Combine(databasePath, "MugMatcher.db");

            client.SetDatabaseConnectionToSQLite(databasePath);
        }
    }
}
