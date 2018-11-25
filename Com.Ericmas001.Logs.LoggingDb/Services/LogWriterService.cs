using System.Linq;
using Com.Ericmas001.Logs.LoggingDb.Entities;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;

namespace Com.Ericmas001.Logs.LoggingDb.Services
{
    public class LogWriterService : ILogWriterService
    {
        private readonly ILoggingDbContext _dbContext;

        public LogWriterService(ILoggingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LogExecutedCommand(string clientIp, string clientUserAgent, string serviceName, string controllerName, string methodName, string parms, string requestContentType, string requestData, string responseContentType, string responseData, string responseCode)
        {
            var service = _dbContext.ServiceMethods.FirstOrDefault(x => x.ServiceName == serviceName && x.ControllerName == controllerName && x.MethodName == methodName) ?? new ServiceMethod
            {
                ServiceName = serviceName,
                ControllerName = controllerName,
                MethodName = methodName
            };

            var client = _dbContext.Clients.FirstOrDefault(x => x.IpAddress == clientIp && x.UserAgent == clientUserAgent) ?? new Client
            {
                IpAddress = clientIp,
                UserAgent = clientUserAgent
            };

            _dbContext.ExecutedCommands.Add(new ExecutedCommand
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

            _dbContext.SaveChanges();
        }

        public void LogNotification(bool success, string topic, string title, string message, string request, string response, string error)
        {
            _dbContext.SentNotifications.Add(new SentNotification
            {
                Success = success,
                Topic = topic,
                Title = title,
                Message = message,
                Request = request,
                Response = response,
                Error = error
            });
            _dbContext.SaveChanges();
        }
    }
}
