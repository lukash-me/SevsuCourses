namespace labs_ud.Application.IDs;

public class CourseID
{
    private CourseID(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static CourseID NewCourseId() => new(Guid.NewGuid());
    public static CourseID EmptyId() => new(Guid.Empty);

    public static CourseID Create(Guid id) => new(id);
    public static implicit operator CourseID(Guid id) => new(id);
    public static implicit operator Guid(CourseID courseId)
    {
        ArgumentNullException.ThrowIfNull(courseId);
        return courseId.Value;
    }
}