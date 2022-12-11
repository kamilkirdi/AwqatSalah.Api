using DiyanetNamazVakti.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

// Prayer TimeSettings
var awqatSalahSettings = builder.Configuration.GetSection(nameof(AwqatSalahSettings));
builder.Services.Configure<AwqatSalahSettings>(awqatSalahSettings);
builder.Services.AddSingleton<IAwqatSalahSettings>(sp => sp.GetRequiredService<IOptions<AwqatSalahSettings>>().Value);
builder.Services.AddHttpClient("AwqatSalahApi", client => { client.BaseAddress = new Uri(awqatSalahSettings.Get<AwqatSalahSettings>()!.ApiUri); });
// CacheSettings
builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));
builder.Services.AddSingleton<ICacheSettings>(sp => sp.GetRequiredService<IOptions<CacheSettings>>().Value);
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddSingleton<ICacheService, MemoryCacheService>();

//Api Call Service Dependence
builder.Services.AddScoped<IAwqatSalahConnectService, AwqatSalahApiService>();

//Service Dependencies
builder.Services.AddTransient<IPlaceService, PlaceService>();
builder.Services.AddTransient<IDailyContentService, DailyContentService>();
builder.Services.AddTransient<IAwqatSalahService, AwqatSalahService>();

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState.Where(e => e.Value.Errors.Count > 0).Select(e => new ValidationErrorModel
        {
            Name = e.Key,
            Message = e.Value.Errors.First().ErrorMessage
        }).ToList();
        throw new ValidationException(JsonSerializer.Serialize<List<ValidationErrorModel>>(errors));
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
