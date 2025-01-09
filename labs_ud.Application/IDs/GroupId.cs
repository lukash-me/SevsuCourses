namespace labs_ud.Application.IDs;

public class GroupId
{
    private GroupId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static GroupId NewGroupId() => new(Guid.NewGuid());
    public static GroupId EmptyId() => new(Guid.Empty);

    public static GroupId Create(Guid id) => new(id);
    public static implicit operator GroupId(Guid id) => new(id);
    public static implicit operator Guid(GroupId groupId)
    {
        ArgumentNullException.ThrowIfNull(groupId);
        return groupId.Value;
    }
}