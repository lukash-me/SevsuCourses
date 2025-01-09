namespace labs_ud.Application.IDs;

public class StudentId
{
    private StudentId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static StudentId NewStudentId() => new(Guid.NewGuid());
    public static StudentId EmptyId() => new(Guid.Empty);

    public static StudentId Create(Guid id) => new(id);
    public static implicit operator StudentId(Guid id) => new(id);
    public static implicit operator Guid(StudentId studentId)
    {
        ArgumentNullException.ThrowIfNull(studentId);
        return studentId.Value;
    }
}