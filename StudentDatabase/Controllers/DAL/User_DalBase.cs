using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using StudentDatabase.Models;
using System.Data;
using System.Data.Common;

namespace StudentDatabase.Controllers.DAL
{
    public class User_DalBase
    {
        public static (List<string>, List<dynamic>?) userData(User? user = null) => (new List<string>() { "Username", "UserPassword", "Email" }, user != null ? new List<dynamic>() { user.Username, user.UserPassword, user.Email } : null);
        private static string connStr;
        public User_DalBase(string connStrs) => connStr = connStrs;
        private (SqlDatabase?, DbCommand?) _command(string procType = "SelectAll")
        {
            SqlDatabase? sqlDB = null;
            DbCommand? dbComm = null;
            try
            {
                sqlDB = new SqlDatabase(connectionString: connStr);
                dbComm = sqlDB.GetStoredProcCommand($"[dbo].[PR_Users_{procType}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing the database: {ex.ToString()}");
            }
            return (sqlDB, dbComm);
        }
        public User? SelectUser(User user)
        {
            var (sqlDB, dbComm) = _command();
            sqlDB.AddInParameter(dbComm, "Username", SqlDbType.VarChar, user.Username);
            sqlDB.AddInParameter(dbComm, "UserPassword", SqlDbType.VarChar, user.UserPassword);
            using (IDataReader reader = sqlDB.ExecuteReader(dbComm))
            {
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count > 0)
                {
                    return new User(dataTable.Rows[0]);
                }
            }
            return null;
        }
        public int InsertUser(User user)
        {
            var (dataNames, value) = User_DalBase.userData(user);
            var (sqlDB, dbComm) = _command("Insert");
            for (int i = 0; i < dataNames.Count; i++) sqlDB.AddInParameter(dbComm, dataNames[i], SqlDbType.VarChar, value[i]);
            return sqlDB.ExecuteNonQuery(dbComm);
        }
    }
}
