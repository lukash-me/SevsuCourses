using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Group
{
    public Group(Guid courseId, Guid? mentorId, DateOnly start, DateOnly end, string status)
    {
        Id = GroupId.NewGroupId();
        CourseId = courseId;
        MentorId = mentorId;
        Start = start;
        End = end;
        Status = status;
    }

    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid? MentorId { get; set; }
    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }
    public string Status { get; set; }

    public void UpdateMentor(Guid mentorId)
    {
        MentorId = mentorId;
    }
    
    public static Result<Group, Error> Create(
        Guid courseId,
        Guid? mentorId,
        DateOnly start,
        DateOnly end,
        string status
    )
    {
        var group = new Group(
            courseId,
            mentorId,
            start,
            end,
            status
        );
        
        return group;
    }
}