using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using liveCoding.Models;
using liveCoding.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace liveCoding.Tests
{
    [TestClass]
    public class HomeControllerTests : IDisposable
    {
        public void Dispose()
        {
            Ticket.ClearAll();
        }
        [TestMethod]
        public void TicketListView_ContainsAListOfTickets()
        {
            //Arrange
            ViewResult indexView = new HomeController().TicketList() as ViewResult;
            //Act
            var thisTicketList = indexView.ViewData.Model;
            //Assert
            Assert.IsTrue(thisTicketList.GetType() == typeof(List<Ticket>));
        }
        [TestMethod]
        public void TicketScanAction_FirstTicketIsScanned()
        {
            //Arrange
            Ticket ticketOne = new Ticket(1, "1", "1-1-01");
            ticketOne.Save();
            Ticket ticketTwo = new Ticket(2, "2", "2-2-02");
            ticketTwo.Save();
            ViewResult indexView = new HomeController().ScanTicket(ticketOne.GetId()) as ViewResult;
            //Act
            List<Ticket> thisTicketList = indexView.ViewData.Model as List<Ticket>;
            //Assert
            Console.WriteLine(Ticket.GetAll().Count);
            Assert.IsTrue(true == thisTicketList[0].GetScanned());
            Assert.IsTrue(false == thisTicketList[1].GetScanned());
        }

    }
}
