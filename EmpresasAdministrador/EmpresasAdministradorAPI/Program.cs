

using CompanyManagerAPI;

var builder = WebApplication.CreateBuilder(args);

var startup = new StartUp(builder.Configuration);
startup.ConfigureService(builder.Services);



var app = builder.Build();
startup.Configure(app, app.Environment);


app.Run();