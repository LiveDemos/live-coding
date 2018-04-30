using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using liveCoding.Models;
using System;

namespace liveCoding.Tests
{
    [TestClass]
    public class TicketTests : IDisposable
    {
        public void Dispose()
        {
            Ticket.ClearAll();
        }
        [TestMethod]
        public void TicketIsUnscannedByDefault_True()
        {
            //Arrange
            Ticket testTicket = new Ticket(20, "2A", "02/12/2018");
            //Act
            bool scannedStatus = testTicket.GetScanned();
            //Assert
            Assert.AreEqual(false, scannedStatus);
        }
        [TestMethod]
        public void Ticket_ScanningChangestoTrue_True()
        {
            //Arrange
            Ticket testTicket = new Ticket(20, "2A", "02/12/2018");
            //Act
            testTicket.Scan();
            bool scannedStatus = testTicket.GetScanned();
            //Assert
            Assert.AreEqual(true, scannedStatus);
        }
    }
}
