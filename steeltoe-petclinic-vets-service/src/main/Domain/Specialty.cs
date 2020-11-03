using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace steeltoe_petclinic_vets_api.Domain
{
  public class Specialty : IComparable {

    public static Specialty Radiology = new Specialty("radiology", 1);

    public static Specialty Surgery = new Specialty("surgery", 2);

    public static Specialty Dentistry = new Specialty("dentistry", 3);

    public Specialty(string name, int id = default) {
      Id = id;
      Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public int Id { get; private set; }
    public string Name { get; private set; }

    public int CompareTo(object obj) {
      if (obj == null) return 1;

      Specialty other = obj as Specialty;
      if (other != null)
        return this.Id.CompareTo(other.Id);
      else
        throw new ArgumentException("Object is not a specialty");
    }
    public static IEnumerable<Specialty> GetAll() {
      return new List<Specialty>() { Radiology, Surgery, Dentistry };
    }
  }
}
