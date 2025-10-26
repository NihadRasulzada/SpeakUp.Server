using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Domain.Models;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.Repositories;

public class EntryRepository : GenericRepository<Entry>, IEntryRepository
{
    public EntryRepository(SpeakUpContext dbContext) : base(dbContext)
    {
    }
}