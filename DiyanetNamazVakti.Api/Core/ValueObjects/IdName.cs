namespace DiyanetNamazVakti.Api.Core.ValueObjects;

public class IdName<T>
{
    public IdName(T id, string name)
    {
        Id = id;
        Name = name;
    }

    public T Id { get; set; }
    public string Name { get; set; }
}