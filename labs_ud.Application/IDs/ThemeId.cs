namespace labs_ud.Application.IDs;

public class ThemeId
{
    private ThemeId(Guid value)
    {
        Value = value;
    }
    public Guid Value { get; }
    
    //ToDo
    public static ThemeId NewThemeId() => new(Guid.NewGuid());
    public static ThemeId EmptyId() => new(Guid.Empty);

    public static ThemeId Create(Guid id) => new(id);
    public static implicit operator ThemeId(Guid id) => new(id);
    public static implicit operator Guid(ThemeId themeId)
    {
        ArgumentNullException.ThrowIfNull(themeId);
        return themeId.Value;
    }
}