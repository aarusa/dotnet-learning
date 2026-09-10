using FirstApi.Models;

namespace FirstApi.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> students = new()
    {
        new Student(1, "Arusha", 29),
        new Student(2, "Maya", 25),
        new Student(3, "Irisha", 26)
    };

    public List<Student> GetAllStudents()
    {
        return students;
    }

    public Student? GetStudentById(int id)
    {
        Student? student = students.Find(s => s.Id == id);

        return student;
    }

    public Student CreateStudent(string name, int age)
    {
        int newId = students.Count + 1;

        Student student = new Student(newId, name, age);

        students.Add(student);

        return student;
    }

    public Student? UpdateStudent(int id, string name, int age)
    {
        Student? student = students.Find(s => s.Id == id);

        if(student == null)
        {
            return null;
        }

        student.Name = name;
        student.Age = age;

        return student;
    }

    public bool DeleteStudent(int id)
    {
        Student? student = students.Find(s => s.Id == id);

        if (student == null)
        {
            return false;
        }

        students.Remove(student);

        return true;
    }
}
