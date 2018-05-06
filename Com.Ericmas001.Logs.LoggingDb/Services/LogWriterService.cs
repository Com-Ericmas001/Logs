using System.Linq;
using Com.Ericmas001.Logs.LoggingDb.Entities;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;

namespace Com.Ericmas001.Logs.LoggingDb.Services
{
    public class LogWriterService : ILogWriterService
    {
        private readonly ILoggingDbContext m_DbContext;

        public LogWriterService(ILoggingDbContext dbContext)
        {
            m_DbContext = dbContext;
        }

        public void LogExecutedCommand(string clientIp, string clientUserAgent, string serviceName, string controllerName, string methodName, string parms, string requestContentType, string requestData, string responseContentType, string responseData, string responseCode)
        {
            var service = m_DbContext.ServiceMethods.FirstOrDefault(x => x.ServiceName == serviceName && x.ControllerName == controllerName && x.MethodName == methodName) ?? new ServiceMethod
            {
                ServiceName = serviceName,
                ControllerName = controllerName,
                MethodName = methodName
            };

            var client = m_DbContext.Clients.FirstOrDefault(x => x.IpAddress == clientIp && x.UserAgent == clientUserAgent) ?? new Client
            {
                IpAddress = clientIp,
                UserAgent = clientUserAgent
            };

            m_DbContext.ExecutedCommands.Add(new ExecutedCommand
            {
                ServiceMethod = service,
                Client = client,
                Parms = parms,
                RequestContentType = requestContentType,
                RequestData = requestData,
                ResponseContentType = responseContentType,
                ResponseData = responseData,
                ResponseCode = responseCode
            });

            m_DbContext.SaveChanges();
        }

        public void LogNotification(bool success, string topic, string title, string message, string request, string response, string error)
        {
            m_DbContext.SentNotifications.Add(new SentNotification
            {
                Success = success,
                Topic = topic,
                Title = title,
                Message = message,
                Request = request,
                Response = response,
                Error = error
            });
            m_DbContext.SaveChanges();
        }
    }
}
