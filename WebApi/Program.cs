using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder
    .Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(x =>
    {
        x.RegisterModule(new AutofacBusinessModule());
    });


// Add services to the container
builder.Services.AddControllers().ConfigureApiBehaviorOptions(x =>
{
    x.SuppressModelStateInvalidFilter = true;
    x.SuppressInferBindingSourcesForParameters = true;
});


builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
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

app.UseAuthorization();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Public", "Attachments")),
    RequestPath = "/Public/Attachments"
});

app.ConfigureExceptionMiddleware();

app.MapControllers();

app.Run();
