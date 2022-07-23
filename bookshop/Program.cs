using System.Text;
using bookshop.DbContext;
using bookshop.Entity;
using bookshop.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using uitbooks.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
String connection =
    "server=localhost; port=3306; database=bookshop; user=root; password=root";
builder.Services.AddDbContext<Dbcontext>(option => option.UseMySql(connection, ServerVersion.AutoDetect(connection)));
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API", Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.Configure<IdentityOptions>(options =>
{
    // Thiết lập về Password
    options.Password.RequireDigit = false; // Không bắt phải có số
    options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
    options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
    options.Password.RequireUppercase = false; // Không bắt buộc chữ in
    options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
    options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

    // Cấu hình Lockout - khóa user
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
    options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
    options.Lockout.AllowedForNewUsers = true;

    // // Cấu hình về User.
    // options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
    //     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    // options.User.RequireUniqueEmail = true; // Email là duy nhất
    //
    // // Cấu hình đăng nhập.
    // options.SignIn.RequireConfirmedEmail = true; // Cấu hình xác thực địa chỉ email (email phải tồn tại)
    // options.SignIn.RequireConfirmedPhoneNumber = false; // Xác thực số điện thoại
});

builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<UserInter, UserService>();
builder.Services.AddScoped<RateInter, RateService>();
builder.Services.AddScoped<OrderInter, OrderService>();
builder.Services.AddScoped<OrderDeInter, OrderDeService>();
builder.Services.AddScoped<CmtInter, CmtService>();
builder.Services.AddScoped<CateInter, CateService>();
builder.Services.AddScoped<BookInter, BookService>();
builder.Services.AddScoped<BlogInter, BlogService>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Dbcontext>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("svvafvaefvaefvarv"))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("user",
        policy => policy.RequireRole("user"));
    options.AddPolicy("admin",
        policy => policy.RequireRole("admin"));
    options.AddPolicy("seller",
        policy => policy.RequireRole("seller"));
});
builder.Services.AddMvc();
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();