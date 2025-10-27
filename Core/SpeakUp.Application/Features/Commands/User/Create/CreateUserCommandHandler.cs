using AutoMapper;
using MediatR;
using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Common;
using SpeakUp.Common.Events.User;
using SpeakUp.Common.Infratructure;
using SpeakUp.Common.Infratructure.Exceptions;
using SpeakUp.Common.Models.RequestModels;

namespace SpeakUp.Application.Features.Commands.User.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public CreateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existsUser = await userRepository.GetSingleAsync(i => i.EmailAddress == request.EmailAddress);

        if (existsUser is not null)
            throw new DatabaseValidationException("User already exists!");

        var dbUser = mapper.Map<Domain.Models.User>(request);

        var rows = await userRepository.AddAsync(dbUser);

        if (rows > 0)
        {
            var @event = new UserEmailChangedEvent()
            {
                OldEmailAddress = null,
                NewEmailAddress = dbUser.EmailAddress
            };

            QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.UserExchangeName,
                exchangeType: SpeakUpConstants.DefaultExchangeType,
                queueName: SpeakUpConstants.UserEmailChangedQueueName,
                obj: @event);
        }

        return dbUser.Id;
    }
}