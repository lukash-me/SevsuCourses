namespace labs_ud.Application.IDs;

public class AnswerId
{
    private AnswerId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static AnswerId NewAnswerId() => new(Guid.NewGuid());
    public static AnswerId EmptyId() => new(Guid.Empty);

    public static AnswerId Create(Guid id) => new(id);
    public static implicit operator AnswerId(Guid id) => new(id);
    public static implicit operator Guid(AnswerId answerId)
    {
        ArgumentNullException.ThrowIfNull(answerId);
        return answerId.Value;
    }
}