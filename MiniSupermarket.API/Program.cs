//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAYTHUCHIEN: 19/9/2026

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================================================
// 1. CAU HINH JWT AUTHENTICATION
// ==================================================

var jwtSecret = builder.Configuration["JwtSettings:Secret"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];

if (string.IsNullOrWhiteSpace(jwtSecret) || jwtSecret.Length < 32 ||
    string.IsNullOrWhiteSpace(jwtIssuer) || string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "Thiếu cấu hình JwtSettings:Secret (tối thiểu 32 ký tự), Issuer hoặc Audience. " +
        "Hãy cấu hình bằng User Secrets hoặc biến môi trường trước khi chạy API.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;

    options.DefaultChallengeScheme =
        JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,

        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSecret)
        ),

        ValidIssuer = jwtIssuer,
        ValidateIssuer = true,
        ValidAudience = jwtAudience,
        ValidateAudience = true,

        ValidateLifetime = true
    };
});

// ==================================================
// 2. DANG KY CAC DICH VU
// ==================================================

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
        new BadRequestObjectResult(new
        {
            success = false,
            message = "Dữ liệu gửi lên không hợp lệ.",
            errors = context.ModelState
                .Where(pair => pair.Value?.Errors.Count > 0)
                .ToDictionary(
                    pair => pair.Key,
                    pair => pair.Value!.Errors.Select(error =>
                        string.IsNullOrWhiteSpace(error.ErrorMessage)
                            ? "Giá trị không hợp lệ."
                            : error.ErrorMessage).ToArray())
        });
});

builder.Services.AddEndpointsApiExplorer();

// ==================================================
// 3. CAU HINH ENTITY FRAMEWORK CORE
// ==================================================

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Thiếu ConnectionStrings:DefaultConnection trong cấu hình API.");
}

builder.Services.AddDbContext<SupermarketDbContext>(options =>
    options.UseSqlServer(connectionString));

// ==================================================
// 4. CAU HINH SWAGGER + JWT AUTHORIZE
// ==================================================

builder.Services.AddSwaggerGen(options =>
{
    // Tao nut Authorize
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",

        Type = SecuritySchemeType.Http,

        Scheme = "bearer",

        BearerFormat = "JWT",

        In = ParameterLocation.Header,

        Description = "Nhap JWT token vao day."
    });

    // Yeu cau Swagger su dung JWT
    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });
});

// ==================================================
// 5. BUILD UNG DUNG
// ==================================================

var app = builder.Build();

// ==================================================
// 6. CAU HINH SWAGGER
// ==================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

// ==================================================
// 7. MIDDLEWARE
// ==================================================

app.UseHttpsRedirection();

// Xac thuc JWT phai dat truoc Authorization
app.UseAuthentication();

app.UseAuthorization();

// ==================================================
// 8. MAP CONTROLLER
// ==================================================

app.MapControllers();

// ==================================================
// 9. CHAY UNG DUNG
// ==================================================

app.Run();