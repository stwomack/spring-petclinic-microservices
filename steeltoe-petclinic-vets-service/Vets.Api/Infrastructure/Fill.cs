using Petclinic.Vets.Domain;
using System.Linq;

namespace Petclinic.Vets.Infrastructure
{
    public static class Fill
    {
        public static Vet[] Vets => new[]
        {
            new Vet("James", "Carter", 1),
            new Vet("Helen", "Leary", 2),
            new Vet("Linda", "Douglas", 3),
            new Vet("Rafael", "Ortega", 4),
            new Vet("Henry", "Stevens", 5),
            new Vet("Sharon", "Jenkins", 6)
        };

        public static VetSpecialty[] VetSpecialties => new[]
        {
            new VetSpecialty(2, 1),
            new VetSpecialty(3, 2),
            new VetSpecialty(3, 3),
            new VetSpecialty(4, 2),
            new VetSpecialty(5, 1)
        };

        public static Specialty[] Specialties => new[]
        {
            new Specialty("radiology", 1),
            new Specialty("surgery", 2),
            new Specialty("dentistry", 3)
        };

    }
}
