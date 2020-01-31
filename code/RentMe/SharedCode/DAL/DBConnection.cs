using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace SharedCode.DAL
{
	/// <summary>
	/// The class responsible for establishing a connection the DB 
	/// </summary>
	public class DbConnection
	{

        /// <summary>
		/// The connection string for the current database
		/// </summary>
        public static readonly string ConnString = "server=160.10.25.16; port=3306; uid=cs4982s20c; " +
                                                   "pwd=zOAIfJ3BhiAeUryr; database=cs4982s20c;";
        /// <summary>
        /// Gets the connection to the DB
        /// </summary>
        /// <returns>the connection </returns>
        public static MySqlConnection GetConnection()
        {
            var conn = new MySqlConnection(ConnString);

            return conn;
        }
    }
}
