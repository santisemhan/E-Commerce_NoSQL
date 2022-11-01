namespace Commerce.Core.Helpers.Middleware
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Net;

    /// <summary>
    ///    Middleware principal de manejo de excepciones en controladores. Las
    ///    excepciones pueden provenir desde el controlador en sí mismo, o
    ///    desde un nivel inferior dentro de la aplicación.
    /// </summary>
    public sealed class ExceptionHandler
    {

        private readonly RequestDelegate nextMiddleware;

        public ExceptionHandler(RequestDelegate nextMiddleware)
        {
            this.nextMiddleware = nextMiddleware;
        }

        /// <summary>
        ///    Procesa el mensaje, atrapando las excepciones generadas.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await nextMiddleware.Invoke(context);
            }
            catch (Exception exception)
            {
                await SendPayload(context, Envelope.Error(exception.Message));
            }
        }

        private async Task SendPayload<TPayload>(HttpContext context, TPayload payload, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var result = JsonConvert.SerializeObject(payload, settings);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
