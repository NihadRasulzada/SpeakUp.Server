using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Domain.Models;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.Repositories;

public class EntryCommentRepository : GenericRepository<EntryComment>, IEntryCommentRepository
{
    public EntryCommentRepository(SpeakUpContext dbContext) : base(dbContext)
    {
    }
}