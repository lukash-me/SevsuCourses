using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Get.Student;

public class GetAllService
{
    private readonly StudentRepository _studentRepository;

    public GetAllService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<Result<List<AllStudentsResponse>, Error>> Handle(CancellationToken cancellationToken = default)
    {
        var studentResult = await _studentRepository.GetAll(cancellationToken);
        
        if (studentResult.IsFailure)
            return studentResult.Error;
        
        var students = studentResult.Value;
        
        var response = new List<AllStudentsResponse>();
        
        foreach (var student in students)
        {
            var studentId = student.Id;
            var fio = student.Fio;
            
            var result = new AllStudentsResponse(studentId, fio);
            
            response.Add(result);
        }

        response = response.OrderBy(r=>r.Fio).ToList();
        
        return response;
    }
}

public record AllStudentsResponse(
    Guid StudentId,
    string Fio
    );