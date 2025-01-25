using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Solution;

public class GetAnswerService
{
    private readonly SolutionRepository _solutionRepository;

    public GetAnswerService(SolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }

    public async Task<Result<List<string>, Error>> Handle(TaskId taskId, CancellationToken cancellationToken = default)
    {
        var solutionsResult = await _solutionRepository.GetByTaskId(taskId, cancellationToken);
        var solutions = solutionsResult.Value;
        var response = new List<string>();
        
        foreach (var solution in solutions)
        {
            response.Add(solution.Answer);
        }

        return response;
    }
}