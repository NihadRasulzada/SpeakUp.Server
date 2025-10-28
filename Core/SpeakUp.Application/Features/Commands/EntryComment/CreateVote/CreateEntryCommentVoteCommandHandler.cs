using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.EntryComment;
using SpeakUp.Common.Infratructure;
using SpeakUp.Common.Models.RequestModels;

namespace SpeakUp.Application.Features.Commands.EntryComment.CreateVote;

public class CreateEntryCommentVoteCommandHandler :
    IRequestHandler<CreateEntryCommentVoteCommand, bool>
{
    public async Task<bool> Handle(CreateEntryCommentVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.VoteExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.CreateEntryCommentVoteQueueName,
            obj: new CreateEntryCommentVoteEvent()
            {
                EntryCommentId = request.EntryCommentId,
                VoteType = request.VoteType,
                CreatedBy = request.CreatedBy
            });

        return await Task.FromResult(true);
    }
}