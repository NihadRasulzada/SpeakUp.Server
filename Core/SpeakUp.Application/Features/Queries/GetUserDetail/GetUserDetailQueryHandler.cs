using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Common.Models.Queries;
using SpeakUp.Domain.Models;

namespace SpeakUp.Application.Features.Queries.GetUserDetail;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailViewModel>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        User dbUser = null;

        if (request.UserId != Guid.Empty)
            dbUser = await userRepository.GetByIdAsync(request.UserId);
        else if (!string.IsNullOrEmpty(request.UserName))
            dbUser = await userRepository.GetSingleAsync(i => i.UserName == request.UserName);

        // TODO if both are empty, throw new exception

        return mapper.Map<UserDetailViewModel>(dbUser);
    }
}