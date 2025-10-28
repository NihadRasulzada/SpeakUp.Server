using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence;

public class SpeakUpContextFactory : IDesignTimeDbContextFactory<SpeakUpContext>
{
    public SpeakUpContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../WebApi/SpeakUp.Api"))
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration["SpeakUpDbConnectionString"];

        var optionsBuilder = new DbContextOptionsBuilder<SpeakUpContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new SpeakUpContext(optionsBuilder.Options);
    }
}