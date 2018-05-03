using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using liveCoding.Models;

namespace liveCoding.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet("/tickets")]
        public ActionResult TicketList()
        {
            List<Ticket> allTickets = Ticket.GetAll();
            return View(allTickets);
        }

        [HttpGet("/tickets/new")]
        public ActionResult TicketForm()
        {
            return View(Event.GetAll());
        }

        [HttpPost("/submit")]
        public ActionResult CreateTicket()
        {
            int price = int.Parse(Request.Form["price"]);
            string seat = Request.Form["seat"];
            string date = Request.Form["date"];
            int event_id = int.Parse(Request.Form["event"]);
            Ticket newTicket = new Ticket(price, seat, date, event_id);
            newTicket.Save();
            return RedirectToAction("TicketList");
        }

        [HttpGet("/events")]
        public ActionResult EventList()
        {
            List<Event> allEvents = Event.GetAll();
            return View(allEvents);
        }


    }
}
