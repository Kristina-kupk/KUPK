using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;


namespace Ателье
{
    class DB
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings
                ["Ателье.Properties.Settings.AteleConnectionString1"].ConnectionString);
        public void ConnectOpen()
        {
            connect.Open();
        }
        public SqlConnection GetConnect()
        {
            return connect;
        }
    }
}
