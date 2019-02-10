using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using AutomataNETjuegos.Compilador.Excepciones;
using AutomataNETjuegos.Web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AutomataNETjuegos.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMapper mapper;

        public ErrorHandlingMiddleware(RequestDelegate next, IMapper mapper)
        {
            this.next = next;
            this.mapper = mapper;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (context.Request.ContentType != "application/json")
                {
                    throw ex;
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            ErrorModel errors;
            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (ex is ExcepcionCompilacion)
            {
                errors = mapper.Map<ExcepcionCompilacion, ErrorModel>((ExcepcionCompilacion)ex);
                code = HttpStatusCode.BadRequest;
            } else
            {
                errors = mapper.Map<Exception, ErrorModel>(ex);
            }

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var result = JsonConvert.SerializeObject(errors, serializerSettings);

            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}
