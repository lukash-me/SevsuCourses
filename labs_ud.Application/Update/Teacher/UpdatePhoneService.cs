using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Share;

namespace labs_ud.Application.Update.Teacher;

public class UpdatePhoneService
{
    private readonly TeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhoneService(TeacherRepository teacherRepository, IUnitOfWork unitOfWork)
    {
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdatePhoneRequest request,
        CancellationToken cancellationToken)
    {
        var teacherResult = await _teacherRepository.GetById(request.Id, cancellationToken);
        if (teacherResult.IsFailure)
            return teacherResult.Error;
        
        teacherResult.Value.UpdatePhone(
            request.Dto.Phone);
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.Id;
    }
}