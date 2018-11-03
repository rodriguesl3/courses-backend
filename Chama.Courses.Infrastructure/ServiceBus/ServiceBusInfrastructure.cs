using Chama.Courses.Domain.Configuration;
using Chama.Courses.Domain.Entities;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Persistence.Repository.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Chama.Courses.Infrastructure.ServiceBus
{
    public class ServiceBusInfrastructure : IServiceBusInfrastructure
    {
        static IQueueClient queueClient;
        private readonly ISignupCourseRepository _signupRepo;

        public ServiceBusInfrastructure(ISignupCourseRepository signupRepo)
        {
            _signupRepo = signupRepo;
        }


        public async Task SendAsync(Student student, ServiceBusConfiguration config)
        {
            queueClient = new QueueClient(config.ConnectionString, config.QueueName);
            await SendMessagesAsync(student);
            await queueClient.CloseAsync();
        }

        private static async Task SendMessagesAsync(Student student)
        {
            try
            {
                string messageBody = JsonConvert.SerializeObject(student);
                var message = new Message(Encoding.UTF8.GetBytes(messageBody));
                message.SessionId = student.Id.ToString();
                await queueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                //TODO: GENERATE EXCEPTION HANDLER
            }
                
        }

        
    }
}
