using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Student : Person
    {
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)] // Doesn't define the format of displayed date, that is defined in the CultureInfo of the server.
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        // navigation property | contains all the Enrollment entities that are connected with that Student entity
        public ICollection<Enrollment> Enrollments { get; set; } // when it's ICollection<T> EF defaultly creates HashSet<T>
    }
}