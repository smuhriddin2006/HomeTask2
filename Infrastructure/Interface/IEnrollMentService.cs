// namespace Infrastructure.Interface;
namespace Domain.Models;
public interface IEnrollMentService
{
    void EnrollStudent(int studentId, int courseId);
    List<Enrollment> GetAllEnrollments();
    void DeleteEnrollment(int id);
}
