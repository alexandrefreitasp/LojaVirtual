using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LojaVirtual.Servicos
{
    public abstract class BaseService : IDisposable
    {
        private static readonly string _uri = "http://191.232.164.29:8084/loja/";

        public T Get<T>(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_uri);

                HttpResponseMessage response = client.GetAsync(url).Result;

                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<T> Post<T>(string url, object parametros)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_uri);

                StringContent conteudo = new StringContent(JsonConvert.SerializeObject(parametros), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, conteudo).ConfigureAwait(false);

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