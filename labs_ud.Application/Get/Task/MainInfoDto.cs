namespace labs_ud.Application.Get.Task;

public record MainInfoDto(
    string Title,
    string Condition,
    int MinMark,
    int MaxMark
);