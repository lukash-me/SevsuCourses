using CSharpFunctionalExtensions;
using labs_ud.Application.Errors;
using labs_ud.Application.IDs;

namespace labs_ud.Application.Entities;

public class Answer
{
    public Answer(
        Guid taskId,
        Guid studentId,
        int attempNumber,
        int mark,
        string replyText,
        string answerText)
    {
        Id = AnswerId.NewAnswerId();
        TaskId = taskId;
        StudentId = studentId;
        AttempNumber = attempNumber;
        Mark = mark;
        ReplyText = replyText;
        AnswerText = answerText;
    }
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public Guid StudentId { get; set; }
    public int AttempNumber { get; set; }
    public int Mark { get; set; }
    public string ReplyText { get; set; }
    public string AnswerText { get; set; }

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
        int attempNumber,
        int mark,
        string replyText,
        string answerText
    )
    {
        var answer = new Answer(
            taskId,
            studentId,
            attempNumber,
            mark,
            replyText,
            answerText
        );
        
        return answer;
    }
}