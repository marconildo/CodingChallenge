using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodingChallenge.Core
{
    public class BaseApi<T> : IBaseApi<T>
    {
        private readonly string _endpoint;
        private string _baseUri;
        private readonly JsonSerializerSettings _jsonSettings;
        protected IHttpClientWrapper Client;

        public string BaseUri
        {
            get => _baseUri;
            set => _baseUri = _endpoint + "/" + value;
        }

        public BaseApi(IConfigurationRoot config, IHttpClientWrapper customClient, JsonSerializerSettings customJsonSerializerSettings = null)
        {
            Client = customClient;
            _jsonSettings = customJsonSerializerSettings ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            if (config["UrlApi"] == null)
                throw new ArgumentException("UrlApi");

            _endpoint = config["UrlApi"];
        }

        public BaseApi(IConfigurationRoot config) : this(config, new StandardHttpClient(),
           new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
        {
        }

        protected virtual async Task SetContentJson(object data, HttpRequestMessage requestMessage)
        {
            if (data != null)
            {
                var content = await Task.FromResult(JsonConvert.SerializeObject(data, _jsonSettings)).ConfigureAwait(false);
                requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }
        }

        protected virtual string GetCompleteUrl(string partOfUrl, string id)
        {
            var url = string.IsNullOrEmpty(partOfUrl) ? $"{BaseUri}/{id}" : $"{BaseUri}{partOfUrl.Replace(BaseUri, "")}";

            if (url.Last().Equals('/'))
                url = url.Remove(url.Length - 1);

            return url;
        }

        protected virtual async Task<HttpResponseMessage> SendRequestAsync(HttpMethod method, string url, object data = null, string customToken = null)
        {
            using (var requestMessage = new HttpRequestMessage(method, url))
            {
                await SetContentJson(data, requestMessage);

                var response = await Client.SendAsync(requestMessage).ConfigureAwait(false);
                return response;
            }
        }

        protected static async Task<string> GetCompleteErrorResponseAsync(string data, HttpResponseMessage response)
        {
            try
            {
                var jsonMessage = JsonConvert.DeserializeObject<ComplexErrorResponse>(data);

                return await Task.FromResult(JsonConvert.SerializeObject(new
                {
                    response.StatusCode,
                    ReasonPhase = response.ReasonPhrase,
                    Message = jsonMessage
                })).ConfigureAwait(false);

            }
            catch (Exception)
            {
                try
                {
                    var jsonMessage = JsonConvert.DeserializeObject<ErrorResponse>(data).Errors;
                    return await Task.FromResult(JsonConvert.SerializeObject(new
                    {
                        response.StatusCode,
                        ReasonPhase = response.ReasonPhrase,
                        Message = jsonMessage
                    })).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    return string.Empty;
                }

            }
        }

        protected virtual async Task<T> ProcessResponse<T>(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return await Task.FromResult(JsonConvert.DeserializeObject<T>(data, _jsonSettings)).ConfigureAwait(false);
            }

            var errorMessage = await GetCompleteErrorResponseAsync(data, response).ConfigureAwait(false);
            throw new Exception(errorMessage);
        }

        public virtual async Task<ModelList<T>> GetListAsync()
        {
            var response = await SendRequestAsync(HttpMethod.Get, BaseUri).ConfigureAwait(false);
            return await ProcessResponse<ModelList<T>>(response).ConfigureAwait(false);
        }

        public virtual async Task<ModelList<T>> GetListAsync(string partOfUrl)
        {
            var completeUrl = GetCompleteUrl(partOfUrl, null);
            var response = await SendRequestAsync(HttpMethod.Get, completeUrl).ConfigureAwait(false);
            return await ProcessResponse<ModelList<T>>(response).ConfigureAwait(false);
        }

        public virtual async Task<T> GetAsync(string id)
        {
            var response = await GetAsync(id, null).ConfigureAwait(false);
            return response;
        }

        public virtual async Task<T> GetAsync(string id, string partOfUrl)
        {
            var completeUrl = GetCompleteUrl(partOfUrl, id);
            var response = await SendRequestAsync(HttpMethod.Get, completeUrl, null, null).ConfigureAwait(false);
            return await ProcessResponse<T>(response).ConfigureAwait(false);
        }
    }

    internal sealed class ComplexErrorResponse
    {
        public Dictionary<string, JArray> Errors { get; set; }
    }


    internal sealed class ErrorResponse
    {
        public string Errors { get; set; }
    }
}
