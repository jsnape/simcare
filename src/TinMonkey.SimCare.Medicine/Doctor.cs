namespace TinMonkey.SimCare.Medicine;

public class Doctor(string Name, string Specialty) : Medic(Name)
{
    public string Specialty { get; } = Specialty;
}
