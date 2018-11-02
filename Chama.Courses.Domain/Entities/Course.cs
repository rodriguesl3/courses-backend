using System;
using System.Collections.Generic;

namespace Chama.Courses.Domain.Entities
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int MaximumStudents { get; set; }
        public virtual ICollection<Student> Students { get; set; }

        public Course()
        {
            Id = Guid.NewGuid();
        }
    }
}
