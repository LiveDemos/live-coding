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
            return View();
        }

        [HttpPost("/submit")]
        public ActionResult CreateTicket()
        {
            int price = int.Parse(Request.Form["price"]);
            string seat = Request.Form["seat"];
            string date = Request.Form["date"];
            Ticket newTicket = new Ticket(price, seat, date);
            newTicket.Save();
            return RedirectToAction("TicketList");
        }
        [HttpGet("/scan/{id}")]
        public ActionResult ScanTicket(int id)
        {
            Ticket.Find(id).Scan();
            return View("TicketList", Ticket.GetAll());
            // return RedirectToAction("TicketList");
        }



    }
}
