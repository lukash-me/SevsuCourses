using CSharpFunctionalExtensions;
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
        DateTime dateSent)
    {
        Id = AnswerId.NewAnswerId();
        TaskId = taskId;
        StudentId = studentId;
        Mark = mark;
        ReplyText = replyText;
        AnswerText = answerText;
        DateSent = dateSent;
    }
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid StudentId { get; set; }
    public int Mark { get; set; }
    public string? ReplyText { get; set; }
    public string AnswerText { get; set; }
    public DateTime DateSent { get; set; }

    public void UpdateAnswerText(string newAnswerText)
    {
        AnswerText = newAnswerText;
    } 
    
    public void UpdateReplyAndMark(string newReply, int newMark)
    {
        ReplyText = newReply;
        Mark = newMark;
    } 
    
    public static Result<Answer, Error> Create(
        Guid taskId,
        Guid studentId,
        int mark,
        string? replyText,
        string answerText,
        DateTime dateSent
    )
    {
        var answer = new Answer(
            taskId,
            studentId,
            mark,
            replyText,
            answerText,
            dateSent
        );
        
        return answer;
    }
}