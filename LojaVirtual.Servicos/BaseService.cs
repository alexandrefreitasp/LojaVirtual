using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Servicos
{
    // Classe abstrata que faz comunicação com os serviços através de chamadas HTTP
    public abstract class BaseService : IDisposable
    {
        // URI dos serviços
        private static readonly string _uri = "http://191.232.164.29:8084/loja/";

        // Método genérico para executar um HTTP GET no serviço
        public T Get<T>(string url)
        {
            // Instancia a classe HTTPClient para acesso ao serviço
            using (var client = new HttpClient())
            {
                // Configura a URI base
                client.BaseAddress = new Uri(_uri);

                // Faz a chamada no serviço
                HttpResponseMessage response = client.GetAsync(url).Result;

                // Transforma o retorno de JSON para o tipo enviado em T
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }

        // Método genérico para executar um HTTP POST no serviço
        public async Task<T> Post<T>(string url, object parametros)
        {
            // Instancia a classe HTTPClient para acesso ao serviço
            using (var client = new HttpClient())
            {
                // Configura a URI base
                client.BaseAddress = new Uri(_uri);

                // Transforma o obeto enviado em JSON para enviar ao serviço
                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

                // Faz a chamada no serviço
                HttpResponseMessage response = await client.PostAsync(url, conteudo).ConfigureAwait(false);

                // Transforma o retorno de JSON para o tipo enviado em T
                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }


        #region Disposable

        bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        #endregion
    }
}