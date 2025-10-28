using MediatR;

namespace SpeakUp.Application.Features.Commands.EntryComment.DeleteVote;

public class DeleteEntryCommentVoteCommand: IRequest<bool>
{
    public Guid EntryCommentId { get; set; }

    public Guid UserId { get; set; }

    public DeleteEntryCommentVoteCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}

