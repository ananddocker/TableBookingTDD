using System;
using TableBookingTDD.Core.Domain;

namespace TableBookingTDD.Core.Tests
{
    public class TableBookingRequestProcessor
    {
        public TableBookingRequestProcessor()
        {
        }

        public TableBookingResult BookTable(TableBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            return new TableBookingResult
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}