using MySql.Data.MySqlClient;
using RentMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentMe.DAL
{
	/// <summary>
	/// The class responsible communicating with the DB for the media 
	/// </summary>
	public class MediaDal
	{
        /// <summary>
        /// Retrieves all media.
        /// </summary>
        /// <returns>All media </returns>
        public static List<MediaModel> RetrieveAllMedia()
        {
            var mediaItems = new List<MediaModel>();

            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Media";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                        
                            var categoryOrdinal = reader.GetOrdinal("Category");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var IDOrdinal = reader.GetOrdinal("Id");
                            


                            while (reader.Read())
                            {
                                var id = reader.GetInt32((IDOrdinal));

                                var category = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);
                               


                                var media = new MediaModel { Id = id, Category = category, Title = title };

                                mediaItems.Add(media);
                            }
                        }
                    }
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mediaItems;
        }

    }
}
