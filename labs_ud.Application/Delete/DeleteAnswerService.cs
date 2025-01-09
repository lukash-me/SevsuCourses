using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteAnswerService
{
    private readonly AnswerRepository _answerRepository;

    public DeleteAnswerService(AnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteAnswerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var answerResult = await _answerRepository.GetById(request.AnswerId, cancellationToken);
        if (answerResult.IsFailure)
            return answerResult.Error;

        _answerRepository.Delete(answerResult.Value, cancellationToken);

        return answerResult.Value.Id;
    }
}