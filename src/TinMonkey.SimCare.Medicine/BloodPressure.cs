namespace TinMonkey.SimCare.Medicine;

public record BloodPressure(int Systolic, int Diastolic)
{
    private static readonly Random random = new();

    public static BloodPressure Default =>
        new(120, 80);

    public static BloodPressure CreateNormal() =>
        new(random.Next(90, 120), random.Next(60, 80));

    public override string ToString() => $"{this.Systolic}/{this.Diastolic}";

    public BloodPressure Next() =>
        new(this.Systolic + random.Next(-1, 1), this.Diastolic + random.Next(-1, 1));
}
