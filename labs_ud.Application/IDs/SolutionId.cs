namespace labs_ud.Application.IDs;

public class SolutionId
{
    private SolutionId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static SolutionId NewSolutionId() => new(Guid.NewGuid());
    public static SolutionId EmptyId() => new(Guid.Empty);

    public static SolutionId Create(Guid id) => new(id);
    public static implicit operator SolutionId(Guid id) => new(id);
    public static implicit operator Guid(SolutionId solutionId)
    {
        ArgumentNullException.ThrowIfNull(solutionId);
        return solutionId.Value;
    }
}