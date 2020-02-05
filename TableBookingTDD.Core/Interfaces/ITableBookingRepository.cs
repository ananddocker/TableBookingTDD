using System;
using System.Collections.Generic;
using System.Text;
using TableBookingTDD.Core.Domain;

namespace TableBookingTDD.Core.Interfaces
{
    public interface ITableBookingRepository
    {
        void SaveBooking(TableBooking tableBooking);
    }
}
