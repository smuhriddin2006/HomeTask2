using Domain.Models;
using System.Data;
using Npgsql;
namespace Infrastructure.Services;

public class EnrollmentService : IEnrollMentService
{
            const string connectionString = @"Host=localhost;
                                      Database=EnrollmentsDB;
                                      Username=postgres;
                                      Password=umed008";
    public void DeleteEnrollment(int id)
    {
          try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string dsc = @"delete from Enrollments
                                where id = @id";

                NpgsqlCommand ctd = new NpgsqlCommand(dsc, connection);

                ctd.Parameters.AddWithValue("id", id);

                var delete = ctd.ExecuteNonQuery();
                System.Console.WriteLine($"Deleted succsesfully {delete}");
            }                                       
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Deleted failed {ex.Message}");
        }   
    }

    public void EnrollStudent(int studentId, int courseId)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string cti = @"Insert into Enrollments (StudentId, CourseId)
                                Values (@StudentId, @CourseId)";

                NpgsqlCommand ecc = new NpgsqlCommand(cti, connection);

                ecc.Parameters.AddWithValue("StudentId", studentId);
                ecc.Parameters.AddWithValue("CourseId", courseId);

                var insert = ecc.ExecuteNonQuery();
                System.Console.WriteLine("Inserted Succsefully");
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    public List<Enrollment> GetAllEnrollments()
    {
            try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gsc = "select * from Enrollments";
                
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(gsc, connection);
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var enrollments = dataset.Tables[0];

                var enrollmentsesLit = new List<Enrollment>();

                foreach (DataRow row in enrollments.Rows)
                {
                    enrollmentsesLit.Add(new Enrollment
                    {
                        Id = row.Field<int>("Id"),
                        StudentId = row.Field<int>("StudentId"),
                        CourseId = row.Field<int>("CourseId"),
                        IsPaid = row.Field<bool>("IsPaid")
                    });
                    
                }
                return enrollmentsesLit;
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Failed {ex.Message}");
        }
        return null;    }

}
