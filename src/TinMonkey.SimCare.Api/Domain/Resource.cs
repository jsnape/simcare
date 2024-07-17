using TinMonkey.SimCare.Core;

namespace TinMonkey.SimCare.Api.Domain;

public class Resource
{
    public Id Uid { get; set; } = Guid.NewGuid().ToBase36();

    public override int GetHashCode() => this.Uid.GetHashCode();

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (Object.ReferenceEquals(this, obj))
        {
            return true;
        }

        if (this.GetType() != obj.GetType())
        {
            return false;
        }

        return obj is Resource resource && resource?.Uid == this.Uid;
    }
}
