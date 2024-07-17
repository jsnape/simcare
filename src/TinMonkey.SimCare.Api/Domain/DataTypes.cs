global using Id = string;
global using Code = string;
global using Xhtml = string;
global using Markdown = string;

using System.Diagnostics;

namespace TinMonkey.SimCare.Api.Domain;

public enum NarrativeStatus
{
    Generated,
    Extensions,
    Additional,
    Empty
}

public enum IdentifierUse
{
    Usual,
    Official,
    Temp,
    Secondary,
    Old
}

public record Narrative(NarrativeStatus Status, Xhtml Div);

[DebuggerDisplay("{Code}")]
public record Coding(Uri? System, string? Version, Code? Code, string? Display, bool? UserSelected = null);

[DebuggerDisplay("{Text}")]
public class CodeableConcept(Coding[] Coding, string? Text)
{
    public CodeableConcept()
        : this([], null)
    {
    }

    public IList<Coding> Coding { get; } = Coding;

    public string? Text { get; set; } = Text;
}

[DebuggerDisplay("{Start}-{End}")]
public record Period(DateTime Start, DateTime? End);

[DebuggerDisplay("{Ref}")]
public record Reference(string Ref, Uri? Type, Identifier? Identifier, string? Display);

[DebuggerDisplay("{Value}")]
public class Identifier(IdentifierUse Use, IdentifierType? Type, Uri? System, string Value)
{
    public IdentifierUse IdentifierUse { get; set; } = Use;

    public IdentifierType? Type { get; set; } = Type;

    public Uri? System { get; set; } = System;

    public string Value { get; set; } = Value;

    public Period? Period { get; set; }

    public Reference? Assigner { get; set; }
}
