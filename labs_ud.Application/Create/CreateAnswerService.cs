using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using labs_ud.Application.Entities;
using labs_ud.Application.Errors;
using labs_ud.Application.Repositories;

namespace labs_ud.Application.Create;

public class CreateAnswerService
{
    private readonly AnswerRepository _answerRepository;
    private readonly TaskRepository _taskRepository;
    private readonly StudentRepository _studentRepository;

    public CreateAnswerService(AnswerRepository answerRepository, TaskRepository taskRepository, StudentRepository studentRepository)
    {
        _answerRepository = answerRepository;
        _taskRepository = taskRepository;
        _studentRepository = studentRepository;
    }
    
    public async Task<Result<Guid, Error>> Handle(CreateAnswerRequest request, CancellationToken cancellationToken)
    {
        var taskId = request.TaskId;
        var studentId = request.StudentId;
        int mark = 0;
        string? replyText = null;
        var answerText = request.AnswerText;
        var dateSent = DateTime.UtcNow;
        DateTime? dateReplied = null;

        if (string.IsNullOrWhiteSpace(taskId))
        {
            return Errors.Errors.General.ValueIsRequired("task id");
        }
        
        if (Regex.IsMatch(taskId, Constants.Regexes.GUID_REGEX) == false)
            return Errors.Errors.General.ValueIsInvalid("task id");

        var taskResult = await _taskRepository.GetById(Guid.Parse(taskId));
        if (taskResult.IsFailure)
        {
            return Errors.Errors.General.NotFound(Guid.Parse(taskId));
        }
        
        if (string.IsNullOrWhiteSpace(studentId))
        {
            return Errors.Errors.General.ValueIsRequired("student id");
        }
        
        if (Regex.IsMatch(studentId, Constants.Regexes.GUID_REGEX) == false)
            return Errors.Errors.General.ValueIsInvalid("student id");
        
        var studentResult = await _studentRepository.GetById(Guid.Parse(studentId));
        if (studentResult.IsFailure)
        {
            return Errors.Errors.General.NotFound(Guid.Parse(studentId));
        }

        var answerResult = Answer.Create(
            taskId,
            studentId,
            mark,
            replyText,
            answerText,
            dateSent,
            dateReplied
        );

        if (answerResult.IsFailure)
            return answerResult.Error;
        
        await _answerRepository.Add(answerResult.Value, cancellationToken);

        return answerResult.Value.Id;
    }
}