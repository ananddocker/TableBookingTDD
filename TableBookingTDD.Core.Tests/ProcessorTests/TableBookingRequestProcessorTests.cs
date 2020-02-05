using Moq;
using NUnit.Framework;
using System;
using TableBookingTDD.Core.Domain;
using TableBookingTDD.Core.Interfaces;

namespace TableBookingTDD.Core.Tests
{
    [TestFixture]
    public class TableBookingRequestProcessorTests
    {
        TableBookingRequest request;
        TableBookingRequestProcessor processor;
        Mock<ITableBookingRepository> _bookingRepository;

        //public TableBookingRequestProcessorTests()
        //{
           
        //}
        [SetUp] 
        public void Initialise()
        {
            _bookingRepository = new Mock<ITableBookingRepository>();
            request = new TableBookingRequest { FirstName = "John", LastName = "Doe", Email = "john.doe@hotmail.com", Date = new DateTime(2020, 12, 12) };
            processor = new TableBookingRequestProcessor(_bookingRepository.Object);
        }

        [Test]
        public void Should_return_request_on_booking()
        {
            TableBookingResult result = processor.BookTable(request);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstName, request.FirstName);
            Assert.AreEqual(result.LastName, request.LastName);
            Assert.AreEqual(result.Email, request.Email);
            Assert.AreEqual(result.Date, request.Date);
        }

        [Test]
        public void Should_return_argument_null_exception()
        {
            var result = Assert.Throws<ArgumentNullException>(() => processor.BookTable(null));
            Assert.AreEqual("request", result.ParamName);
        }

        [Test]
        public void Should_save_request_booking()
        {
            TableBooking savedBooking = null;
            _bookingRepository.Setup(_ => _.SaveBooking(It.IsAny<TableBooking>()))
                .Callback<TableBooking>(tableBooking => {
                    savedBooking = tableBooking;
                });

            var result =    processor.BookTable(request);
           
            _bookingRepository.Verify(_ => _.SaveBooking(It.IsAny<TableBooking>()), Times.Once);
            Assert.IsNotNull(savedBooking);
            Assert.AreEqual(savedBooking.FirstName, request.FirstName);
            Assert.AreEqual(savedBooking.LastName, request.LastName);
            Assert.AreEqual(savedBooking.Email, request.Email);
            Assert.AreEqual(savedBooking.Date, request.Date);
        }

    }
}