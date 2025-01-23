using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

/// <summary>
/// Получение id группы, в которой обучается студент по его id
/// </summary>
public class GetGroupByStudentIdService
{
    private readonly StudentRepository _studentRepository;

    public GetGroupByStudentIdService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<Guid, Error>> Handle(StudentId studentId, CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetById(studentId, cancellationToken);

        return studentResult.Value.GroupId;
    }
}
