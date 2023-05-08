using Asp.Versioning;

namespace DiyanetNamazVakti.Api.WebCommon.Extensions;

public static class ApiVersioningExtension
{
    public static IServiceCollection AddAndConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            //.Combine(//new QueryStringApiVersionReader()
            //new UrlSegmentApiVersionReader(),
            //new HeaderApiVersionReader("api-version"),
            //new MediaTypeApiVersionReader("api-version")
            //);

        }).AddApiExplorer(config =>
        {
            config.GroupNameFormat = "'v'VVV";
            config.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
