using System;

namespace TableBookingTDD.Core.Domain
{
    public class TableBookingResult : BaseTableBooking
    {
        public TableBookingResultCode Code { get; set; }
        public int BookingId { get; set; }
    }
}