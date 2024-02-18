using System.Globalization;

namespace NaturalPerson.Api.Middlewares
{
    public class MultiLanguageMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate next = next;

        public async Task Invoke(HttpContext context)
        {
            string? userLanguage = context.Request.Headers.AcceptLanguage.ToString().Split(',').FirstOrDefault();

            string defaultLanguage = string.IsNullOrEmpty(userLanguage) ? "en-US" : userLanguage;
            if (!string.IsNullOrWhiteSpace(defaultLanguage))
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(defaultLanguage);
                    CultureInfo.CurrentUICulture = new CultureInfo(defaultLanguage);
                }
                catch (CultureNotFoundException)
                {
                }
            }
            await next(context);
        }
    }
}
