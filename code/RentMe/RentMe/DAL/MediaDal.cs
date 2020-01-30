using MySql.Data.MySqlClient;
using RentMe.Models;
using RentMeSharedCode.DAL;
using System;
using System.Collections.Generic;


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
                            var typeOrdinal = reader.GetOrdinal("Type");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var IDOrdinal = reader.GetOrdinal("Id");
                            var QtyOrdinal = reader.GetOrdinal("Qty");
                            


                            while (reader.Read())
                            {
                                var id = reader.GetInt32((IDOrdinal));

                                var category = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var type = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);

                                var qty = reader.GetInt32((QtyOrdinal));


                                if (qty > 0)
                                {
                                    var media = new MediaModel { Id = id, Category = category, Title = title, Type = type };

                                    mediaItems.Add(media);
                                }
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

        /// <summary>
        /// Retrieves all media in a specific category.
        /// </summary>
        /// <returns>All media in a category</returns>
        public static List<MediaModel> RetrieveMediaByCategory(string category)
        {
            var mediaItems = new List<MediaModel>();

            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Media where Category = @category";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@category", MySqlDbType.VarChar);
                        cmd.Parameters["@category"].Value = category;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var categoryOrdinal = reader.GetOrdinal("Category");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var typeOrdinal = reader.GetOrdinal("Type");
                            var IDOrdinal = reader.GetOrdinal("Id");
                            var QtyOrdinal = reader.GetOrdinal("Qty");



                            while (reader.Read())
                            {
                                var id = reader.GetInt32((IDOrdinal));

                                var categoryValue = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var type = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);

                                var qty = reader.GetInt32((QtyOrdinal));


                                if (qty > 0)
                                {
                                    var media = new MediaModel { Id = id, Category = categoryValue, Title = title, Type = type };

                                    mediaItems.Add(media);
                                }
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

        /// <summary>
        /// Retrieves all media in a specific type.
        /// </summary>
        /// <returns>All media in a type</returns>
        public static List<MediaModel> RetrieveMediaByType(string type)
        {
            var mediaItems = new List<MediaModel>();

            try
            {
                var conn = DBConnection.GetConnection();
                using (conn)
                {
                    conn.Open();
                    var query = "select * from Media where type = @type";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@type", MySqlDbType.VarChar);
                        cmd.Parameters["@type"].Value = type;

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {

                            var categoryOrdinal = reader.GetOrdinal("Category");
                            var titleOrdinal = reader.GetOrdinal("Title");
                            var typeOrdinal = reader.GetOrdinal("Type");
                            var IDOrdinal = reader.GetOrdinal("Id");
                            var QtyOrdinal = reader.GetOrdinal("Qty");



                            while (reader.Read())
                            {
                                var id = reader.GetInt32((IDOrdinal));

                                var category = reader[categoryOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(categoryOrdinal);

                                var typeValue = reader[typeOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(typeOrdinal);

                                var title = reader[titleOrdinal] == DBNull.Value
                                    ? "null"
                                    : reader.GetString(titleOrdinal);

                                var qty = reader.GetInt32((QtyOrdinal));


                                if (qty > 0)
                                {
                                    var media = new MediaModel { Id = id, Category = category, Title = title, Type = typeValue };

                                    mediaItems.Add(media);
                                }
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
