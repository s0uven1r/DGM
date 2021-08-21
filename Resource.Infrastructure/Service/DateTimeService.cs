using Resource.Application.Common.Interfaces;
using System;

namespace Resource.Infrastructure.Service
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
