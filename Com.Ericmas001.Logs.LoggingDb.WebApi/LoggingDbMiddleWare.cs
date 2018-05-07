using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Ericmas001.Logs.Enums;
using Com.Ericmas001.Logs.LoggingDb.Services.Interfaces;
using Com.Ericmas001.Logs.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace Com.Ericmas001.Logs.LoggingDb.WebApi
{
    public class LoggingDbMiddleWare
    {
        private readonly RequestDelegate m_Next;
        private readonly ILogWriterService m_LogWriterService;
        private readonly ILoggerService m_LoggerService;

        public LoggingDbMiddleWare(RequestDelegate next, ILogWriterService logWriterService, ILoggerService loggerService)
        {
            m_Next = next;
            m_LogWriterService = logWriterService;
            m_LoggerService = loggerService;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            request.EnableRewind();
            Stream originalBody = context.Response.Body;
            var memStream = new MemoryStream();
            context.Response.Body = memStream;


            await m_Next.Invoke(context);



            var result = context.Response;
            try
            {
                var service = request.Scheme + "://" + request.Host + "/";
                if (service.Length > 200)
                    service = service.Remove(200);

                var route = context.GetRouteData();
                var routeParms = route.Values.Where(x => x.Key != "controller" && x.Key != "action").ToArray();
                var endpoint = route.Values["controller"] + "." + route.Values["action"] + "(" + string.Join(", ", routeParms.Select(x => x.Key)) + ")";
                if (endpoint.Length > 100)
                    endpoint = endpoint.Remove(100);

                string parms = JsonConvert.SerializeObject(new
                {
                    Parms = routeParms.ToDictionary(x => x.Key, x => x.Value.ToString()),
                    Headers = request.Headers?.Where(x => x.Key != "User-Agent" && x.Key != "Cookie" && !x.Key.StartsWith("X-")).ToDictionary(x => x.Key, x => string.Join("; ", x.Value))
                }, Formatting.Indented);
                if (parms.Length > 2000)
                    parms = parms.Remove(2000);

                string requestContentType = request.ContentType;
                string requestBody;

                if (requestContentType != null && requestContentType.Contains("multipart/form-data"))
                {
                    requestBody = string.Join(" ", request.Form.Files.Select(x => $"[File {x.FileName}: {x.Length / 1024.0:#.0} KB]"));
                }
                else
                {
                    request.Body.Position = 0;
                    using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true))
                    {
                        requestBody = reader.ReadToEnd();
                    }

                    request.Body.Position = 0;
                }

                string responseBody = null;
                string responseContentType = result.ContentType;
                try
                {

                    if (responseContentType != null && !responseContentType.Contains("multipart/form-data"))
                    {
                        memStream.Position = 0;
                        responseBody = new StreamReader(memStream).ReadToEnd();
                    }

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
                finally
                {
                    context.Response.Body = originalBody;
                }
                string responseCode = $"{result.StatusCode}";
                string clientIp = context.Connection.RemoteIpAddress.ToString();
                var ip = string.IsNullOrEmpty(clientIp) ? "Unknown" : clientIp;
                if (ip.Length > 100)
                    ip = ip.Remove(100);

                var userAgent = context.Request.Headers["User-Agent"].ToString();
                var ag = string.IsNullOrEmpty(userAgent) ? "Unknown" : userAgent;
                if (ag.Length > 4000)
                    ag = ag.Remove(4000);
                m_LogWriterService.LogExecutedCommand(ip, ag, service, endpoint, request.Method, parms, requestContentType, requestBody, responseContentType, responseBody, responseCode);
            }
            catch (Exception e)
            {
                m_LoggerService.Log(LogLevelEnum.Error, e.ToString());
            }

        }
    }
    public static class LoggingDbMiddleWareExtensions
    {
        public static IApplicationBuilder UseLoggingDb(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingDbMiddleWare>();
        }
    }
}
