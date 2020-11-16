using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Http
{
    public class Http
    {
        #region Atributos
        private string urlBase = "https://localhost:44372/api/";
        #endregion

        #region Métodos
        public async Task<object> Get(string controller)
        {
            var client = new HttpClient();
            var url = urlBase + controller;
            object data = new object();

            HttpResponseMessage response = await client.GetAsync(url);

            if  (response.IsSuccessStatusCode)
            {
                data = await response.Content.ReadAsAsync<object>();
            }
            return data;
        }
        #endregion
    }
}
