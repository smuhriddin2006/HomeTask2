

using Domain.Models;
using Infrastructure.Services;

StudentService studentService = new StudentService();
Student student = new()
{
    FullName = "Umed Safarov",
    Email = "umed@gmail.com",
    BirthDate = new DateOnly(2006, 10, 18)
};

Student student2 = new()
{
    FullName = "Voris Safarov",
    Email = "voris@gmail.com",
    BirthDate = new DateOnly(2005, 10, 18)
};

studentService.AddStudent(student2);

studentService.AddStudent(student);


Student student3 = new()
{
    FullName = "Rofe Nzoimov",
    Email = "rofe@gmail.com",
    BirthDate = new DateOnly(2006, 02, 1)
};

Student new_Student = new Student()
{
    Id = 5,
    FullName = "Nozimov Rofe",
    Email = "rofenozimov90@gmail.com",
    BirthDate = new DateOnly(2006, 02, 1)
};

studentService.AddStudent(student3);

studentService.DeleteStudent(1);

studentService.UpdateStudent(new_Student);

var s = studentService.GetAllStudents();

foreach(var d in s)
{
    System.Console.WriteLine($"{d.Id} {d.FullName} {d.Email} {d.BirthDate}");
}

Course course = new()
{
    Title = "C#",
    Price = 20000,
    DurationWeeks = 6
};


Course new_course2 = new()
{
    Id = 3,
    Title = "C#",
    Price = 40000,
    DurationWeeks = 6
};


Course new_course = new()
{
    Title = "C#",
    Price = 30000,
    DurationWeeks = 4
};


Course new_course3 = new()
{
    Id = 2,
    Title = "C++",
    Price = 500000,
    DurationWeeks = 6
};


CourseService courseService = new();


courseService.AddCourse(course);
// courseService.DeleteCourse(1);

courseService.UpdateCourse(new_course);

courseService.UpdateCourse(new_course3);

// var s = courseService.GetAllCourses();

// foreach (var d in s)
// {
//     System.Console.WriteLine($"{d.Id} {d.Title} {d.Price} {d.DurationWeeks}");
// }

// var d = courseService.GetCourseById(3);

// System.Console.WriteLine($"{d.Id} {d.Title} {d.Price} {d.DurationWeeks}");


EnrollmentService enrollmentService = new EnrollmentService();


// enrollmentService.EnrollStudent(5,2);
// enrollmentService.EnrollStudent(6,2);


// enrollmentService.EnrollStudent(5,2);

// var s = studentService.GetAllStudents();
// foreach(var d in s){System.Console.WriteLine(d.Id + " " + d.FullName + " " + d.Email + " " + d.BirthDate);}

// var da = courseService.GetAllCourses();

// foreach(var item in da)
// {
//     System.Console.WriteLine(item.Id + item.Title + item.Price + item.DurationWeeks);
//}

// var d = enrollmentService.GetAllEnrollments();

// foreach(var s in d)
// {
//     System.Console.WriteLine($"{s.Id} {s.CourseId} {s.StudentId} {s.IsPaid}");
// }

// var c = courseService.GetExensiveCourse();

// System.Console.WriteLine(c.Id + " " + " " + c.Title + " " + " " + c.Price + c.DurationWeeks );



var g = courseService.GetAveragePriceCourse();
System.Console.WriteLine(g);