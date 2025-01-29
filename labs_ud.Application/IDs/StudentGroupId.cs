namespace labs_ud.Application.IDs;

public class StudentGroupId
{
    private StudentGroupId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static StudentGroupId NewStudentGroupId() => new(Guid.NewGuid());
    public static StudentGroupId EmptyId() => new(Guid.Empty);

    public static StudentGroupId Create(Guid id) => new(id);
    public static implicit operator StudentGroupId(Guid id) => new(id);
    public static implicit operator Guid(StudentGroupId studentGroupId)
    {
        ArgumentNullException.ThrowIfNull(studentGroupId);
        return studentGroupId.Value;
    }
}