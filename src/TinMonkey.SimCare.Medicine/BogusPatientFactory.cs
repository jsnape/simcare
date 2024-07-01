using Bogus;

namespace TinMonkey.SimCare.Medicine;

public class BogusPatientFactory
{
    public Patient CreateBogusPatient()
    {
        var person = new Person();
        return new(person.FullName, DateOnly.FromDateTime(person.DateOfBirth));
    }
}
