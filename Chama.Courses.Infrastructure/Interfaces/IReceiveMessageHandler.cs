using Chama.Courses.Domain.Configuration;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Courses.Infrastructure.Interfaces
{
    public interface IReceiveMessageHandler
    {
        void ReceiveMessageAsync(QueueClient serviceBusQueue);
    }
}
