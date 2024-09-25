using Microsoft.AspNetCore.Authentication.JwtBearer; // Espacio de nombres para autenticación JWT
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using api.Data; // Asegúrate de que esto coincide con la ubicación de tu DbContext
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Obtiene la cadena de conexión
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Comprueba si la cadena de conexión es nula y lanza una excepción si es así
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no se ha encontrado en el archivo de configuración.");
}

// Configura el contexto de la base de datos
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configuración de JWT
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];
var audience = builder.Configuration["Jwt:Audience"];

// Verifica si la clave JWT es nula o vacía
if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("La clave JWT no se ha encontrado en el archivo de configuración.");
}

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin() // Permitir todos los orígenes
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(options =>
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
        ValidIssuer = issuer,
        ValidAudience = audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddControllers();

// Configura Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "api", Version = "v1" });
});

var app = builder.Build();

// Configura el middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins"); // Cambiado a "AllowAllOrigins"
app.UseRouting();
app.UseAuthentication(); // Asegúrate de que el middleware de autenticación se use antes de la autorización
app.UseAuthorization();

// Registra los controladores directamente en el pipeline
app.MapControllers();

// Configura Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
    c.RoutePrefix = string.Empty; // Para que Swagger UI sea accesible en la raíz
});

app.Run();
