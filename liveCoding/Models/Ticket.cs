using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace liveCoding.Models
{
    public class Ticket
    {
        private int _id; //1
        private int _price; //15
        private string _seat; //"D5"
        private string _date; //"Monday"
        private bool _scanned; //true
        private int _event_id; //12

        //Constructor
        public Ticket(int price, string seat, string date, int event_id, bool scanned = false, int id = 0)
        {
            _price = price;
            _seat = seat;
            _date = date;
            _scanned = scanned;
            _id = id;
            _event_id = event_id;
        }

        public void Scan()
        {
            _scanned = true;
        }

        public int GetEventId()
        {
            return _event_id;
        }

        public int GetPrice()
        {
            return _price;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetPrice(int newPrice)
        {
            _price = newPrice;
        }

        public string GetSeat()
        {
            return _seat;
        }

        public string GetDate()
        {
            return _date;
        }

        public bool GetScanned()
        {
            return _scanned;
        }

        public static List<Ticket> GetAll()
        {
            List<Ticket> allTickets = new List<Ticket> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM tickets";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                int price = rdr.GetInt32(1);
                string seat = rdr.GetString(2);
                string date = rdr.GetString(3);
                bool scanned = rdr.GetBoolean(4);
                int event_id = rdr.GetInt32(5);
                Ticket newTicket = new Ticket(price, seat, date, event_id, scanned, id);
                allTickets.Add(newTicket);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allTickets;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO tickets (price, seat, date, scanned, event_id) VALUES (@thisPrice, @thisSeat, @thisDate, 0, @thisEventId)";

            cmd.Parameters.Add(new MySqlParameter("@thisPrice", _price));
            cmd.Parameters.Add(new MySqlParameter("@thisSeat", _seat));
            cmd.Parameters.Add(new MySqlParameter("@thisDate", _date));
            cmd.Parameters.Add(new MySqlParameter("@thisEventId", _event_id));

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public string GetEventName()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT name FROM events WHERE id = @thisEventId;";
            cmd.Parameters.Add(new MySqlParameter("@thisEventId", _event_id));

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            string eventName = "";

            while(rdr.Read())
            {
                eventName = rdr.GetString(0);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return eventName;
        }

        // public static Ticket Find(int searchId)
        // {
        //     foreach (Ticket ticket in _instances) {
        //         if (ticket._id == searchId) {
        //             return ticket;
        //         }
        //     }
        //     return null;
        // }

        // public static void ClearAll()
        // {
        //     _instances.Clear();
        // }
    }
}
