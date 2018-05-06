using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Ericmas001.Logs.LoggingDb.WebApi
{
    public abstract class WebApiRequestAndResponseHandler : DelegatingHandler
    {
        protected abstract string ExtractBaseUrl(HttpRequestMessage request);
        protected abstract string ExtractController(HttpRequestMessage request);
        protected abstract string ExtractParms(HttpRequestMessage request);
        protected abstract void Log(string clientIp, string userAgent, string service, string endpoint, string httpMethod, string parms, string requestContentType, string requestBody, string responseContentType, string responseBody, string responseCode);

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            
            try
            {
                string service;
                string endpoint;
                string parms; 

                try
                {
                    service = ExtractBaseUrl(request);
                }
                catch(Exception e)
                {
                    service = e.ToString();
                }
                if (service.Length > 200)
                    service = service.Remove(200);

                try
                {
                    endpoint = ExtractController(request);
                }
                catch (Exception e)
                {
                    endpoint = e.ToString();
                }
                if (endpoint.Length > 100)
                    endpoint = endpoint.Remove(100);

                try
                {
                    parms = ExtractParms(request);
                }
                catch (Exception e)
                {
                    parms = e.ToString();
                }
                if (parms.Length > 2000)
                    parms = parms.Remove(2000);


                string requestContentType = null;
                string requestBody = null;
                if (request.Content != null)
                {
                    if (request.Content.Headers?.ContentType != null)
                        requestContentType = request.Content.Headers.ContentType.ToString();

                    request.Content.ReadAsStreamAsync().Result.Seek(0, System.IO.SeekOrigin.Begin);

                    // TODO
                    // if (requestContentType != null && requestContentType.Contains("multipart/form-data"))
                    // {
                    //         requestBody = string.Join(" ", HttpContext.Current.Request.Files.AllKeys.Select(x => $"[File {HttpContext.Current.Request.Files[x].FileName}: {HttpContext.Current.Request.Files[x].ContentLength / 1024.0:#.0} KB]"));
                    // }
                    //else
                    requestBody = await request.Content.ReadAsStringAsync();
                }

                string responseContentType = null;
                string responseBody = null;
                string responseCode = $"{(int)result.StatusCode} - {result.StatusCode}";
                if (result.Content != null)
                {
                    if (result.Content.Headers?.ContentType != null)
                        responseContentType = result.Content.Headers.ContentType.ToString();

                    if (responseContentType == null || !responseContentType.Contains("multipart/form-data"))
                        responseBody = await result.Content.ReadAsStringAsync();
                }

                var clientIp = request.GetClientIpString();
                var ip = string.IsNullOrEmpty(clientIp) ? "Unknown" : clientIp;
                if (ip.Length > 100)
                    ip = ip.Remove(100);

                var userAgent = request.Headers.UserAgent.ToString();
                var ag = string.IsNullOrEmpty(userAgent) ? "Unknown" : userAgent;
                if (ag.Length > 4000)
                    ag = ag.Remove(4000);

                Log(ip, ag, service, endpoint, request.Method.ToString(), parms, requestContentType, requestBody, responseContentType, responseBody, responseCode);

            }
            catch (Exception e)
            {
                OnLogException(e);
            }

            return result;
        }

        protected virtual void OnLogException(Exception e)
        {
            Trace.WriteLine(e.ToString());
        }
    }
}
