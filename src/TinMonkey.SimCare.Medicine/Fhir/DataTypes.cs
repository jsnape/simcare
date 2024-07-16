global using Id = string;
global using Code = string;
global using Markdown = string;
global using Xhtml = string;
using System.Diagnostics;

namespace TinMonkey.SimCare.Medicine.Fhir;

public record Address(Code? Use, Code? Type, string? Text, string[]? Line,
    string? City, string? District, string? State, string? PostalCode, string? Country, Period? Period);

public record Annotation(Reference? AuthorReference, string? AuthorString, DateTime? Time, Markdown Text);

[DebuggerDisplay("{Code}")]
public record Coding(Uri? System, string? Version, Code? Code, string? Display, bool? UserSelected);

[DebuggerDisplay("{Text}")]
public record CodeableConcept(Coding[] Coding, string? Text);

public record CodeableReference(CodeableConcept? Concept, Reference? Reference);

[DebuggerDisplay("{Value}")]
public record Identifier(Code? Use, CodeableConcept? Type, Uri? System, string Value, Period? Period, Reference? Assigner);

public record HumanName(Code? Use, string Text, string? Family, string[]? Given, string[]? Prefix, string[]? Suffix, Period? Period);

[DebuggerDisplay("{Value}")]
public record Quantity(decimal Value, Code? Comparator, string Unit, Uri? System, Code? Code);

public record Range(Quantity Low, Quantity High);

public record Ratio(Quantity Numerator, Quantity Denominator);

public record RatioRange(Quantity LowNumerator, Quantity HighNumerator, Quantity Denominator);

public record Period(DateTime Start, DateTime? End);

public record Reference(string Ref, Uri? Type, Identifier? Identifier, string? Display);

public record Participant(CodeableConcept? Type, Period? Period, Reference? Actor);

public record Reason(CodeableConcept[]? Use, CodeableReference[]? Value);

public record Diagnosis(CodeableConcept[]? Use, CodeableReference[]? Condition);

public record Admission(Identifier? PreAdmissionIdentifier, Reference? Origin,
    CodeableConcept? AdmitSource, CodeableConcept? ReAdmission,  Reference? Destination, CodeableConcept[]? DischargeDisposition);

public record EncounterLocation(Reference Location, Code? Status, CodeableConcept? Form, Period? Period);

public record Narrative(Code Status, Xhtml Div);
