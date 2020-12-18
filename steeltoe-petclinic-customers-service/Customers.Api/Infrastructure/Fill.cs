using Petclinic.Customers.Domain;
using System;
using System.Globalization;

namespace Petclinic.Customers.Infrastructure
{
    public static class Fill
    {
        public static PetType[] PetTypes => new[]
        {
            new PetType("cat", 1),
            new PetType("dog", 2),
            new PetType("lizard", 3),
            new PetType("snake", 4),
            new PetType("bird", 5),
            new PetType("hamster", 6)
        };

        public static Owner[] Owners => new[]
        {
            new Owner("George", "Franklin", "110 W. Liberty St.", "Madison", "6085551023", 1),
            new Owner("Betty", "Davis", "638 Cardinal Ave.", "Sun Prairie", "6085551749", 2),
            new Owner("Eduardo", "Rodriquez", "2693 Commerce St.", "McFarland", "6085558763", 3),
            new Owner("Harold", "Davis", "563 Friendly St.", "Windsor", "6085553198", 4),
            new Owner("Peter", "McTavish", "2387 S. Fair Way", "Madison", "6085552765", 5),
            new Owner("Jean", "Coleman", "105 N. Lake St.", "Monona", "6085552654", 6),
            new Owner("Jeff", "Black", "1450 Oak Blvd.", "Monona", "6085555387", 7),
            new Owner("Maria", "Escobito", "345 Maple St.", "Madison", "6085557683", 8),
            new Owner("David", "Schroeder", "2749 Blackhawk Trail", "Madison", "6085559435", 9),
            new Owner("Carlos", "Estaban", "2335 Independence La.", "Waunakee", "6085555487", 10)
        };

        public static Pet[] Pets => new[]
        {
            new Pet("Leo", DateTime.ParseExact("2010-09-07", "yyyy-MM-dd", CultureInfo.InvariantCulture), 1, 1, 1),
            new Pet("Basil", DateTime.ParseExact("2012-08-06", "yyyy-MM-dd", CultureInfo.InvariantCulture), 2, 3, 2),
            new Pet("Rosy", DateTime.ParseExact("2011-04-17", "yyyy-MM-dd", CultureInfo.InvariantCulture), 2, 3, 3),
            new Pet("Jewel", DateTime.ParseExact("2010-03-07", "yyyy-MM-dd", CultureInfo.InvariantCulture), 2, 3, 4),
            new Pet("Iggy", DateTime.ParseExact("2010-11-30", "yyyy-MM-dd", CultureInfo.InvariantCulture), 3, 4, 5),
            new Pet("George", DateTime.ParseExact("2010-01-20", "yyyy-MM-dd", CultureInfo.InvariantCulture), 4, 5, 6),
            new Pet("Samantha", DateTime.ParseExact("2012-09-04", "yyyy-MM-dd", CultureInfo.InvariantCulture), 1, 6, 7),
            new Pet("Max", DateTime.ParseExact("2012-09-04", "yyyy-MM-dd", CultureInfo.InvariantCulture), 1, 6, 8),
            new Pet("Lucky", DateTime.ParseExact("2011-08-06", "yyyy-MM-dd", CultureInfo.InvariantCulture), 5, 7, 9),
            new Pet("Mulligan", DateTime.ParseExact("2007-02-24", "yyyy-MM-dd", CultureInfo.InvariantCulture), 2, 8, 10),
            new Pet("Freddy", DateTime.ParseExact("2010-03-09", "yyyy-MM-dd", CultureInfo.InvariantCulture), 5, 9, 11),
            new Pet("Lucky", DateTime.ParseExact("2010-06-24", "yyyy-MM-dd", CultureInfo.InvariantCulture), 2, 10, 12),
            new Pet("Sly", DateTime.ParseExact("2012-06-08", "yyyy-MM-dd", CultureInfo.InvariantCulture), 1, 10, 13)
        };
    }
}
