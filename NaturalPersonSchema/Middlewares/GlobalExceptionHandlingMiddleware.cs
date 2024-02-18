
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using NaturalPerson.Api.Resources;
using NaturalPersonl.Infra.Person.Exceptions;
using System.Net;
using System.Text.Json;

namespace NaturalPerson.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> logger;
        private readonly IStringLocalizer<ErrorMessages_ka_GEO> localizer;
        private readonly RequestDelegate next;
        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger, IStringLocalizer<ErrorMessages_ka_GEO> localizer)
        {
            this.next = next;
            this.logger = logger;
            this.localizer = localizer;
        }

        public async Task Invoke(HttpContext context)
        {
            
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleErrorr(ex, context, localizer);
            }
        }

        private async Task HandleErrorr(Exception ex, HttpContext context, IStringLocalizer<ErrorMessages_ka_GEO> localizer)
        {
            logger.LogError(ex, message: ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            ProblemDetails problemDetails = new();
            switch (ex)
            {
                case AddConnectionException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "AddConnectionError", "CantAddConnection", ex.Message, localizer);
                    break;
                case AddPictureException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "PictureError", "CantUploadPicture", ex.Message, localizer);
                    break;
                case DeleteConnectionException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "DeleteConnectionError", "CantDeleteConnection", ex.Message, localizer);
                    break;
                case DeletePersonException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "DeletePersonError", "CantDeletePerson", ex.Message, localizer);
                    break;
                case PersonAddException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "AddPersonError", "CantAddPerson", ex.Message, localizer);
                    break;
                case PersonUpdateException:
                    problemDetails = GenerateErrorBody(HttpStatusCode.BadRequest, "PersonUpdateError", "CantUpdatePerson", ex.Message, localizer);
                    break;
                default:
                    GenerateErrorBody(HttpStatusCode.InternalServerError, "error", "UnknownError", ex.Message, localizer);
                    break;
            }

            string json = JsonSerializer.Serialize(problemDetails);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }

        private static ProblemDetails GenerateErrorBody(HttpStatusCode statusCode, string type, string title, string details, IStringLocalizer<ErrorMessages_ka_GEO> localizer)
        {
            var data = localizer[title].Value;
            return new()
            {
                Status = (int)statusCode,
                Type = type,
                Title = localizer[title].Value,
                Detail = details
            };
        }
    }
}
