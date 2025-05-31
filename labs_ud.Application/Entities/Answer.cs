using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities.Validation;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Answer
{
    public Answer(
        Guid taskId,
        Guid studentId,
        int mark,
        string? replyText,
        string answerText,
        DateTime dateSent,
        DateTime? dateReplied)
    {
        Id = AnswerId.NewAnswerId();
        TaskId = taskId;
        StudentId = studentId;
        Mark = mark;
        ReplyText = replyText;
        AnswerText = answerText;
        DateSent = dateSent;
        DateReplied = dateReplied;
    }
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid StudentId { get; set; }
    public int Mark { get; set; }
    public string? ReplyText { get; set; }
    public string AnswerText { get; set; }
    public DateTime DateSent { get; set; }
    public DateTime? DateReplied { get; set; }

    public void UpdateAnswerText(string newAnswerText)
    {
        AnswerText = newAnswerText;
    } 
    
    public void UpdateReplyAndMark(string newReply, int newMark)
    {
        ReplyText = newReply;
        Mark = newMark;
        DateReplied = DateTime.UtcNow;
    } 
    
    public static Result<Answer, Error> Create(
        string taskId,
        string studentId,
        int mark,
        string? replyText,
        string answerText,
        DateTime dateSent,
        DateTime? dateReplied
    )
    {
        var checkGuidResult = Ids.CheckGuid(studentId, "student id");
        if (checkGuidResult.IsFailure)
        {
            return checkGuidResult.Error;
        }
        
        checkGuidResult = Ids.CheckGuid(taskId, "task id");
        if (checkGuidResult.IsFailure)
        {
            return checkGuidResult.Error;
        }
        
        if (int.IsNegative(mark))
        {
            return Errors.Errors.General.ValueIsInvalid("mark");
        }

        if (string.IsNullOrWhiteSpace(answerText))
        {
            return Errors.Errors.General.ValueIsRequired("answerText");
        }
        
        var answer = new Answer(
            Guid.Parse(taskId),
            Guid.Parse(studentId),
            mark,
            replyText,
            answerText,
            dateSent,
            dateReplied
        );
        
        return answer;
    }
}