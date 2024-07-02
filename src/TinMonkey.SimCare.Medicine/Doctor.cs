namespace TinMonkey.SimCare.Medicine;

public class Doctor(string Name, string Specialty)
{
    public string Name { get; } = Name;

    public string Specialty { get; } = Specialty;
}
