using labs_ud.Application.Errors;

namespace labs_ud.Response;

public record Envelope
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
    public Envelope(object? result, ErrorList? errors)
    {
        Result = result;
        Errors = errors;
        TimeGenerated = DateTime.Now;
    }
    public object? Result { get; }
    public ErrorList? Errors { get; }
    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) => 
        new Envelope(result, null);
    public static Envelope Error(ErrorList errors) => 
        new Envelope(null, errors);
}