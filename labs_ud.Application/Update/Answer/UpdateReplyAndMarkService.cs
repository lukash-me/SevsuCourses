using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Answer;

public class UpdateReplyAndMarkService
{
    private readonly AnswerRepository _answerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateReplyAndMarkService(AnswerRepository answerRepository, IUnitOfWork unitOfWork)
    {
        _answerRepository = answerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateReplyAndMarkRequest request,
        CancellationToken cancellationToken)
    {
        var answerResult = await _answerRepository.GetById(request.AnswerId, cancellationToken);
        if (answerResult.IsFailure)
            return answerResult.Error;
        
        answerResult.Value.UpdateReplyAndMark(request.Dto.ReplyText, request.Dto.Mark);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.AnswerId;
    }
}

public record UpdateReplyAndMarkRequest(
    Guid AnswerId,
    UpdateReplyAndMarkDto Dto
    );
    
public record UpdateReplyAndMarkDto(
    string ReplyText,
    int Mark);