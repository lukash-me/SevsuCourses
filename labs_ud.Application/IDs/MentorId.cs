namespace labs_ud.Application.IDs;

public class MentorId
{
    private MentorId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static MentorId NewMentorId() => new(Guid.NewGuid());
    public static MentorId EmptyId() => new(Guid.Empty);

    public static MentorId Create(Guid id) => new(id);
    public static implicit operator MentorId(Guid id) => new(id);
    public static implicit operator Guid(MentorId mentorId)
    {
        ArgumentNullException.ThrowIfNull(mentorId);
        return mentorId.Value;
    }
}