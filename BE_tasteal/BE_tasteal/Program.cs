using BE_tasteal.API.AppSettings;
using BE_tasteal.API.Middleware;
using BE_tasteal.Business;
using BE_tasteal.Business.Interface;
using BE_tasteal.Entity.DTO;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface;
using BE_tasteal.Persistence.Interface.GenericRepository;
using BE_tasteal.Persistence.Repository;
using BE_tasteal.Persistence.Repository.GenericRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    ConfigureServices(builder.Services);

    var app = builder.Build();

    ConfigureMiddleware(app);

    app.Run();

    #region config
    void ConfigureServices(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //add service repository
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        builder.Services.AddScoped<ConnectionManager>();
        builder.Services.AddScoped<DbContext, MyDbContext>();

        builder.Services.AddScoped<ISanPhamResposity, SanPhamResposity>();

        builder.Services.AddScoped<IBusiness<SanPhamDto, SanPhamEntity>, SanPhamBusiness>();

        builder.Services.AddDbContext<MyDbContext>(option =>
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultString");
            option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        // Invoking action filters to validate the model state for all entities received in POST and PUT requests
        // https://code-maze.com/aspnetcore-modelstate-validation-web-api/
        builder.Services.AddScoped<ValidationFilterAttribute>();
        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        builder.Host.ConfigureAppSettings();
        builder.Services
            .AddControllers()
            .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        builder.Services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });

        builder.Services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        //serilog
        Log.Information("Starting web host");
        var serilogUrl = builder.Configuration.GetRequiredSection("Seq").Get<Seq>()?.Url;
        builder.Host.UseSerilog(new LoggerConfiguration()
                   .Enrich.FromLogContext()
                   .Enrich.WithMachineName()
                   .WriteTo.Console()
                   .WriteTo.Debug()
                   .WriteTo.Seq(serverUrl: serilogUrl!)
                   .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
                   .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
                   .ReadFrom.Configuration(builder.Configuration)
                   .CreateLogger());
    }

    void ConfigureMiddleware(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            dbContext.Database.Migrate();
        }

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
    #endregion
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}