using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProjectCopyServer.Samples.HttpClient;
using Volo.Abp.DependencyInjection;

namespace ProjectCopyServer.Common.HttpClient;


public interface IHttpProvider : ISingletonDependency
{
    Task<T> Invoke<T>(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false, bool debugLog = true);

    Task<string> Invoke(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false, bool debugLog = true);

    Task<string> Invoke(HttpMethod method, string url,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, bool withLog = false, bool debugLog = true);

    Task<HttpResponseMessage> InvokeResponse(HttpMethod method, string url,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null,
        bool withLog = false, bool debugLog = true);

    Task<HttpResponseMessage> InvokeResponse(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false,
        bool debugLog = true);
}

public class HttpProvider : IHttpProvider
{
    public static readonly JsonSerializerSettings DefaultJsonSettings = JsonSettingsBuilder.New()
            .WithCamelCasePropertyNamesResolver()
            .IgnoreNullValue()
            .Build();
    
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<HttpProvider> _logger;

    public HttpProvider(IHttpClientFactory httpClientFactory, ILogger<HttpProvider> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<T> Invoke<T>(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false, bool debugLog = true)
    {
        var resp = await Invoke(apiInfo.Method, domain + apiInfo.Path, pathParams, param, body, header, withLog, debugLog);
        try
        {
            return JsonConvert.DeserializeObject<T>(resp, settings ?? DefaultJsonSettings);
        }
        catch (Exception ex)
        {
            throw new HttpRequestException($"Error deserializing service [{apiInfo.Path}] response body: {resp}", ex);
        }
    }

    public async Task<HttpResponseMessage> InvokeResponse(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false, bool debugLog = true)
    {
        return await InvokeResponse(apiInfo.Method, domain + apiInfo.Path, pathParams, param, body, header, withLog, debugLog);
    }
    
    public async Task<string> Invoke(string domain, ApiInfo apiInfo,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null, JsonSerializerSettings settings = null, bool withLog = false, bool debugLog = true)
    {
        return await Invoke(apiInfo.Method, domain + apiInfo.Path, pathParams, param, body, header, withLog, debugLog);
    }

    public async Task<string> Invoke(HttpMethod method, string url,
        Dictionary<string, string> pathParams = null,
        Dictionary<string, string> param = null,
        string body = null,
        Dictionary<string, string> header = null,
        bool withLog = false, bool debugLog = true)
    {
        var response = await InvokeResponse(method, url, pathParams, param, body, header, withLog, debugLog);
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Server [{url}] returned status code {response.StatusCode} : {content}", null, response.StatusCode);
        }
        return content;
    }

    public async Task<HttpResponseMessage> InvokeResponse(HttpMethod method, string url,
            Dictionary<string, string> pathParams = null,
            Dictionary<string, string> param = null,
            string body = null,
            Dictionary<string, string> header = null,
            bool withLog = false, bool debugLog = true)
        {
        // url params
        var fullUrl = PathParamUrl(url, pathParams);
        
        var builder = new UriBuilder(fullUrl);
        var query = HttpUtility.ParseQueryString(builder.Query);
        foreach (var item in param ?? new Dictionary<string, string>())
            query[item.Key] = item.Value;
        builder.Query = query.ToString() ?? string.Empty;

        var request = new HttpRequestMessage(method, builder.ToString());

        // headers
        foreach (var h in header ?? new Dictionary<string, string>())
            request.Headers.Add(h.Key, h.Value);

        // body
        if (body != null)
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

        // send
        var stopwatch = Stopwatch.StartNew();
        var client = _httpClientFactory.CreateClient();
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        var time = stopwatch.ElapsedMilliseconds;
        // log
        if (withLog)
            _logger.LogInformation(
            "Request To {FullUrl}, statusCode={StatusCode}, time={Time}, query={Query}, body={Body}, resp={Content}",
            fullUrl, response.StatusCode, time, builder.Query, body, content);
        else if (debugLog)
            _logger.LogDebug(
                "Request To {FullUrl}, statusCode={StatusCode}, time={Time}, query={Query}, header={Header}, body={Body}, resp={Content}",
                fullUrl, response.StatusCode, time, builder.Query, request.Headers.ToString(), body, content);
        else 
            _logger.LogDebug(
                "Request To {FullUrl}, statusCode={StatusCode}, time={Time}, query={Query}, header={Header}",
                fullUrl, response.StatusCode, time, builder.Query, request.Headers.ToString());
        return response;
    }
    
    
    private static string PathParamUrl(string url, Dictionary<string, string> pathParams)
    {
        return pathParams.IsNullOrEmpty()
            ? url
            : pathParams.Aggregate(url, (current, param) => current.Replace($"{{{param.Key}}}", param.Value));
    }
}