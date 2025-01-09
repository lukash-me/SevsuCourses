namespace labs_ud.Application.IDs;

public class TeacherId
{
    private TeacherId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static TeacherId NewTeacherId() => new(Guid.NewGuid());
    public static TeacherId EmptyId() => new(Guid.Empty);

    public static TeacherId Create(Guid id) => new(id);
    public static implicit operator TeacherId(Guid id) => new(id);
    public static implicit operator Guid(TeacherId teacherId)
    {
        ArgumentNullException.ThrowIfNull(teacherId);
        return teacherId.Value;
    }
}