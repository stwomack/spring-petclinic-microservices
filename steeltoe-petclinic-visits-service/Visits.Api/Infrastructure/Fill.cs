using Petclinic.Visits.Domain;
using System;
using System.Globalization;

namespace Petclinic.Visits.Infrastructure
{
    public static class Fill
    {
        public static Visit[] Visits => new[]
        {
            new Visit(7, DateTime.ParseExact("2013-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture), "rabies shot", id: 1),
            new Visit(8, DateTime.ParseExact("2013-01-02", "yyyy-MM-dd", CultureInfo.InvariantCulture), "rabies shot", id: 2),
            new Visit(8, DateTime.ParseExact("2013-01-03", "yyyy-MM-dd", CultureInfo.InvariantCulture), "neutered", id: 3),
            new Visit(7, DateTime.ParseExact("2013-01-04", "yyyy-MM-dd", CultureInfo.InvariantCulture), "spayed", id: 4)
        };
    }
}
