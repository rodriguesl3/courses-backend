using Chama.Courses.Domain.Entities;
using Chama.Courses.Infrastructure.Interfaces;
using Chama.Courses.Persistence.Repository.Interfaces;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chama.Courses.Infrastructure.ServiceBus
{
    public class ReceiveMessageHandler : IReceiveMessageHandler
    {
        private Object thisLock = new Object();
        private readonly ISignupCourseRepository _signupRepo;
        internal QueueClient _queueClient;

        public ReceiveMessageHandler(ISignupCourseRepository signupRepo)
        {
            _signupRepo = signupRepo;
        }

        public void ReceiveMessageAsync(QueueClient serviceBusQueue)
        {

            _queueClient = serviceBusQueue;
            RegisterOnMessageHandlerAndReceiveMessages();
        }


         void RegisterOnMessageHandlerAndReceiveMessages()
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var sessionHandlerOptions = new SessionHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentSessions = 4,
                AutoComplete = false
            };

            _queueClient.RegisterSessionHandler(ProcessMessagesAsync, sessionHandlerOptions);
        }

        async Task ProcessMessagesAsync(IMessageSession messageSession, Message message, CancellationToken token)
        {
            try
            {
                var student = JsonConvert.DeserializeObject<Student>(Encoding.UTF8.GetString(message.Body));

                lock (thisLock)
                {
                    //revalidation if course is available.
                    if (_signupRepo.CourseIsAvailable(student.CourseId))
                    {
                        _signupRepo.SigningupCourse(student);
                        //TODO: Send Email
                    }
                    else
                    {
                        //TODO: SEND EMAIL THAT COURSE IS NOT AVAILABLE.
                    }
                }


                Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
                await messageSession.CompleteAsync(message.SystemProperties.LockToken);
            }
            catch (SessionLockLostException err)
            {
                await messageSession.RenewSessionLockAsync();
                await messageSession.CompleteAsync(message.SystemProperties.LockToken);
            }

        }

        Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }




    }
}
