using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudLearn.Api.Authentication
{
    public class ApiKeyAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        public ApiKeyAuthorizationFilter(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthenticationConstants.ApiKeyHeaderName,
                out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key is missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthenticationConstants.ApiKeySectionName);

            if (!String.IsNullOrEmpty(apiKey))
            {
                if (!apiKey.Equals(extractedApiKey))
                {
                    context.Result = new UnauthorizedObjectResult("Invalid Api Key");
                    return;
                }
            }

        }
    }
}
