using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMeDesktop.DAL
{
    /// <summary>
    /// The db connection class
    /// </summary>
    public class DbConnection
    {
        /// <summary>
        /// The connection string
        /// </summary>
        public static readonly string ConnString = "server=160.10.25.16; port=3306; uid=cs4982s20c; " +
                                                   "pwd=zOAIfJ3BhiAeUryr; database=cs4982s20c;";


        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns>The connection</returns>
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(ConnString);
            return conn;
        }

    }
}
