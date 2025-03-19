using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Task
{
    public Task(
        Guid themeId,
        string title,
        string condition,
        int minMark,
        int maxMark)
    {
        Id = TaskId.NewTaskId();
        ThemeId = themeId;
        Title = title;
        Condition = condition;
        MinMark = minMark;
        MaxMark = maxMark;
    }
    
    public Guid Id { get; set; }
    public Guid ThemeId { get; set; }
    public string Title { get; set; }
    public string Condition { get; set; }
    public int MinMark { get; set; }
    public int MaxMark { get; set; }
    
    public void UpdateMainInfo(
        string title,
        string condition,
        int minMark,
        int maxMark)
    {
        Title = title;
        Condition = condition;
        MinMark = minMark;
        MaxMark = maxMark;
    }
    
    public static Result<Task, Error> Create(
        Guid themeId,
        string title,
        string condition,
        int attempsAmount,
        int minMark,
        int maxMark
    )
    {
        var task = new Task(
            themeId,
            title,
            condition,
            minMark,
            maxMark
        );
        
        return task;
    }
}