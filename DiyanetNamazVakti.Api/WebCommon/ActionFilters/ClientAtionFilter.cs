using Microsoft.AspNetCore.Mvc.Filters;

namespace DiyanetNamazVakti.Api.WebCommon.ActionFilters;

public class ClientAtionFilter : IAsyncActionFilter
{
    private readonly IMyApiClientSettings _myApiClientSettings;

    public ClientAtionFilter(IMyApiClientSettings myApiClientSettings)
    {
        _myApiClientSettings = myApiClientSettings;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var headers = context.HttpContext.Request.Headers;

        // TODO Please do this check from database
        #region UserName and SecretCode Check
        var myCustomHeader = headers.FirstOrDefault(x => x.Key == "UserName");
        var isAuthHorize = false;
        if (!string.IsNullOrEmpty(myCustomHeader.Key))
        {
            isAuthHorize = myCustomHeader.Value == _myApiClientSettings.UserName;
        }
        myCustomHeader = headers.FirstOrDefault(x => x.Key == "SecretCode");
        if (isAuthHorize && !string.IsNullOrEmpty(myCustomHeader.Key))
        {
            isAuthHorize = myCustomHeader.Value == _myApiClientSettings.SecretCode;
        }
        #endregion
        if (isAuthHorize)
            await next();
        else
            throw new BadHttpRequestException(string.Format(Dictionary.AccessDenied));

    }
}
