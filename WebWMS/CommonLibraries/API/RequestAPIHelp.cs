using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibraries.API
{
    public class RequestAPIHelp
    {
        private readonly IOptionsSnapshot<APIConfig> options;
        private RestClient restClient;

        private string  baseUrl;
        public RequestAPIHelp(IOptionsSnapshot<APIConfig> options)
        {
            restClient = new RestClient();
            baseUrl = options.Value.Url;
            this.options = options;
        }
        /// <summary>
        /// 非泛型接口请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ExecuteHttpClient(BaseRequest baseRequest)
        {
            ApiResponse resulst = new ApiResponse();
            try
            {
                string requstUrl = baseUrl + baseRequest.Route;
                var request = new RestRequest(requstUrl, baseRequest.Method);
                request.AddHeader("Content-Type", baseRequest.ContentType);
                if (baseRequest.Body != null)
                {
                    request.AddStringBody(JsonConvert.SerializeObject(baseRequest.Body), DataFormat.Json);
                }
                var response = await restClient.ExecuteAsync(request);
                resulst.Message = response.ErrorMessage;
                resulst.Resulst = response.Content;
                resulst.Status = response.IsSuccessful;
            }
            catch (Exception ex)
            {
                resulst.Message = ex.Message;
                resulst.Resulst = "";
                resulst.Status = false;
            }
            return resulst;
        }

        /// <summary>
        /// 泛型接口请求
        /// </summary>
        /// <param name="baseRequest"></param>
        /// <returns></returns>
        public async Task<ApiResponse<T>> ExecuteHttpClient<T>(BaseRequest baseRequest)
        {
            ApiResponse<T> resulst = new ApiResponse<T>();
            try
            {
                string requstUrl = baseUrl + baseRequest.Route;
                var request = new RestRequest(requstUrl, baseRequest.Method);
                request.AddHeader("Content-Type", baseRequest.ContentType);
                if (baseRequest.Body != null)
                    request.AddStringBody(JsonConvert.SerializeObject(baseRequest.Body), DataFormat.Json);

                var response = await restClient.ExecuteAsync(request);
                resulst.Message = response.ErrorMessage;
                resulst.Resulst = JsonConvert.DeserializeObject<T>(response.Content);
                resulst.Status = response.IsSuccessful;

            }
            catch (Exception ex)
            {
                resulst.Message = ex.Message;
                resulst.Resulst = default;
                resulst.Status = false;
            }
            return resulst;
        }
    }
}
