using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Domain.Entities
{
    public class Students
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Students()
        {
            Id = Guid.NewGuid();
        }
    }
}
