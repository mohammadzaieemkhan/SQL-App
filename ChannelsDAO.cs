using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class ChannelsDAO
    {
        string connectionString = "datasource=localhost;port=3306;username=root;password=root;database=coding_youtubers;";
        private object youtubers;

        public List<Youtubers> getAllYoutubers()
        {
            List<Youtubers> returnThese = new List<Youtubers>();
            MySqlConnection connection = new MySqlConnection
              (connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("SELECT*FROM  youtubers", connection);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Youtubers a = new Youtubers
                    {
                        ID = reader.GetInt32(0),
                        ChannelName = reader.GetString(1),
                        TotalsSubscribers = reader.GetString(2),
                        Speciality = reader.GetString(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)

                    };
                    returnThese.Add(a);
                }

            }
            connection.Close();

            return returnThese;
        }

        public List<Youtubers> searchTitles(string searchTerm)
        {
            List<Youtubers> returnThese = new List<Youtubers>();
            MySqlConnection connection = new MySqlConnection
              (connectionString);
            connection.Open();
            String searchWildPhrase = "%" + searchTerm + "%";
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM youtubers WHERE `Channel Name` LIKE @search";
            command.Parameters.AddWithValue("@search", searchWildPhrase);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Youtubers a = new Youtubers
                    {
                        ID = reader.GetInt32(0),
                        ChannelName = reader.GetString(1),
                        TotalsSubscribers = reader.GetString(2),
                        Speciality = reader.GetString(3),
                        ImageURL = reader.GetString(4),
                        Description = reader.GetString(5)

                    };
                    returnThese.Add(a);
                }

            }
            connection.Close();

            return returnThese;
        }

        internal int addOneYoutuber(Youtubers youtuber)
        {
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("INSERT INTO `youtubers`( `Channel Name`, `Totals Subscribers`, `Speciality`, `Image`, `Description`) VALUES (@ChannelName ,@TotalsSubscribers, @Speciality, @ImageURL,@Description)", connection);
            command.Parameters.AddWithValue("@ChannelName", youtuber.ChannelName);
            command.Parameters.AddWithValue("@TotalsSubscribers", youtuber.TotalsSubscribers);
            command.Parameters.AddWithValue("@Speciality", youtuber.Speciality);
            command.Parameters.AddWithValue("@ImageURL", youtuber.ImageURL);
            command.Parameters.AddWithValue("@Description", youtuber.Description);

            int newRows = command.ExecuteNonQuery();
            connection.Close();

            return newRows;
        }

        internal int deleteVideo(int videoID)
        {
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("DELETE FROM videos WHERE `videos`.`ID` = @videoID", connection);
            command.Parameters.AddWithValue("@videoID", videoID);
            int result = command.ExecuteNonQuery();
            int newRows = command.ExecuteNonQuery();
            connection.Close();
            return result;

        }

        public List<Videos> getVideo(int youtubersID)
        {
            List<Videos> returnThese = new List<Videos>();
            MySqlConnection connection = new MySqlConnection
              (connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand();
            command.CommandText = "SELECT * FROM Videos WHERE youtubers_ID= @youtubersID";
            command.Parameters.AddWithValue("@youtubersid", youtubersID);
            command.Connection = connection;
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Videos v = new Videos
                    {
                        ID = reader.GetInt32(0),
                        Video_Title = reader.GetString(1),
                        Video_URL = reader.GetString(2),
                        youtubers_ID = reader.GetInt32(3)


                    };
                    returnThese.Add(v);
                }

            }
            connection.Close();

            return returnThese;

        }

        internal int addOneVideo(Videos video)
        {
            MySqlConnection connection = new MySqlConnection
                (connectionString);
            connection.Open();

            MySqlCommand command = new MySqlCommand("INSERT INTO videos(Video_Title, Video_URL, youtubers_ID) VALUES (@Video_Title, @Video_URL,@youtubers_ID)", connection);
            command.Parameters.AddWithValue("@Video_Title", video.Video_Title);
            command.Parameters.AddWithValue("@Video_URL", video.Video_URL);
            command.Parameters.AddWithValue("@youtubers_ID", video.youtubers_ID);
            int Rows = command.ExecuteNonQuery();
            connection.Close();

            return Rows;
        }

    }
}
