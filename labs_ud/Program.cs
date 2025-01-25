using labs_ud.Application.Create;
using labs_ud.Application.DataBase;
using labs_ud.Application.Delete;
using labs_ud.Application.Get.Answer;
using labs_ud.Application.Get.Course;
using labs_ud.Application.Get.Group;
using labs_ud.Application.Get.Mentor;
using labs_ud.Application.Get.Solution;
using labs_ud.Application.Get.Student;
using labs_ud.Application.Get.Tasks;
using labs_ud.Application.Get.Teacher;
using labs_ud.Application.Get.Themes;
using labs_ud.Application.Repositories;
using labs_ud.Application.Update.Answer;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
    options.AddPolicy("AllowVueApp", builder =>
        builder.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services
    .AddScoped<UpdateReplyAndMarkService>()
    .AddScoped<GetAnswerService>()
    .AddScoped<GetTitlesByTeacherIdService>()
    .AddScoped<labs_ud.Application.Get.Teacher.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Mentor.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Student.GetPhoneService>()
    .AddScoped<labs_ud.Application.Get.Teacher.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Mentor.GetMainInfoService>()
    .AddScoped<labs_ud.Application.Get.Student.GetMainInfoService>()
    .AddScoped<GetStudentsFioStatusByGroupIdService>()
    .AddScoped<GetMentorFioByIdService>()
    .AddScoped<GetTeacherFioByIdService>()
    .AddScoped<GetTitleAndTeacherByIdService>()
    .AddScoped<GetGroupByIdService>()
    .AddScoped<GetGroupByStudentIdService>()
    .AddScoped<UpdateAnswerTextService>()
    .AddScoped<GetAnswerByTaskIdStudentIdService>()
    .AddScoped<GetMentorByLoginPasswordService>()
    .AddScoped<GetTeacherByLoginPasswordService>()
    .AddScoped<GetStudentByLoginPasswordService>()
    .AddScoped<GetThemeByIdService>()
    .AddScoped<GetTaskByIdService>()
    .AddScoped<GetTasksByThemeService>()
    .AddScoped<GetCourseByIdService>()
    .AddScoped<GetThemesByCourseService>()
    .AddScoped<GetAllCoursesService>()
    .AddScoped<CreateAnswerService>()
    .AddScoped<DeleteAnswerService>()
    .AddScoped<CreateStudentService>()
    .AddScoped<DeleteStudentService>()
    .AddScoped<CreateGroupService>()
    .AddScoped<DeleteGroupService>()
    .AddScoped<CreateSolutionService>()
    .AddScoped<DeleteSolutionService>()
    .AddScoped<CreateTaskService>()
    .AddScoped<DeleteTaskService>()
    .AddScoped<CreateCourseService>()
    .AddScoped<DeleteCourseService>()
    .AddScoped<CreateTeacherService>()
    .AddScoped<DeleteTeacherService>()
    .AddScoped<CreateMentorService>()
    .AddScoped<DeleteMentorService>()
    .AddScoped<CreateThemeService>()
    .AddScoped<DeleteThemeService>()
    .AddScoped<ApplicationDbContext>()
    .AddScoped<CourseRepository>()
    .AddScoped<TeacherRepository>()
    .AddScoped<MentorRepository>()
    .AddScoped<ThemeRepository>()
    .AddScoped<TaskRepository>()
    .AddScoped<SolutionRepository>()
    .AddScoped<GroupRepository>()
    .AddScoped<StudentRepository>()
    .AddScoped<AnswerRepository>()
    .AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors("AllowAllOrigins");
    app.UseCors("AllowVueApp");
    app.UseRouting();
}

app.UseStaticFiles();
app.MapControllers();

app.Run();