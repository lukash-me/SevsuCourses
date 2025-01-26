using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Mentor;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Mentor;

public class UpdateMainInfoService
{
    private readonly MentorRepository _mentorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(MentorRepository mentorRepository, IUnitOfWork unitOfWork)
    {
        _mentorRepository = mentorRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var mentorResult = await _mentorRepository.GetById(request.MentorId, cancellationToken);
        if (mentorResult.IsFailure)
            return mentorResult.Error;
        
        mentorResult.Value.UpdateMainInfo(
            request.Dto.Fio,
            request.Dto.Email,
            request.Dto.Education,
            request.Dto.Photo,
            request.Dto.Description
        );
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.MentorId;
    }
}

public record UpdateMainInfoRequest(
    Guid MentorId,
    MainInfoDto Dto
);