using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Solution
{
    public Solution(Guid taskId, int mark, string answer)
    {
        Id = SolutionId.NewSolutionId();
        TaskId = taskId;
        Mark = mark;
        Answer = answer;
    }

    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public int Mark { get; set; }
    public string Answer { get; set; }
    
    public static Result<Solution, Error> Create(
        string taskId,
        int mark,
        string answer
    )
    {
        var checkGuidResult = Ids.CheckGuid(taskId, "task id");
        if (checkGuidResult.IsFailure)
        {
            return checkGuidResult.Error;
        }

        //получение мин макс оценки должно быть через запрос к answer
        
        if (int.IsNegative(mark))
        {
            return Errors.Errors.General.ValueIsInvalid("mark");
        }
        
        if (string.IsNullOrWhiteSpace(answer))
        {
            return Errors.Errors.General.ValueIsRequired("answer");
        }
        
        if (answer.Length > Constants.Values.HUGE_TEXT)
        {
            return Errors.Errors.General.InvalidLength("answer");
        }
        
        var solution = new Solution(
            Guid.Parse(taskId),
            mark,
            answer
        );
        
        return solution;
    }
}