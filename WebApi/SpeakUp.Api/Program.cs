using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using SpeakUp.Api.Infrastructure.ActionFilters;
using SpeakUp.Api.Infrastructure.Extensions;
using SpeakUp.Application.Extensions;
using SpeakUp.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

// --- Add Services ---
builder.Services
    .AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
    .AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policyBuilder =>
{
    policyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

// --- Swagger (yalnız development üçün) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpeakUp API V1");
        //c.RoutePrefix = string.Empty; // Swagger root URL
    });
}

// --- Middlewares ---
app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

// --- Map Controllers ---
app.MapControllers();

app.Run();