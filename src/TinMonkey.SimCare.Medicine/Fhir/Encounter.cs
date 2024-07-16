namespace TinMonkey.SimCare.Medicine.Fhir;

public class Encounter(Code Status) : DomainResource
{
    public Identifier? Identifier { get; set; }

    public Code Status { get; } = Status;

    public CodeableConcept? EncounterClass { get; set; }

    public CodeableConcept? Priority { get; set; }

    public CodeableConcept? EncounterType { get; set; }

    public required Reference Subject { get; set; }

    public CodeableConcept? SubjectStatus { get; set; }

    public Reference? EpisodeOfCare { get; set; }

    public Reference? BasedOn { get; set; }

    public Reference? CareTeam { get; set; }

    public Reference? PartOf { get; set; }

    public Reference? ServiceProvider { get; set; }

    public IList<Participant> Participants { get; } = [];

    public Reference? Appointment { get; set; }

    public Period? Period { get; set; }

    public DateTime? PlannedStartDate { get; set; }

    public DateTime? PlannedEndDate { get; set; }

    public TimeSpan? Length
    {
        get
        {
            var start = this.Period?.Start ?? this.PlannedStartDate;
            var end = this.Period?.End ?? this.PlannedEndDate;

            return start == null || end == null ? null : end - start;
        }
    }

    public IList<Reason> Reasons { get; } = [];

    public IList<CodeableConcept> DietPreferences { get; } = [];

    public IList<CodeableConcept> SpecialArrangements { get; } = [];

    public IList<CodeableConcept> SpecialCourtesies { get; } = [];

    public IList<Diagnosis> Diagnoses { get; } = [];

    public Admission? Admission { get; set; }

    public IList<EncounterLocation> Locations { get; } = [];
}
