using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Domain.Models;
using SpeakUp.Persistence.Context;

namespace SpeakUp.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SpeakUpContext dbContext) : base(dbContext)
    {
    }
}