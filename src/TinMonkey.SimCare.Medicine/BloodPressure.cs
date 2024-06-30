namespace TinMonkey.SimCare.Medicine;

public record BloodPressure(int Systolic, int Diastolic)
{
    public static BloodPressure Default => new(120, 80);

    public override string ToString() => $"{this.Systolic}/{this.Diastolic}";
}
