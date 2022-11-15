using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Text;

namespace Argo.Shop.IntegrationTests.TestHelpers
{
    public static class SqlServerHelper
    {
        public static string ScriptTableData(
            string dbConnectionString,
            string databaseName)
        {
            var options = new ScriptingOptions
            {
                IncludeIfNotExists = false,
                ScriptSchema = false,
                ScriptData = true
            };

            return ScriptDatabase(
                dbConnectionString,
                databaseName,
                options);
        }

        public static void ExecuteScript(
            string dbConnectionString,
            string databaseName,
            string script)
        {
            var conn = new SqlConnection(dbConnectionString);
            var serverConn = new ServerConnection(conn);
            var server = new Server(serverConn);
            server.Databases[databaseName].ExecuteNonQuery(script);
        }

        // based on https://stackoverflow.com/questions/37003017/scriptingoptions-sql-smo-does-not-support-scripting-data
        public static string ScriptDatabase(
            string dbConnectionString,
            string databaseName,
            ScriptingOptions options)
        {
            var conn = new SqlConnection(dbConnectionString);
            var serverConn = new ServerConnection(conn);
            var server = new Server(serverConn);
            var database = server.Databases[databaseName];

            var scripter = new Scripter(server) { Options = options };

            var sb = new StringBuilder();
            foreach (Table myTable in database.Tables)
            {
                foreach (var s in scripter.EnumScript(new[] { myTable.Urn }))
                    sb.Append(s).AppendLine();
            }

            return sb.ToString();
        }
    }
}
