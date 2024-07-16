namespace TinMonkey.SimCare.Medicine;

public class Hospital(string Name)
{
    public string Name { get; } = Name;

    public IList<Department> Departments { get; } = [];

    public IList<Medic> Staff { get; } = [];

    public IList<Patient> Patients { get; } = [];
}
