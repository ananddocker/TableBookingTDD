using System;
using TableBookingTDD.Core.Domain;
using TableBookingTDD.Core.Interfaces;

namespace TableBookingTDD.Core.Tests
{
    public class TableBookingRequestProcessor
    {
        private readonly ITableBookingRepository _bookingRepository;
        public TableBookingRequestProcessor(ITableBookingRepository tableBookingRepository)
        {
            _bookingRepository = tableBookingRepository;
        }

        public TableBookingResult BookTable(TableBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            _bookingRepository.SaveBooking(Assign<TableBooking>(request));

            return Assign<TableBookingResult>(request);
        }

        private static T Assign<T>(TableBookingRequest request) where T:BaseTableBooking,new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Date = request.Date
            };
        }
    }
}