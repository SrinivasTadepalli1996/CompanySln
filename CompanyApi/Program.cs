using CompanyApi.Data;
using CompanyApi.Repositories;
using CompanyApi.Services;
using CompanyApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using CompanyApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Database Context
builder.Services.AddDbContext<CompanyDbContext>(options =>
    options.UseInMemoryDatabase("CompanyDb"));
// Set Kestrel to listen on all network interfaces inside the container
builder.WebHost.UseKestrel()
       .UseUrls("http://+:5168");
// Register Repository & Service
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

app.UseCors("AllowAll");

//  Global Exception Handling Middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

//  Configure Middleware & Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
