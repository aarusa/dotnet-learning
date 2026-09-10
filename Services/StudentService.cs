using FirstApi.Data;
using FirstApi.Models;

namespace FirstApi.Services;

public class StudentService : IStudentService
{
    private readonly ApplicationDbContext db;
    public StudentService(ApplicationDbContext db)
    {
        this.db = db;
    }

    public List<Student> GetAllStudents()
    {
        return db.Students.ToList();
    }

    public Student? GetStudentById(int id)
    {
        return db.Students.Find(id);
    }

    public Student CreateStudent(string name, int age)
    {
        Student student = new Student
        {
            Name = name,
            Age = age
        };

        db.Students.Add(student);
        db.SaveChanges();

        return student;
    }

    public Student? UpdateStudent(int id, string name, int age)
    {
        Student? student = db.Students.Find(id);

        if(student == null)
        {
            return null;
        }

        student.Name = name;
        student.Age = age;

        db.SaveChanges();

        return student;
    }

    public bool DeleteStudent(int id)
    {
        Student? student = db.Students.Find(id);

        if (student == null)
        {
            return false;
        }

        db.Students.Remove(student);
        db.SaveChanges();

        return true;
    }
}
