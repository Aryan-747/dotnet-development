using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseRegistrationSystem
{
    // 1. BASE INTERFACES
    public interface IStudent
    {
        int StudentId { get; }
        string Name { get; }
        int Semester { get; }
    }

    public interface ICourse
    {
        string CourseCode { get; }
        string Title { get; }
        int MaxCapacity { get; }
        int Credits { get; }
    }

    // 2. GENERIC ENROLLMENT SYSTEM
    public class EnrollmentSystem<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private Dictionary<TCourse, List<TStudent>> _enrollments = new();

        public bool EnrollStudent(TStudent student, TCourse course)
        {
            if (!_enrollments.ContainsKey(course))
                _enrollments[course] = new List<TStudent>();

            var students = _enrollments[course];

            if (students.Count >= course.MaxCapacity)
            {
                Console.WriteLine("Enrollment failed: Course is full");
                return false;
            }

            if (students.Any(s => s.StudentId == student.StudentId))
            {
                Console.WriteLine("Enrollment failed: Student already enrolled");
                return false;
            }

            // Check prerequisite if course is LabCourse
            if (course is LabCourse lab && student.Semester < lab.RequiredSemester)
            {
                Console.WriteLine("Enrollment failed: Semester prerequisite not met");
                return false;
            }

            students.Add(student);
            Console.WriteLine($"Enrollment successful: {student.Name} -> {course.Title}");
            return true;
        }

        public IReadOnlyList<TStudent> GetEnrolledStudents(TCourse course)
        {
            if (_enrollments.ContainsKey(course))
                return _enrollments[course].AsReadOnly();

            return new List<TStudent>().AsReadOnly();
        }

        public IEnumerable<TCourse> GetStudentCourses(TStudent student)
        {
            return _enrollments
                .Where(e => e.Value.Any(s => s.StudentId == student.StudentId))
                .Select(e => e.Key);
        }

        public int CalculateStudentWorkload(TStudent student)
        {
            return GetStudentCourses(student).Sum(c => c.Credits);
        }

        public bool IsStudentEnrolled(TStudent student, TCourse course)
        {
            return _enrollments.ContainsKey(course) &&
                   _enrollments[course].Any(s => s.StudentId == student.StudentId);
        }
    }

    // 3. STUDENT & COURSE
    public class EngineeringStudent : IStudent
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public int Semester { get; set; }
        public string Specialization { get; set; }
    }

    public class LabCourse : ICourse
    {
        public string CourseCode { get; set; }
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public int Credits { get; set; }

        public string LabEquipment { get; set; }
        public int RequiredSemester { get; set; }
    }

    // 4. GENERIC GRADEBOOK 
    public class GradeBook<TStudent, TCourse>
        where TStudent : IStudent
        where TCourse : ICourse
    {
        private Dictionary<(TStudent, TCourse), double> _grades = new();
        private EnrollmentSystem<TStudent, TCourse> _enrollmentSystem;

        public GradeBook(EnrollmentSystem<TStudent, TCourse> enrollmentSystem)
        {
            _enrollmentSystem = enrollmentSystem;
        }

        public void AddGrade(TStudent student, TCourse course, double grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentException("Grade must be between 0 and 100");

            if (!_enrollmentSystem.IsStudentEnrolled(student, course))
                throw new InvalidOperationException("Student is not enrolled in the course");

            _grades[(student, course)] = grade;
        }

        public double? CalculateGPA(TStudent student)
        {
            var studentGrades = _grades
                .Where(g => g.Key.Item1.StudentId == student.StudentId)
                .Select(g => new { Course = g.Key.Item2, Grade = g.Value });

            if (!studentGrades.Any())
                return null;

            double totalWeighted = studentGrades.Sum(g => g.Grade * g.Course.Credits);
            int totalCredits = studentGrades.Sum(g => g.Course.Credits);

            return totalWeighted / totalCredits;
        }

        public (TStudent student, double grade)? GetTopStudent(TCourse course)
        {
            var courseGrades = _grades
                .Where(g => EqualityComparer<TCourse>.Default.Equals(g.Key.Item2, course));

            if (!courseGrades.Any())
                return null;

            var top = courseGrades.OrderByDescending(g => g.Value).First();
            return (top.Key.Item1, top.Value);
        }
    }

    // 5. TEST SCENARIO
    class Program
    {
        static void Main()
        {
            var enrollmentSystem = new EnrollmentSystem<EngineeringStudent, LabCourse>();
            var gradeBook = new GradeBook<EngineeringStudent, LabCourse>(enrollmentSystem);

            // Students
            var s1 = new EngineeringStudent { StudentId = 1, Name = "Aryan", Semester = 4, Specialization = "CSE" };
            var s2 = new EngineeringStudent { StudentId = 2, Name = "Riya", Semester = 2, Specialization = "ECE" };
            var s3 = new EngineeringStudent { StudentId = 3, Name = "Karan", Semester = 5, Specialization = "ME" };

            // Courses
            var c1 = new LabCourse
            {
                CourseCode = "CSL101",
                Title = "Data Structures Lab",
                Credits = 4,
                MaxCapacity = 2,
                RequiredSemester = 3,
                LabEquipment = "Computers"
            };

            var c2 = new LabCourse
            {
                CourseCode = "EEL201",
                Title = "Electronics Lab",
                Credits = 3,
                MaxCapacity = 1,
                RequiredSemester = 4,
                LabEquipment = "Oscilloscopes"
            };

            // Enrollment tests
            enrollmentSystem.EnrollStudent(s1, c1); // success
            enrollmentSystem.EnrollStudent(s2, c1); // fail (semester)
            enrollmentSystem.EnrollStudent(s3, c1); // success
            enrollmentSystem.EnrollStudent(s1, c1); // fail (already enrolled)

            enrollmentSystem.EnrollStudent(s1, c2); // success
            enrollmentSystem.EnrollStudent(s3, c2); // fail (capacity)

            // Grades
            gradeBook.AddGrade(s1, c1, 85);
            gradeBook.AddGrade(s3, c1, 92);
            gradeBook.AddGrade(s1, c2, 88);

            // GPA
            Console.WriteLine($"\nGPA of {s1.Name}: {gradeBook.CalculateGPA(s1)}");
            Console.WriteLine($"GPA of {s3.Name}: {gradeBook.CalculateGPA(s3)}");

            // Top student
            var topStudent = gradeBook.GetTopStudent(c1);
            Console.WriteLine($"\nTop student in {c1.Title}: {topStudent?.student.Name} ({topStudent?.grade})");

            // Workload
            Console.WriteLine($"\n{s1.Name}'s Workload: {enrollmentSystem.CalculateStudentWorkload(s1)} credits");
        }
    }
}
