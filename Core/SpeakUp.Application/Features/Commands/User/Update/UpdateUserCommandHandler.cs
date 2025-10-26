using AutoMapper;
using MediatR;
using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Common;
using SpeakUp.Common.Events.User;
using SpeakUp.Common.Infratructure;
using SpeakUp.Common.Infratructure.Exceptions;
using SpeakUp.Common.Models.RequestModels;

namespace SpeakUp.Application.Features.Commands.User.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Guid>
{
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;

    public UpdateUserCommandHandler(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var dbUser = await userRepository.GetByIdAsync(request.Id);
        
        if(dbUser is null)
            throw new DatabaseValidationException("User not found!");

        var dbEmailAddress = dbUser.EmailAddress;
        var emailChanged = string.CompareOrdinal(dbEmailAddress, request.EmailAddress) != 0;

        mapper.Map(request, dbUser);

        var rows = await userRepository.UpdateAsync(dbUser);

        if (emailChanged && rows > 0)
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

            dbUser.EmailConfirmed = false;
            await userRepository.UpdateAsync(dbUser);
        }

        return dbUser.Id;
    }
}