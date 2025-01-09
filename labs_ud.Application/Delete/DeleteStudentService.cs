using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Delete;

public class DeleteStudentService
{
    private readonly StudentRepository _studentRepository;

    public DeleteStudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(
        DeleteStudentRequest request, 
        CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetById(request.StudentId, cancellationToken);
        if (studentResult.IsFailure)
            return studentResult.Error;

        _studentRepository.Delete(studentResult.Value, cancellationToken);

        return studentResult.Value.Id;
    }
}