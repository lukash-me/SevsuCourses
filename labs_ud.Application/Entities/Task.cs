using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
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
    
    public Result<string, Error> UpdateMainInfo(
        string title,
        string condition,
        int minMark,
        int maxMark)
    {
        var checkMainInfoResult = Block.CheckMainInfo(title, condition);
        if (checkMainInfoResult.IsFailure)
        {
            return checkMainInfoResult.Error;
        }
        
        if (int.IsNegative(minMark))
        {
            return Errors.Errors.General.ValueIsInvalid("min mark");
        }
        
        if (int.IsNegative(maxMark) || minMark > maxMark)
        {
            return Errors.Errors.General.ValueIsInvalid("max mark");
        }
        
        Title = title;
        Condition = condition;
        MinMark = minMark;
        MaxMark = maxMark;

        return "Success";
    }
    
    public static Result<Task, Error> Create(
        string themeId,
        string title,
        string condition,
        int minMark,
        int maxMark
    )
    {
        var checkGuidResult = Ids.CheckGuid(themeId, "theme id");
        if (checkGuidResult.IsFailure)
        {
            return checkGuidResult.Error;
        }
        
        var checkMainInfoResult = Block.CheckMainInfo(title, condition);
        if (checkMainInfoResult.IsFailure)
        {
            return checkMainInfoResult.Error;
        }
        
        if (int.IsNegative(minMark))
        {
            return Errors.Errors.General.ValueIsInvalid("min mark");
        }
        
        if (int.IsNegative(maxMark) || minMark > maxMark)
        {
            return Errors.Errors.General.ValueIsInvalid("max mark");
        }
        
        var task = new Task(
            Guid.Parse(themeId),
            title,
            condition,
            minMark,
            maxMark
        );
        
        return task;
    }
}