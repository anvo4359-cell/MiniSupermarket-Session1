//********** HOTEN: VO THI THUY AN
//********** LOP: CCQ2411C
//********** MSSV: 2124110097
//********** MON: LAP TRINH UNG DUNG
//********** NGAYTHUCHIEN: 19/9/2026

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================================================
// 1. CAU HINH JWT AUTHENTICATION
// ==================================================

var jwtSecret = builder.Configuration["JwtSettings:Secret"]
    ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

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

        ValidateIssuer = false,
        ValidateAudience = false,

        ValidateLifetime = true
    };
});

// ==================================================
// 2. DANG KY CAC DICH VU
// ==================================================

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

// ==================================================
// 3. CAU HINH ENTITY FRAMEWORK CORE
// ==================================================

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

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