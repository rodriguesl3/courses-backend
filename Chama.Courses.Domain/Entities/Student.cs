using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Chama.Courses.Domain.Entities
{
    public class Student
    {
        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }


        //public Student()
        //{
        //    Id = Guid.NewGuid();
        //}
    }
}
