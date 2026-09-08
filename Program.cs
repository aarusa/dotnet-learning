using FirstApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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
    return students;
});

app.MapGet("/students/{id}", (int id) =>
{
    Student? student = students.Find(s => s.Id == id);

    if (student == null)
    {
        return Results.NotFound("Student not found.");
    }

    return Results.Ok(student);
});

app.MapPost("/students", (CreateStudentRequest request) =>
{
   int newId = students.Count + 1;

   Student student = new Student(newId, request.Name, request.Age);

   students.Add(student);

   return Results.Created($"/students/{student.Id}", student); 
});

app.MapPut("/students/{id}", (int id, UpdateStudentRequest request) =>
{
    Student? student = students.Find(s => s.Id == id);

    if(student == null)
    {
        return Results.NotFound("Student not found.");
    }

    student.Name = request.Name;
    student.Age = request.Age;

    return Results.Ok(student);
});

app.MapDelete("/students/{id}", (int id) =>
{
    Student? student = students.Find(s => s.Id == id);

    if(student == null)
    {
        return Results.NotFound("Student not found.");
    }

    students.Remove(student);
    
    return Results.NoContent();
});

app.Run();