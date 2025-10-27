using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Domain.Models;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.Repositories;

public class EmailConfirmationRepository : GenericRepository<EmailConfirmation>, IEmailConfirmationRepository
{
    public EmailConfirmationRepository(SpeakUpContext dbContext) : base(dbContext)
    {
    }
}