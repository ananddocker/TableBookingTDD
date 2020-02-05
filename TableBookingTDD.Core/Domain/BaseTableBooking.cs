using System;
using System.Collections.Generic;
using System.Text;

namespace TableBookingTDD.Core.Domain
{
    public class BaseTableBooking
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
