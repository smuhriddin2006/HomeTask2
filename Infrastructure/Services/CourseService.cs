using Domain.Models;
using Npgsql;
using System.Data;
namespace Infrastructure.Services;

public class CourseService : ICourseService
{
        const string connectionString = @"Host=localhost;
                                      Database=EnrollmentsDB;
                                      Username=postgres;
                                      Password=umed008";

    public void AddCourse(Course course)
    {
        try
        {
        using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            string asc = @"Insert into Courses (Title, Price, DurationWeeks)
                           Values (@Title, @Price, @DurationWeeks)";
            
            NpgsqlCommand cta = new NpgsqlCommand(asc, connection);
            
            cta.Parameters.AddWithValue("Title", course.Title);
            cta.Parameters.AddWithValue("Price", course.Price);
            cta.Parameters.AddWithValue("DurationWeeks", course.DurationWeeks);

            var insert = cta.ExecuteNonQuery();
            System.Console.WriteLine($"Inserted Succsesfully {insert}");
        }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Inserted Falailed {ex.Message}");
        }    
        }

    public void DeleteCourse(int id)
    {
            try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string dsc = @"delete from Courses
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
        }    }

    public List<Course> GetAllCourses()
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gsc = "select * from Courses";
                
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(gsc, connection);
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var courses = dataset.Tables[0];

                var coursesLit = new List<Course>();

                foreach (DataRow row in courses.Rows)
                {
                    coursesLit.Add(new Course
                    {
                        Id = row.Field<int>("Id"),
                        Title = row.Field<string>("Title"),
                        Price = row.Field<decimal>("Price"),
                        DurationWeeks = row.Field<int>("DurationWeeks")
                    });
                    
                }
                return coursesLit;
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Failed {ex.Message}");
        }
        return null;    }

    public Course GetCourseById(int id)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gsc = @"select * from Courses
                                where id = @id";
                
                var ctg = new NpgsqlCommand(gsc, connection);
                ctg.Parameters.AddWithValue("id", id);

                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(gsc, connection);
            
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var courses = dataset.Tables[0];

                foreach (DataRow row in courses.Rows)
                {
                    var course = new Course
                    {
                        Id = row.Field<int>("Id"),
                        Title = row.Field<string>("Title"),
                        Price = row.Field<decimal>("Price"),
                        DurationWeeks = row.Field<int>("DurationWeeks")
                    };
                    return course;
                }
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine($"Failed {ex.Message}");
        }
        return null;    }

    public void UpdateCourse(Course course)
    {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string usc = @"Update Courses Set
                                Title = @Title,
                                Price = @Price,
                                DurationWeeks = @DurationWeeks
                                where id = @id";

                NpgsqlCommand ctu = new NpgsqlCommand(usc, connection);

                ctu.Parameters.AddWithValue("id", course.Id);
                ctu.Parameters.AddWithValue("Title", course.Title);
                ctu.Parameters.AddWithValue("Price", course.Price);
                ctu.Parameters.AddWithValue("DurationWeeks", course.DurationWeeks);

                var update = ctu.ExecuteNonQuery();

            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }    
        }

        public Course GetExensiveCourse()
        {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gec = @"select id, title, Price, DurationWeeks
                               from Courses
                               where Price = (
                               select max(price) from Courses
                                )";   
                var adapter = new NpgsqlDataAdapter(gec, connection);
                var dataset = new DataSet();

                adapter.Fill(dataset);

                var courses = dataset.Tables[0];

                foreach (DataRow row in courses.Rows)
                {
                    Course course = new()
                    {
                        Id = row.Field<int>("Id"),
                        Title = row.Field<string>("Title"),
                        Price = row.Field<decimal>("Price"),
                        DurationWeeks = row.Field<int>("DurationWeeks")
                    };
                    return course; 
                }       
                           
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        return null;
        }

         public decimal GetAveragePriceCourse()
        {
        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string gec = @"select avg(Price) from Courses";   

                var cgp = new NpgsqlCommand(gec, connection);

                var get = cgp.ExecuteScalar();
                return Convert.ToDecimal(get);

                           
            }
            
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
        return 0;
        }

}
