using CSharpFunctionalExtensions;
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
        Guid taskId,
        int mark,
        string answer
    )
    {
        var solution = new Solution(
            taskId,
            mark,
            answer
        );
        
        return solution;
    }
}