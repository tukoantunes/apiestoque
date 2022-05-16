using ApiEstoque.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling
            = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();

//registrando as configurações do projeto
EntityFrameworkConfiguration.Register(builder);
CorsConfiguration.Register(builder);
SwaggerConfiguration.Register(builder);
JwtConfiguration.Register(builder);

var app = builder.Build();

//ativando as configurações do projeto
CorsConfiguration.Use(app);
SwaggerConfiguration.Use(app);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



