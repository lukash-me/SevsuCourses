using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class StudentGroup
{
    public StudentGroup(
        Guid studentId,
        Guid groupId
       )
    {
        Id = StudentGroupId.NewStudentGroupId();
        StudentId = studentId;
        GroupId = groupId;
    }

    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid GroupId { get; set; }
    
    public static Result<StudentGroup, Error> Create(Guid studentId, Guid groupId)
    {
        var studentGroup = new StudentGroup(studentId, groupId);
        
        return studentGroup;
    }
}