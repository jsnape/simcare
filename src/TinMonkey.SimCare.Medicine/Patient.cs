namespace TinMonkey.SimCare.Medicine;

public class Patient(string FullName, DateOnly BirthDate)
{
    public string FullName { get; } = FullName;

    public DateOnly BirthDate { get; } = BirthDate;

    public BloodPressure BloodPressure { get; set; } = BloodPressure.Default;
}
