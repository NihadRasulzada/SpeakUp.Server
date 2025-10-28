using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.Entry;
using SpeakUp.Common.Infratructure;

namespace SpeakUp.Application.Features.Commands.Entry.CreateFav;

public class CreateEntryFavCommandHandler : IRequestHandler<CreateEntryFavCommand, bool>
{

    public Task<bool> Handle(CreateEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.FavExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.CreateEntryFavQueueName,
            obj: new CreateEntryFavEvent()
            { 
                EntryId = request.EntryId.Value,
                CreatedBy = request.UserId.Value
            });

        return Task.FromResult(true);
    }
}