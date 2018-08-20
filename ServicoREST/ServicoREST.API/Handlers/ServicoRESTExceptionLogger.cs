using System;
using System.Web.Http.ExceptionHandling;

namespace ServicoREST.API.Handlers
{
    public class ServicoRESTExceptionLogger : ExceptionLogger
    {
        private Logger logger = new Logger();

        public override void Log(ExceptionLoggerContext context)
        {
            Exception ex = context.ExceptionContext.Exception;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }

            logger.Error(ex.Message, ex);
        }
    }
}