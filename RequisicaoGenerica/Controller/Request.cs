using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace RequisicaoGenerica.Controller
{
    class Request<T> where T : new()
    {
        private RestClient _client = null;
        private RestRequest _request = null;
        private IRestResponse resposta;

        public Request(string url)
        {
            _client = new RestClient(url);
            _request = new RestRequest(Method.GET);
        }

        public async Task<T> MakeHttpRequest(Dictionary<string, object> param, Dictionary<string, object> req)
        {
            try
            {
                MontaCabecalho(req);
                MontaCorpo(param);

                _client.Timeout = 10000;
                await Task.Run(() => resposta = _client.Execute(_request));
                HttpStatusCode statusCode = resposta.StatusCode;
                int numericStatusCode = (int)statusCode;

                return JsonConvert.DeserializeObject<T>(resposta.Content);
            }
            catch (Exception ex)
            {
                Console.WriteLine("o erro é: " + ex);
                throw;
            }
        }

        private void MontaCorpo(Dictionary<string, object> param)
        {
            if (param != null)
            {
                foreach (var item in param)
                {
                    _request.AddParameter(item.Key, item.Value == null ? "" : item.Value.ToString());
                }
            }
        }

        private void MontaCabecalho(Dictionary<string, object> req)
        {
            //_request.AddHeader("cache-control", "no-cache");
            //_request.AddHeader("content-Type", "application/json");
            if (req != null)
            {
                foreach (var item in req)
                {
                    _request.AddHeader(item.Key, item.Value.ToString());
                }
            }
        }
    }
}