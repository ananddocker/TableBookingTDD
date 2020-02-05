using System;

namespace TableBookingTDD.Core.Domain
{
    public class TableBookingRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}