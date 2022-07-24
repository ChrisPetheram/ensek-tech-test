using MeterReadDatabaseAccess;
using MeterReadService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appSettings.json",true)
                .Build();

var connString = config.GetConnectionString("database");

builder.Services.AddSingleton<IDbConnector>(new DbConnector(connString));
builder.Services.AddSingleton(typeof(MeterReadingRepository));
builder.Services.AddSingleton(typeof(MeterReadBulkUpload));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();

app.Run();
