using System;
using System.Collections.Generic;
using System.Text;

namespace Chama.Courses.Domain.Configuration
{
    public class ServiceBusConfiguration
    {
        public string ConnectionString { get; set; }
        public string QueueName { get; set; }
    }
}
