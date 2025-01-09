namespace labs_ud.Application.IDs;

public class TaskId
{
    private TaskId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static TaskId NewTaskId() => new(Guid.NewGuid());
    public static TaskId EmptyId() => new(Guid.Empty);

    public static TaskId Create(Guid id) => new(id);
    public static implicit operator TaskId(Guid id) => new(id);
    public static implicit operator Guid(TaskId taskId)
    {
        ArgumentNullException.ThrowIfNull(taskId);
        return taskId.Value;
    }
}