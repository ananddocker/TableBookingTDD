using System;
using System.Collections.Generic;
using TableBookingTDD.Core.Domain;

namespace TableBookingTDD.Core.Interfaces
{
    public interface ITableRepository
    {
        IEnumerable<Table> GetAvailableTables(DateTime date);
       
    }
}
