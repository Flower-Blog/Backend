using Microsoft.EntityFrameworkCore;
using DotnetWebApi.Models;
using Microsoft.OpenApi.Models;
using System.Reflection;
using DotnetWebApi.Helper;
using System.Text.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using SwaggerExample_Advanced_Authorization.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
       .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT驗證描述"
    });
    options.OperationFilter<AuthorizeCheckOperationFilter>();
});

#region JWT
//清除預設映射
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//註冊JwtHelper
builder.Services.AddSingleton<JwtHelper>();
//設定認證方式
builder.Services
  //使用bearer token方式認證並且token用jwt格式
  .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // 可以讓[Authorize]判斷角色
        RoleClaimType = "roles",
        // 預設會認證發行人
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JwtSettings_Issuer"],
        // 不認證使用者
        ValidateAudience = false,
        // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
        ValidateIssuerSigningKey = true,
        // 簽章所使用的key
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings_SignKey"]))
    };
  });
#endregion

builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin() // 允許任何來源
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// 驗證
app.UseAuthentication();
// 授權
app.UseAuthorization();

app.MapControllers();

app.Run();