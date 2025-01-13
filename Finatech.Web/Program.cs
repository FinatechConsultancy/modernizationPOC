using Etx.Infrastructure.Cache;
using Etx.Infrastructure.Factories;
using Etx.Infrastructure.Service;
using Finatech.AccountManagement.Extensions;
using Finatech.CreditManagement.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add cache services
builder.Services.AddApplicationCache(builder.Configuration);

//Add Business Modules
builder.Services.AddAccountManagementModule();
builder.Services.AddCreditModule();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));


//Create servises wiht this ReflectionFactory class and adds it to DI container.
// CreateInstance() method simulates RouterInternal.ExecuteDLLMethod() in the legacy code.
// builder.Services.AddScoped<ReflectionFactory>();
builder.Services.AddScoped<IDependencyReflectorFactory, DependencyReflectorFactory>();
// Add Swagger services
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.MapStaticAssets();

app.MapControllers();


// Set the service provider to use DI from legacy code.
ServiceLocator.ServiceProvider = app.Services;

app.Run();