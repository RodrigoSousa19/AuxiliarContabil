using System.Net;
using System.Text.Json;
using AuxiliarContabil.API.CustomExceptions;
using Prometheus;

namespace AuxiliarContabil.API.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        Counter exceptionsCounter = Metrics.CreateCounter("exceptions_total", "Exceptions count");
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    erro = Guid.NewGuid(),
                    mensagem = ex.Message,
                    erros = ex.Errors.Select(e => new { e.Field, e.Message }),
                    data = DateTime.UtcNow
                };
                exceptionsCounter.Inc();
                await context.Response.WriteAsJsonAsync(response);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    erro = Guid.NewGuid(),
                    mensagem = "Ocorreu um erro inesperado.",
                    detalhes = "Verifique com o administrador do sistema.",
                    data = DateTime.UtcNow
                };
                exceptionsCounter.Inc();
                await context.Response.WriteAsJsonAsync(response);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context)
        {
            var response = new
            {
                erro = Guid.NewGuid(), 
                mensagem = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.",
                data = DateTime.UtcNow
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
