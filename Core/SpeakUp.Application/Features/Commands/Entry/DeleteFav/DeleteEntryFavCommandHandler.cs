using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.Entry;
using SpeakUp.Common.Infratructure;

namespace SpeakUp.Application.Features.Commands.Entry.DeleteFav;

public class DeleteEntryFavCommandHandler : IRequestHandler<DeleteEntryFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.FavExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.DeleteEntryFavQueueName,
            obj: new DeleteEntryFavEvent()
            { 
                EntryId = request.EntryId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}