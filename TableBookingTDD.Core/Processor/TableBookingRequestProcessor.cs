using System;
using System.Linq;
using TableBookingTDD.Core.Domain;
using TableBookingTDD.Core.Interfaces;

namespace TableBookingTDD.Core.Tests
{
    public class TableBookingRequestProcessor
    {
        private readonly ITableBookingRepository _bookingRepository;
        private readonly ITableRepository _tableRepository;
        public TableBookingRequestProcessor(ITableBookingRepository tableBookingRepository, ITableRepository tableRepository)
        {
            _bookingRepository = tableBookingRepository;
            _tableRepository = tableRepository;
        }

        public TableBookingResult BookTable(TableBookingRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var tables = _tableRepository.GetAvailableTables(request.Date);
            var result = Assign<TableBookingResult>(request);
            if (tables.FirstOrDefault() is Table availableTable)
            { 
                var tableBooking = Assign<TableBooking>(request);
                tableBooking.TableId = availableTable.Id;
                _bookingRepository.SaveBooking(tableBooking);                
                result.Code = TableBookingResultCode.Available;
                result.BookingId = tableBooking.TableId;
            }
            else
            {
                result.Code = TableBookingResultCode.NotAvailable;
            }
            return result;
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