using BE_tasteal.API.AppSettings;
using BE_tasteal.API.Middleware;
using BE_tasteal.Business;
using BE_tasteal.Business.Ingredient;
using BE_tasteal.Business.Recipe;
using BE_tasteal.Entity.DTO.Request;
using BE_tasteal.Entity.Entity;
using BE_tasteal.Persistence.Context;
using BE_tasteal.Persistence.Interface;
using BE_tasteal.Persistence.Interface.GenericRepository;
using BE_tasteal.Persistence.Interface.IngredientRepo;
using BE_tasteal.Persistence.Interface.RecipeRepo;
using BE_tasteal.Persistence.Repository;
using BE_tasteal.Persistence.Repository.GenericRepository;
using BE_tasteal.Persistence.Repository.IngredientRepo;
using BE_tasteal.Persistence.Repository.RecipeRepo;
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

    #region config Service
    void ConfigureServices(IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //add service repository
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<ConnectionManager>();
        services.AddScoped<DbContext, MyDbContext>();

        //business
        services.AddScoped<IRecipeBusiness<SanPhamDto, SanPhamEntity>, SanPhamBusiness>();
        services.AddScoped<IRecipeBusiness<RecipeDto, RecipeEntity>, RecipeBusiness>();
        services.AddScoped<IIngredientBusiness, IngredientBusiness>();

        //repo
        services.AddScoped<ISanPhamResposity, SanPhamResposity>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IRecipeSearchRepo, RecipeSearchRepo>();
        services.AddScoped<IIngredientRepo, IngredientRepo>();

        services.AddDbContext<MyDbContext>(option =>
        {
            //local run
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            //docker container run
            //var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            //var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            //var dbPass = Environment.GetEnvironmentVariable("DB_ROOT_PASSWORD");
            //var connectionString = $"Server={dbHost};Port=3306;Database={dbName};Uid=root;Pwd={dbPass};";
            option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }, ServiceLifetime.Scoped);

        services.Configure<IISServerOptions>(option =>
        {
            option.AllowSynchronousIO = true;
        });

        // Invoking action filters to validate the model state for all entities received in POST and PUT requests
        // https://code-maze.com/aspnetcore-modelstate-validation-web-api/
        services.AddScoped<ValidationFilterAttribute>();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        builder.Host.ConfigureAppSettings();
        services
            .AddControllers()
            .AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        services.AddApiVersioning(option =>
        {
            option.AssumeDefaultVersionWhenUnspecified = true;
            option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            option.ReportApiVersions = true;
            option.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        ////serilog
        //Log.Information("Starting web host");
        ////var serilogUrl = builder.Configuration.GetRequiredSection("Seq").Get<Seq>()?.Url;
        //builder.Host.UseSerilog(new LoggerConfiguration()
        //           .Enrich.FromLogContext()
        //           .Enrich.WithMachineName()
        //           .WriteTo.Console()
        //           .WriteTo.Debug()
        //           //.WriteTo.Seq(serverUrl: serilogUrl!)
        //           .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
        //           .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        //           .ReadFrom.Configuration(builder.Configuration)
        //           .CreateLogger());
    }
    #endregion

    #region config middleware
    void ConfigureMiddleware(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();

            dbContext.Database.Migrate();
        }

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

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
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {

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