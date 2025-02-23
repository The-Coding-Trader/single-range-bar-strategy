namespace SingleRangeBar.Strategy.Marketry.Models;

public abstract class ValueObject<T>(int id, string name)
    where T : ValueObject<T>
{
    public static IReadOnlyList<T> All { get; protected set; }
    public int Id { get; } = id;
    public string Name { get; } = name;

    public override bool Equals(object obj)
    {
        if (obj is not T other) return false;

        return Id == other.Id;
    }

    public static T FromName(string name)
    {
        return All.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) ??
               throw new ArgumentException($"Invalid Name for {typeof(T).Name}: {name}");
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;

        return left.Equals(right);
    }

    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return Name;
    }
}