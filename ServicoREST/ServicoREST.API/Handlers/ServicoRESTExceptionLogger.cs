using System;
using System.Web.Http.ExceptionHandling;

namespace ServicoREST.API.Handlers
{
    // Classe genérica que trata e gera log dos erros ocorridos na API
    public class ServicoRESTExceptionLogger : ExceptionLogger
    {
        private Logger logger = new Logger();

        public override void Log(ExceptionLoggerContext context)
        {
            // Obtém a exception ocorrida
            Exception ex = context.ExceptionContext.Exception;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            // Loga a exception
            logger.Error(ex.Message, ex);
        }
    }
}