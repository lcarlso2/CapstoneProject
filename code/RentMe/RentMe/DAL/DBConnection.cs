using MySql.Data.MySqlClient;


namespace RentMe.DAL
{
	public class DBConnection
	{
		public static readonly string ConnString = "server=160.10.25.16; port=3306; uid=cs4982s20c; " +
												   "pwd=zOAIfJ3BhiAeUryr; database=cs4982s20c;";

		public static MySqlConnection GetConnection()
		{
			var conn = new MySqlConnection(ConnString);

			return conn;
		}
	}
}
