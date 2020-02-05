using NUnit.Framework;
using System;
using TableBookingTDD.Core.Domain;

namespace TableBookingTDD.Core.Tests
{
    [TestFixture]
    public class TableBookingRequestProcessorTests
    {
        TableBookingRequest request;
        TableBookingRequestProcessor processor;

        [SetUp]
        public void Initialise()
        {
            request = new TableBookingRequest { FirstName = "John", LastName = "Doe", Email = "john.doe@hotmail.com", Date = new DateTime(2020, 12, 12) };
            processor = new TableBookingRequestProcessor();
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

    }
}