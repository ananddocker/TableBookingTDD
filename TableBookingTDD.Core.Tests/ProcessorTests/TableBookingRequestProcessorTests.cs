using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TableBookingTDD.Core.Domain;
using TableBookingTDD.Core.Interfaces;

namespace TableBookingTDD.Core.Tests
{
    [TestFixture]
    public class TableBookingRequestProcessorTests
    {
        TableBookingRequest _request;
        TableBookingRequestProcessor _processor;
        Mock<ITableBookingRepository> _bookingRepository;
        Mock<ITableRepository> _tableRepository;
        List<Table> _tables;

        [SetUp] 
        public void Initialise()
        {
            _bookingRepository = new Mock<ITableBookingRepository>();
            _tableRepository = new Mock<ITableRepository>();
            _request = new TableBookingRequest { FirstName = "John", LastName = "Doe", Email = "john.doe@hotmail.com", Date = new DateTime(2020, 12, 12) };
            _processor = new TableBookingRequestProcessor(_bookingRepository.Object,_tableRepository.Object);
            _tables = new List<Table> { new Table { Id=1 } };
            
            _tableRepository.Setup(_ => _.GetAvailableTables(_request.Date)).Returns(_tables);
        }

        [Test]
        public void Should_return_request_on_booking()
        {
            TableBookingResult result = _processor.BookTable(_request);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.FirstName, _request.FirstName);
            Assert.AreEqual(result.LastName, _request.LastName);
            Assert.AreEqual(result.Email, _request.Email);
            Assert.AreEqual(result.Date, _request.Date);
        }

        [Test]
        public void Should_return_argument_null_exception()
        {
            var result = Assert.Throws<ArgumentNullException>(() => _processor.BookTable(null));
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

            var result = _processor.BookTable(_request);
           
            _bookingRepository.Verify(_ => _.SaveBooking(It.IsAny<TableBooking>()), Times.Once);
            Assert.IsNotNull(savedBooking);
            Assert.AreEqual(savedBooking.FirstName, _request.FirstName);
            Assert.AreEqual(savedBooking.LastName, _request.LastName);
            Assert.AreEqual(savedBooking.Email, _request.Email);
            Assert.AreEqual(savedBooking.Date, _request.Date);
            Assert.AreEqual(_tables.First().Id, savedBooking.TableId);
        }

        [Test]
        public void should_not_save_if_booking_unavailable()
        {
            _tables.Clear();          

            _processor.BookTable(_request);

            _bookingRepository.Verify(_ => _.SaveBooking(It.IsAny<TableBooking>()), Times.Never);
        }

        [Test]
        [TestCase(TableBookingResultCode.Available, true)]
        [TestCase(TableBookingResultCode.NotAvailable, false)]
        public void should_return_success_code_on_booking(TableBookingResultCode tableBookingResultCode, bool isAvailable)
        {
            if (!isAvailable)
            {
                _tables.Clear();
            }
            var result=  _processor.BookTable(_request);

            Assert.AreEqual(tableBookingResultCode,result.Code);

        }

        [Test]
        [TestCase(1, true)]
        [TestCase(null, false)]
        public void should_return_expected_bookingId_on_booking(int bookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _tables.Clear();
            }
            else
            {
                _bookingRepository.Setup(_ => _.SaveBooking(It.IsAny<TableBooking>())).Callback<TableBooking>(_ => {
                    _.TableId = bookingId;
                });
            }
            var result = _processor.BookTable(_request);

            Assert.AreEqual(bookingId, result.BookingId);

        }

        [Test]
        [TestCase(1, true)]
        [TestCase(null, false)]
        public void should_return_expected_bookingId_on_booking_duplicate(int bookingId, bool isAvailable)
        {
            if (!isAvailable)
            {
                _tables.Clear();
            }
            else
            {
                _bookingRepository.Setup(_ => _.SaveBooking(It.IsAny<TableBooking>())).Callback<TableBooking>(_ => {
                    _.TableId = bookingId;
                });
            }
            var result = _processor.BookTable(_request);

            Assert.AreEqual(bookingId, result.BookingId);

        }
    }
}