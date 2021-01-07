using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourismDXP.Admin.Controllers
{
    public class BaseAdoController : Controller
    {
        public BaseAdoController()
        {
            Initialize();
        }
        internal static IDbConnection db;
        public static void Initialize()
        {            //Data Source=.;Initial Catalog=Tenat1;Integrated Security=true;" providerName="System.Data.SqlClient
            var connection = "EthnicityContextManger.GetConnectionString()";
            db = new SqlConnection(connection);

        }
    }
}