using System.Data;
using System.Data.SqlClient;
namespace TourismDXP.Services.Classes
{
    public class SecondaryPortalManager
    {
        #region Users Query
        public static string getUserList = "select * from Client where IsDeleted=0 and role='client'";

        #endregion
        public static string _connection { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public SecondaryPortalManager(string connection)
        {
            _connection = connection;
            Initialize();
        }
        internal static IDbConnection db;
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            db = new SqlConnection(_connection);

        }
    }
}
