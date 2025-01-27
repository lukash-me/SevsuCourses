using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Share;

namespace labs_ud.Application.Update.Mentor;

public class UpdatePhoneService
{
    private readonly MentorRepository _mentorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhoneService(MentorRepository mentorRepository, IUnitOfWork unitOfWork)
    {
        _mentorRepository = mentorRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdatePhoneRequest request,
        CancellationToken cancellationToken)
    {
        var mentorResult = await _mentorRepository.GetById(request.Id, cancellationToken);
        if (mentorResult.IsFailure)
            return mentorResult.Error;
        
        mentorResult.Value.UpdatePhone(
            request.Dto.Phone
        );
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.Id;
    }
}