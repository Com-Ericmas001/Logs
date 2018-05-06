using System.Net;
using System.Net.Http;

namespace Com.Ericmas001.Logs.LoggingDb.WebApi
{
    static class HttpRequestMessageExtensions
    {

        private const string HTTP_CONTEXT = "MS_HttpContext";
        private const string REMOTE_ENDPOINT_MESSAGE = "System.ServiceModel.Channels.RemoteEndpointMessageProperty";
        private const string OWIN_CONTEXT = "MS_OwinContext";

        public static string GetClientIpString(this HttpRequestMessage request)
        {
            //Web-hosting
            if (request.Properties.ContainsKey(HTTP_CONTEXT))
            {
                dynamic ctx = request.Properties[HTTP_CONTEXT];
                if (ctx != null)
                    return ctx.Request.UserHostAddress;
            }
            //Self-hosting
            if (request.Properties.ContainsKey(REMOTE_ENDPOINT_MESSAGE))
            {
                dynamic remoteEndpoint = request.Properties[REMOTE_ENDPOINT_MESSAGE];
                if (remoteEndpoint != null)
                    return remoteEndpoint.Address;
            }
            //Owin-hosting
            if (request.Properties.ContainsKey(OWIN_CONTEXT))
            {
                dynamic ctx = request.Properties[OWIN_CONTEXT];
                if (ctx != null)
                    return ctx.Request.RemoteIpAddress;
            }
            //if (System.Web.HttpContext.Current != null)
            //{
            //    return System.Web.HttpContext.Current.Request.UserHostAddress;
            //}
            // Always return all zeroes for any failure
            return "0.0.0.0";
        }

        public static IPAddress GetClientIpAddress(this HttpRequestMessage request)
        {
            IPAddress.TryParse(request.GetClientIpString(), out var ipAddress);
            return ipAddress;
        }

    }
}
