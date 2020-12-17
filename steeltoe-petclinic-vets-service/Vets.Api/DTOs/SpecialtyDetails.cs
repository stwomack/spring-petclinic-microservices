namespace Petclinic.Vets.DTOs
{
    public class SpecialtyDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SpecialtyDetails() { }

        public SpecialtyDetails(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
