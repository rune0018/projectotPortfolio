using ProjectorAPI.models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectorAPI.Data
{
    public class DbInitializer
    {
        public static void Initialize(ProjectorContext context)
        {
            context.Database.EnsureCreated();

            if (!context.projects.Any() && !context.users.Any())
            {
                context.users.Add(new models.User { Username = "admin", Password = Encrypt.sha512("localAdmin") });
                context.projects.Add(new models.Project("Test", "Something"));
            }
            
            //seed data if needed

            var students = new Student[]
            {
                new Student { Firstname = "Carson",   Lastname = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Student { Firstname = "Meredith", Lastname = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Arturo",   Lastname = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Firstname = "Gytis",    Lastname = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Yan",      Lastname = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Student { Firstname = "Peggy",    Lastname = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Student { Firstname = "Laura",    Lastname = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Student { Firstname = "Nino",     Lastname = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };
            if (!context.students.Any())
            {
                foreach (Student s in students)
                {
                    context.students.Add(s);
                }
            }
            
            context.SaveChanges();

            var instructors = new Instructor[]
            {
                new Instructor { FirstName = "Kim",     LastName = "Abercrombie",
                    HireDate = DateTime.Parse("1995-03-11") },
                new Instructor { FirstName = "Fadi",    LastName = "Fakhouri",
                    HireDate = DateTime.Parse("2002-07-06") },
                new Instructor { FirstName = "Roger",   LastName = "Harui",
                    HireDate = DateTime.Parse("1998-07-01") },
                new Instructor { FirstName = "Candace", LastName = "Kapoor",
                    HireDate = DateTime.Parse("2001-01-15") },
                new Instructor { FirstName = "Roger",   LastName = "Zheng",
                    HireDate = DateTime.Parse("2004-02-12") }
            };
            if(!context.instructors.Any())
            foreach (Instructor i in instructors)
            {
                context.instructors.Add(i);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department { Name = "English",     Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Instructor  = instructors.Single( i => i.LastName == "Abercrombie") },
                new Department { Name = "Mathematics", Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Instructor  = instructors.Single( i => i.LastName == "Fakhouri") },
                new Department { Name = "Engineering", Budget = 350000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Instructor  = instructors.Single( i => i.LastName == "Harui") },
                new Department { Name = "Economics",   Budget = 100000,
                    StartDate = DateTime.Parse("2007-09-01"),
                    Instructor  = instructors.Single( i => i.LastName == "Kapoor") }
            };
            if(!context.departments.Any())
            foreach (Department d in departments)
            {
                context.departments.Add(d);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course {ID = 1050, Title = "Chemistry",      Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Engineering").Id
                },
                new Course {ID = 4022, Title = "Microeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").Id
                },
                new Course {ID = 4041, Title = "Macroeconomics", Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "Economics").Id
                },
                new Course {ID = 1045, Title = "Calculus",       Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").Id
                },
                new Course {ID = 3141, Title = "Trigonometry",   Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "Mathematics").Id
                },
                new Course {ID = 2021, Title = "Composition",    Credits = 3,
                    DepartmentID = departments.Single( s => s.Name == "English").Id
                },
                new Course {ID = 2042, Title = "Literature",     Credits = 4,
                    DepartmentID = departments.Single( s => s.Name == "English").Id
                },
            };
            if(!context.courses.Any())
            foreach (Course c in courses)
            {
                context.courses.Add(c);
            }
            context.SaveChanges();

            var officeAssignments = new OfficeAssignment[]
            {
                new OfficeAssignment {
                    Instructor = instructors.Single( i => i.LastName == "Fakhouri"),
                    Locaton = "Smith 17" },
                new OfficeAssignment {
                    Instructor = instructors.Single( i => i.LastName == "Harui"),
                    Locaton = "Gowan 27" },
                new OfficeAssignment {
                    Instructor = instructors.Single( i => i.LastName == "Kapoor"),
                    Locaton = "Thompson 304" },
            };
            if(!context.officeAssignments.Any())
            foreach (OfficeAssignment o in officeAssignments)
            {
                context.officeAssignments.Add(o);
            }
            context.SaveChanges();

            var courseInstructors = new CourseAssignment[]
            {
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Calculus" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Composition" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
                new CourseAssignment {
                    CourseID = courses.Single(c => c.Title == "Literature" ).ID,
                    InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                    },
            };
            if(!context.officeAssignments.Any())
            foreach (CourseAssignment ci in courseInstructors)
            {
                context.courseAssignments.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alexander"),
                    Course = context.courses.Single(c => c.Title == "Chemistry" ),
                    Grade = Grade.A
                },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alexander"),
                    Course = context.courses.Single(c => c.Title == "Microeconomics" ),
                    Grade = Grade.C
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alexander"),
                    Course = context.courses.Single(c => c.Title == "Macroeconomics" ),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alonso"),
                    Course = context.courses.Single(c => c.Title == "Calculus" ),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alonso"),
                    Course = context.courses.Single(c => c.Title == "Trigonometry" ),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Alonso"),
                    Course = context.courses.Single(c => c.Title == "Composition" ),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Anand"),
                    Course = context.courses.Single(c => c.Title == "Chemistry" )
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Anand"),
                    Course = context.courses.Single(c => c.Title == "Microeconomics"),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Barzdukas"),
                    Course = context.courses.Single(c => c.Title == "Chemistry"),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Li"),
                    Course = context.courses.Single(c => c.Title == "Composition"),
                    Grade = Grade.B
                    },
                new Enrollment {
                    Student = context.students.Single(s => s.Lastname == "Justice"),
                    Course = context.courses.Single(c => c.Title == "Literature"),
                    Grade = Grade.B
                    }
            };
            if(!context.enrollments.Any())
            foreach (Enrollment e in enrollments)
            {
                context.enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
