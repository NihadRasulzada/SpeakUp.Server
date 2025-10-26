using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SpeakUp.Application.Mapping;

namespace SpeakUp.Application.Extensions;

public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
    {
        var assm = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assm);
        });
        services.AddAutoMapper(typeof(MappingProfile).Assembly);   
        services.AddValidatorsFromAssembly(assm);

        return services;
    }
}