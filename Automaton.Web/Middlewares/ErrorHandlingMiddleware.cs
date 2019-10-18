using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Automaton.Logica.Excepciones;
using Automaton.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Automaton.Web.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly IMapper mapper;

        public ErrorHandlingMiddleware(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var devolverJson = context.Request.ContentType == "application/json";
                if (!devolverJson)
                {
                    throw ex;
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            ErrorModel error;
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ExcepcionCompilacion)
            {
                var errorComp = mapper.Map<Exception, ErrorCompositorModel>(ex);
                errorComp.Errors = ((ExcepcionCompilacion)ex).ErroresCompilacion.Select(mapper.Map<string, ErrorModel>).ToArray();

                error = errorComp;
                code = HttpStatusCode.BadRequest;
            } else
            {
                error = mapper.Map<Exception, ErrorModel>(ex);
            }

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var result = JsonConvert.SerializeObject(error, serializerSettings);

            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}
