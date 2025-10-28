using MediatR;
using SpeakUp.Common;
using SpeakUp.Common.Events.Entry;
using SpeakUp.Common.Infratructure;

namespace SpeakUp.Application.Features.Commands.Entry.DeleteVote;

public class DeleteEntryVoteCommandHandler : IRequestHandler<DeleteEntryVoteCommand, bool>
{
    public async Task<bool> Handle(DeleteEntryVoteCommand request, CancellationToken cancellationToken)
    {
        QueueFactory.SendMessageToExchange(exchangeName: SpeakUpConstants.VoteExchangeName,
            exchangeType: SpeakUpConstants.DefaultExchangeType,
            queueName: SpeakUpConstants.DeleteEntryVoteQueueName,
            obj: new DeleteEntryVoteEvent()
            { 
                EntryId = request.EntryId,
                CreatedBy = request.UserId
            });

        return await Task.FromResult(true);
    }
}