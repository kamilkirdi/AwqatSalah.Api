namespace DiyanetNamazVakti.Api.Core.ValueObjects
{
    public class IdCodeName<T>
    {
        public T Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public IdCodeName()
        {

        }
        public IdCodeName(T id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }
    }
}