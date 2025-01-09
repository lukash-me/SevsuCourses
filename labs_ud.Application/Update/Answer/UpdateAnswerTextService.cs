using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Answer;

public class UpdateAnswerTextService
{
    private readonly AnswerRepository _answerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAnswerTextService(AnswerRepository answerRepository, IUnitOfWork unitOfWork)
    {
        _answerRepository = answerRepository;
        _unitOfWork = unitOfWork;
    }


    public async Task<Result<Guid, Error>> Handle(
        UpdateAnswerTextRequest request,
        CancellationToken cancellationToken)
    {
        var answerResult = await _answerRepository.GetById(request.AnswerId, cancellationToken);
        if (answerResult.IsFailure)
            return answerResult.Error;
        
        answerResult.Value.UpdateAnswerText(request.Dto.Text);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.AnswerId;
    }
}

public record UpdateAnswerTextRequest(
    Guid AnswerId,
    UpdateAnswerTextDto Dto);

public record UpdateAnswerTextDto(
    string Text);
    