using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Get.Teacher;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Update.Teacher;

public class UpdateMainInfoService
{
    private readonly TeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMainInfoService(TeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var teacherResult = await _teacherRepository.GetById(request.TeacherId, cancellationToken);
        if (teacherResult.IsFailure)
            return teacherResult.Error;
        
        teacherResult.Value.UpdateMainInfo(
            request.Dto.Fio,
            request.Dto.Experience,
            request.Dto.Email,
            request.Dto.Education,
            request.Dto.Description,
            request.Dto.Photo);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.TeacherId;
    }
}

public record UpdateMainInfoRequest(
    Guid TeacherId,
    MainInfoDto Dto
    );