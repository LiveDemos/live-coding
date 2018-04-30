using System.Collections.Generic;

namespace liveCoding.Models
{
    public class Ticket
    {
        private int _price; //15
        private string _seat; //"D5"
        private string _date; //"Monday"
        private bool _scanned; //true
        private int _id; //1

        //STATIC properties
        private static List<Ticket> _instances = new List<Ticket> {};
        private static int _counter = 0;

        //Constructor
        public Ticket(int price, string seat, string date, bool scanned = false)
        {
            _price = price;
            _seat = seat;
            _date = date;
            _scanned = scanned;
            _counter++;
            _id = _counter;
        }

        public void Scan()
        {
            _scanned = true;
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
            return _instances;
        }

        public void Save()
        {
            _instances.Add(this);
        }

        public static Ticket Find(int searchId)
        {
            foreach (Ticket ticket in _instances) {
                if (ticket._id == searchId) {
                    return ticket;
                }
            }
            return null;
        }

        public static void ClearAll()
        {
            _instances.Clear();
        }
    }
}
