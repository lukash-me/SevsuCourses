using CSharpFunctionalExtensions;
using labs_ud.Application.DataBase;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Share;

namespace labs_ud.Application.Update.Student;

public class UpdatePhoneService
{
    private readonly StudentRepository _studentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePhoneService(StudentRepository studentRepository, IUnitOfWork unitOfWork)
    {
        _studentRepository = studentRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        UpdatePhoneRequest request,
        CancellationToken cancellationToken)
    {
        var studentResult = await _studentRepository.GetById(request.Id, cancellationToken);
        if (studentResult.IsFailure)
            return studentResult.Error;
        
        studentResult.Value.UpdatePhone(
            request.Dto.Phone
        );
        
        await _unitOfWork.SaveChanges(cancellationToken);

        return request.Id;
    }
}