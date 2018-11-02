using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Domain.Entities
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Course Course { get; set; }


        public Teacher()
        {
            Id = Guid.NewGuid();
        }
    }
}
