using CustomerApplication.DataAccess;
using CustomerApplication.DataAccess.DataServices.Dapper;
using CustomerApplication.Logic;
using CustomerApplication.Logic.Validation;
using CustomerApplication.Models.RequestModel;
using CustomerApplication.WebApp.Middleware;
using FluentValidation;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();  

    // logic and data services
    builder.Services.AddSingleton<ICustomerData, DapperCustomerData>();
    builder.Services.AddSingleton<ICustomerHoursData, DapperCustomerHoursData>();
    builder.Services.AddTransient<ICustomerLogic, CustomerLogic>();
    builder.Services.AddTransient<ICustomerHoursLogic, CustomerHoursLogic>();

    // validator services
    builder.Services.AddScoped<IValidator<CustomerRequest>, CustomerRequestValidator>();
    builder.Services.AddScoped<IValidator<CustomerHoursRequest>, CustomerHoursRequestValidator>();

    // configure logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    var localLogger = app.Services.GetRequiredService<ILogger<Program>>();
    app.ConfigureExceptionHandler(localLogger);

    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404)
        {
            context.Request.Path = "/Dashboard/Error";
            await next();
        }
    });

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Dashboard}/{action=Index}");

    app.Run();
}
catch(Exception exception)
{
    logger.Error(exception, "Execution halted because of exception on initialize");
}
finally
{
    LogManager.Shutdown();
}