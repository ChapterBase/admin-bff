using admin_bff.Controllers.Outbound;
using admin_bff.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register clients
builder.Services.AddScoped<CoreServiceClient>();

// Register services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<JwtUtilService>();

// Resource server configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = false // Disable default audience validation
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Headers["Authorization"].ToString();

                if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Token = token.Substring("Bearer ".Length).Trim();
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Authentication failed"
                });
            },
            OnTokenValidated = async context =>
            {
                var rawToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Trim();

                var token = context.SecurityToken as JwtSecurityToken;
                if (token == null)
                {
                    // Attempt manual decoding if token is null
                    var jwtHandler = new JwtSecurityTokenHandler();
                    if (jwtHandler.CanReadToken(rawToken))
                    {
                        token = jwtHandler.ReadJwtToken(rawToken);
                    }
                    else
                    {
                        context.Fail("Invalid token format");
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "Invalid token format"
                        });

                        return;
                    }
                }

                // Validate audience manually
                var clientId = token.Claims.FirstOrDefault(c => c.Type == "client_id")?.Value;
                if (clientId != builder.Configuration["Jwt:Audience"])
                {
                    context.Fail("Invalid audience");

                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        message = "Invalid audience"
                    });

                    return;
                }
            }
        };

        options.MetadataAddress = builder.Configuration["Jwt:Issuer"] + "/.well-known/openid-configuration";
    });




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicyBuilder =>
    {
        corsPolicyBuilder.AllowAnyOrigin()
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

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
