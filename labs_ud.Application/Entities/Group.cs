using System.Text.RegularExpressions;
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

    public Result<string, Error> UpdateMentor(string? mentorId)
    {
        if (mentorId != null && Regex.IsMatch(mentorId, Constants.Regexes.GUID_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("mentorId");
        }
        
        MentorId = Guid.Parse(mentorId);

        return "Success";
    }
    
    public static Result<Group, Error> Create(
        string courseId,
        string? mentorId,
        DateOnly start,
        DateOnly end,
        string status
    )
    {
        if (Regex.IsMatch(courseId, Constants.Regexes.GUID_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("courseId");
        }

        if (mentorId != null && Regex.IsMatch(mentorId, Constants.Regexes.GUID_REGEX) == false)
        {
            return Errors.Errors.General.ValueIsInvalid("mentorId");
        }
        
        if (string.IsNullOrWhiteSpace(status))
        {
            return Errors.Errors.General.ValueIsRequired("status");
        }
        
        if (status.Length > Constants.Values.STATUS_LENGTH)
        {
            return Errors.Errors.General.ValueIsInvalid("status");
        }
        
        var group = new Group(
            Guid.Parse(courseId),
            Guid.Parse(mentorId),
            start,
            end,
            status
        );
        
        return group;
    }
}