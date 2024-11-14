using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using src.Database;
using src.Entity;
using src.Repository;
using src.Services.Address;
using src.Services.cart;
using src.Services.category;
using src.Services.Payment;
// using src.Services.PaymentCard;
using src.Services.review;
using src.Services.User;
using src.Services.Jewelry;
using src.Services.Gemstone;
using src.Utils;
//using src.Services.OrderGemstone;
using src.Services.Order;
using src.Middlewares;
using src.Services.Gym;
using src.Services.InsurancePlan;
using src.Services;

var builder = WebApplication.CreateBuilder(args);

var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);
dataSourceBuilder.MapEnum<Role>();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build())
    .EnableSensitiveDataLogging()
    .ConfigureWarnings(warnings => 
    warnings.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning));
});

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

///user
builder
    .Services.AddScoped<IUserService, UserService>()
    .AddScoped<UserRepository, UserRepository>();

///Address
builder
    .Services.AddScoped<IAddressService, AddressService>()
    .AddScoped<AddressRepository, AddressRepository>();

// add DI services for category
builder
    .Services.AddScoped<ICategoryService, CategoryService>()
    .AddScoped<CategoryRepository, CategoryRepository>();

// add DI services for review
builder
    .Services.AddScoped<IReviewService, ReviewService>()
    .AddScoped<ReviewRepository, ReviewRepository>();

// add DI services for cart
builder
    .Services.AddScoped<ICartService, CartService>()
    .AddScoped<CartRepository, CartRepository>();

///Payment
builder
    .Services.AddScoped<IPaymentService, PaymentService>()
    .AddScoped<PaymentRepository, PaymentRepository>();

///PaymentCard
// builder
//     .Services.AddScoped<IPaymentCardService, PaymentCardService>()
//     .AddScoped<PaymentCardRepository, PaymentCardRepository>();

//Gemstones
builder.Services
    .AddScoped<IGemstoneService, GemstoneService>()
    .AddScoped<GemstonesRepository, GemstonesRepository>();

//Jewelry
builder.Services
    .AddScoped<IJewelryService, JewelryService>()
    .AddScoped<JewelryRepository, JewelryRepository>();

// //OrderGemstone
// builder.Services
//     .AddScoped<IOrderGemstoneService, OrderGemstoneService>()
//     .AddScoped<OrderGemstoneRepository, OrderGemstoneRepository>();

//Order
builder.Services
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<OrderRepository, OrderRepository>();

//Gym
builder.Services
    .AddScoped<IGymService, GymService>()
    .AddScoped<GymRepository, GymRepository>();

//Insurance plan
builder.Services
    .AddScoped<IInsurancePlan, InsurancePlanService>()
    .AddScoped<InsurancePlanRepository, InsurancePlanRepository>();

//Gym Insurance plan
builder.Services
    .AddScoped<IGymInsuranceService, GymInsuranceService>()
    .AddScoped<GymInsuranceRepository, GymInsuranceRepository>();

//CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy => {
        policy.WithOrigins("http://localhost:3000").AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed((host)=>true)
        .AllowCredentials();
    });
});

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            ),
        };
    });

builder.Services.AddAuthorization(options=>{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    try
    {
        // Check if the application can connect to the database
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}

// add middleware 
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
