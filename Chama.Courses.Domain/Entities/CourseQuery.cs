using System;

namespace Chama.Courses.Domain.Entities
{
    public class CourseQuery
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public int MinimumAge { get; set; }
        public int MaximumAge { get; set; }
        public double AverageAge { get; set; }
    }
}
