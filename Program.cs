using FirstApi.Models;
using FirstApi.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

StudentService studentService = new StudentService();

List<Student> students = new List<Student>
{
    new Student(1, "Arusha", 29),
    new Student(2, "Maya", 25),
    new Student(3, "Ashish", 31)
};

app.MapGet("/", () => "My first DOTNET API!");

app.MapGet("/about", () => "This is about page.");

app.MapGet("/students", () =>
{
    return studentService.GetAllStudents();
});

app.MapGet("/students/{id}", (int id) =>
{
    Student? student = studentService.GetStudentById(id);

    if (student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapPost("/students", (CreateStudentRequest request) =>
{
   Student student = studentService.CreateStudent(
        request.Name,
        request.Age
   );

   return Results.Created($"/students/{student.Id}", student); 
});

app.MapPut("/students/{id}", (int id, UpdateStudentRequest request) =>
{
    Student? student = studentService.UpdateStudent(
        id,
        request.Name,
        request.Age
    );

    if(student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapDelete("/students/{id}", (int id) =>
{
    bool deleted = studentService.DeleteStudent(id);

    if(deleted)
    {
        return Results.NotFound("Student not found.");
    }
    
    return Results.NoContent();
});

app.Run();