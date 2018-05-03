using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace liveCoding.Models
{
        public class Event
        {
            private int _id;
            private string _name;
            private string _location;

            public Event(string name, string location, int id = 0)
            {
                _name = name;
                _location = location;
                _id = id;
            }

            public int GetId()
            {
                return _id;
            }

            public string GetName()
            {
                return _name;
            }

            public string GetLocation()
            {
                return _location;
            }

            public void Save()
            {
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"INSERT INTO events (name, location) VALUES (@thisName, @thisLocation)";

                cmd.Parameters.Add(new MySqlParameter("@thisName", _name));
                cmd.Parameters.Add(new MySqlParameter("@thisLocation", _location));

                cmd.ExecuteNonQuery();
                _id = (int) cmd.LastInsertedId;

                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

            public static List<Event> GetAll()
            {
                List<Event> allEvents = new List<Event> {};
                MySqlConnection conn = DB.Connection();
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = @"SELECT * FROM events";
                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

                while(rdr.Read())
                {
                    int id = rdr.GetInt32(0);
                    string name = rdr.GetString(1);
                    string location = rdr.GetString(2);
                    Event newEvent = new Event(name, location, id);
                    allEvents.Add(newEvent);
                }
                conn.Close();
                if (conn != null)
                {
                    conn.Dispose();
                }
                return allEvents;
            }


        }
}
