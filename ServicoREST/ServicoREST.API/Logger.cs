using log4net;
using System;
using System.Reflection;

namespace ServicoREST.API
{
    // Classe utilizada para gravar logs de erro no serviço
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Grava um log de erro
        public void Error(string message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }
    }
}