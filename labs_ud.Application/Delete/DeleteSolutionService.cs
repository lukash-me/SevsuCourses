using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteSolutionService
{
    private readonly SolutionRepository _solutionRepository;

    public DeleteSolutionService(SolutionRepository solutionRepository)
    {
        _solutionRepository = solutionRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteSolutionRequest request, 
        CancellationToken cancellationToken = default)
    {
        var solutionResult = await _solutionRepository.GetById(request.SolutionId, cancellationToken);
        if (solutionResult.IsFailure)
            return solutionResult.Error;

        _solutionRepository.Delete(solutionResult.Value, cancellationToken);

        return solutionResult.Value.Id;
    }
}