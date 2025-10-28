using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.EntryComment;
using SpeakUp.Common.Infratructure;

namespace SpeakUp.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommandHandler : IRequestHandler<DeleteEntryCommentFavCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryCommentFavCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.FavExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.DeleteEntryCommentFavQueueName,
            obj: new DeleteEntryCommentFavEvent()
            {
                EntryCommentId = request.EntryCommentId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}