using System.Collections.Generic;
using System.Threading.Tasks;

namespace RequisicaoGenerica.Controller.Helper
{
    class ServicesRequest
    {
        public class CepService<T> : Request<T> where T : new()
        {
            public CepService(string url) : base(Constantes.INICIO_URL + url + Constantes.FINAL_URL)
            {
            }

            public async Task<T> Consultar(Dictionary<string, object> param, Dictionary<string, object> req)
            {
                return await MakeHttpRequest(param, req);
            }
        }
    }
}