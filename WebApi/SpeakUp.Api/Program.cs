using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using SpeakUp.Api.Infrastructure.ActionFilters;
using SpeakUp.Api.Infrastructure.Extensions;
using SpeakUp.Application.Extensions;
using SpeakUp.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(opt => opt.Filters.Add<ValidateModelStateFilter>())
    .AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; })
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddOpenApi();

builder.Services.AddApplicationRegistration();
builder.Services.AddInfrastructureRegistration(builder.Configuration);
builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
    app.MapScalarApiReference();


app.UseHttpsRedirection();

app.ConfigureExceptionHandling(app.Environment.IsDevelopment());

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("MyPolicy");

app.MapControllers();

app.Run();