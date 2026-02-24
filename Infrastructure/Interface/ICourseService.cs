// namespace Infrastructure.Interface;
namespace Domain.Models;
public interface ICourseService
{
    void AddCourse(Course course);
    List<Course> GetAllCourses();
    Course GetCourseById(int id);
    void UpdateCourse(Course course);
    void DeleteCourse(int id);
}
