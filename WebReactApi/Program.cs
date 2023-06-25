using Microsoft.EntityFrameworkCore;
using WebReactApi.Service.Todos;
using WebReactApi.Core.Context;
using WebReactApi.Core.Uow;
using WebReactApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    var env = builder.Environment;

    // use sql server db in production and sqlite db in development
    builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QuizContext"), b => b.MigrationsAssembly("Quiz.Api")));


    services.AddCors();
    services.AddControllers();



    // configure strongly typed settings object


    // configure DI for application services

    services.AddScoped<IDataContext, DataContext>();
    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddScoped<ITodoService, TodoService>();

}

var app = builder.Build();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

// migrate any database changes on startup (includes initial db creation)
//using (var scope = app.Services.CreateScope())
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
//    dataContext.Database.Migrate();
//}

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // global error handler
    app.UseMiddleware<ErrorHandlerMiddleware>();

    // custom jwt auth middleware
    //app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

app.Run("http://localhost:4000");