namespace Petclinic.Customers.DTOs
{
    public class PetType
    {

        public PetType() { }

        public int Id { get; set; }

        public string Name { get; set; }

        public static PetType ToDTO(Domain.PetType petType)
        {
            return new PetType()
            {
                Id = petType.Id,
                Name = petType.Name
            };
        }
    }
}
