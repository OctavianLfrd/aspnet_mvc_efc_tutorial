using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public enum Grades // That needed to be outside the class.
        {
            A, B, C, D, E, F
        }
        public int EnrollmentID { get; set; } // another template of defining the primary key [normally it's better to use only one template]

        public int CourseID { get; set; }

        public int StudentID { get; set; }

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grades? Grade { get; set; } // nullable

        public Course Course { get; set; } // navigation property

        public Student Student { get; set; } // navigation property
    }


}