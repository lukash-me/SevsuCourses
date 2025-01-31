using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Share;

namespace labs_ud.Application.Update.Mentor;

public class UpdatePasswordService
{
    private readonly MentorRepository _mentorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePasswordService(MentorRepository mentorRepository, IUnitOfWork unitOfWork)
    {
        _mentorRepository = mentorRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdatePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var mentorResult = await _mentorRepository.GetById(request.UserId, cancellationToken);
        if (mentorResult.IsFailure)
            return mentorResult.Error;
        
        mentorResult.Value.UpdatePassword(request.Dto.Password);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.UserId;
    }
}