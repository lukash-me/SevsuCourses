using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateSolutionService
{
    private readonly SolutionRepository _solutionRepository;

    public CreateSolutionService(SolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateSolutionRequest request, CancellationToken cancellationToken)
    {
        var taskId = request.TaskId;

        var mark = request.Mark;
        
        var answer  = request.Answer;

        var solutionResult = Solution.Create(
            taskId,
            mark,
            answer
        );

        if (solutionResult.IsFailure)
            return solutionResult.Error;
        
        await _solutionRepository.Add(solutionResult.Value, cancellationToken);

        return solutionResult.Value.Id;
    }
}