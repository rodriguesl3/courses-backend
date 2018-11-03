using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Courses.Infrastructure.Interfaces
{
    public interface IServiceBusInfrastructure
    {
        Task SendAsync(Student student, ServiceBusConfiguration config);
    }
}
