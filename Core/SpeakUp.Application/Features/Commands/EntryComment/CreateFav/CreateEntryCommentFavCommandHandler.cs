using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.EntryComment;
using SpeakUp.Common.Infratructure;

namespace SpeakUp.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommandHandler : IRequestHandler<CreateEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.FavExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.CreateEntryCommentFavQueueName,
            obj: new CreateEntryCommentFavEvent()
            { 
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}