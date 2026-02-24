using Domain.Models;
namespace Infrastructure.Services;

using System.Data;

using Npgsql;
public class StudentService : IStudentService
{
    const string connectionString = @"Host=localhost;
                                      Database=EnrollmentsDB;
                                      Username=postgres;
                                      Password=umed008";
    public void AddStudent(Student student)
    {
        try{
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string asc = @"Insert into Students (FullName, Email, BirthDate)
                           Values (@FullName, @Email, @BirthDate)";
            
            NpgsqlCommand cta = new NpgsqlCommand(asc, connection);
            
            cta.Parameters.AddWithValue("FullName", student.FullName);
            cta.Parameters.AddWithValue("Email", student.Email);
            cta.Parameters.AddWithValue("BirthDate", student.BirthDate);

            var insert = cta.ExecuteNonQuery();
            System.Console.WriteLine($"Inserted Succsesfully {insert}");
        }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Inserted Falailed {ex.Message}");
        }
    }

    public void DeleteStudent(int id)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string dsc = @"delete from Students
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

    public List<Student> GetAllStudents()
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gsc = "select * from Students";
                
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(gsc, connection);
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var students = dataset.Tables[0];

                var studentList = new List<Student>();

                foreach (DataRow row in students.Rows)
                {
                    studentList.Add(new Student
                    {
                        Id = row.Field<int>("Id"),
                        FullName = row.Field<string>("FullName"),
                        Email = row.Field<string>("Email"),
                        BirthDate = row.Field<DateOnly>("BirthDate")
                    });
                    
                }
                return studentList;
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Failes {ex.Message}");
        }
        return null;
    }

    public Student GetStudentById(int id)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gsc = @"select * from Students
                                where id = @id";
                
                var ctg = new NpgsqlCommand(gsc, connection);
                ctg.Parameters.AddWithValue("id", id);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(gsc, connection);
            
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var students = dataset.Tables[0];

                foreach (DataRow row in students.Rows)
                {
                    var student = new Student
                    {
                        Id = row.Field<int>("Id"),
                        FullName = row.Field<string>("FullName"),
                        Email = row.Field<string>("Email"),
                        BirthDate = row.Field<DateOnly>("BirthDate")
                    };
                    return student;
                }
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Failes {ex.Message}");
        }
        return null;
    }
    public void UpdateStudent(Student student)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string usc = @"Update Students Set
                                FullName = @FullName,
                                Email = @Email,
                                BirthDate = @BirthDate";

                NpgsqlCommand ctu = new NpgsqlCommand(usc, connection);

                ctu.Parameters.AddWithValue("FullName", student.FullName);
                ctu.Parameters.AddWithValue("Email", student.Email);
                ctu.Parameters.AddWithValue("BirthDate", student.BirthDate);

                var update = ctu.ExecuteNonQuery();

            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }




    }


    
