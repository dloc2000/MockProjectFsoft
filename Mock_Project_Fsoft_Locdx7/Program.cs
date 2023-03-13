using Fsoft.Web.Locdx7.Common.Entities;
using Fsoft.Web.Locdx7.BL;
using Fsoft.Web.Locdx7.DL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Fsoft.Web.Locdx7.Common.SendEmail;
using Serilog;
using Fsoft.Web.Locdx7.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add DBContext
builder.Services.AddDbContext<Mock_Project_FSoft_locdx7Context>(option => 
    option.UseSqlServer(builder.Configuration.GetConnectionString("MockProject")));

// Dependency Injection 

builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<IProductDL, ProductDL>();

builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();

builder.Services.AddScoped<IOderBL, OrderBL>();
builder.Services.AddScoped<IOrderDL, OrderDL>();

builder.Services.AddScoped<IOderDetailBL, OrderDetailBL>();
builder.Services.AddScoped<IOrderDetailDL, OrderDetailDL>();

builder.Services.AddScoped<ISendEmailService, SendEmailService>();


// Use Serilogs
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Use Middleware
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(1800);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
//// JWT Barber
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(o =>
//{
//    o.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey
//        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = false,
//        ValidateIssuerSigningKey = true
//    };
//});
//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

// Use middleware
app.UseStaticFiles();
app.UseSession();

// Middleware handle error
app.UseMiddleware<ExceptionMiddleware>();
//app.ConfigureCustomExceptionMiddleware();

// Đưa middlware vào pipeline
app.UseMiddleware<CheckAcessMiddleware>();
//app.UseAuthorization();

app.MapControllers();

app.Run();
