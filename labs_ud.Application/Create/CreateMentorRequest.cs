namespace labs_ud.Application.Create;

public record CreateMentorRequest
(
    string Fio,
    string Phone,
    string Email,
    string Education,
    string Photo,
    string Description,
    string Login,
    string Password);