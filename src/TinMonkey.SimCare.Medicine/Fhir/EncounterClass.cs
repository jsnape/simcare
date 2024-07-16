namespace TinMonkey.SimCare.Medicine.Fhir;

public static class EncounterClass
{
    public static readonly Uri System = new("http://terminology.hl7.org/CodeSystem/v3-ActCode");

    public static readonly CodeableConcept InPatient =
        new([new(System, null, "IMP", "inpatient encounter", null)], "Inpatient Encounter");
}
