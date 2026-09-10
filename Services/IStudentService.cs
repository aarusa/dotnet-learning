using FirstApi.Models;

namespace FirstApi.Services;

public interface IStudentService
{
    List<Student> GetAllStudents();
    Student? GetStudentById(int id);
    Student CreateStudent(string name, int age);
    Student? UpdateStudent(int id, string name, int age);
    bool DeleteStudent(int id);
}