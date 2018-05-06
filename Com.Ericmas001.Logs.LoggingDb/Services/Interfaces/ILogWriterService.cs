namespace Com.Ericmas001.Logs.LoggingDb.Services.Interfaces
{
    public interface ILogWriterService
    {
        void LogExecutedCommand(string clientIp, string clientUserAgent, string serviceName, string controllerName, string methodName, string parms, string requestContentType, string requestData, string responseContentType, string responseData, string responseCode);
        void LogNotification(bool success, string topic, string title, string message, string request, string response, string error);
    }
}
