using MediatR;

namespace SpeakUp.Application.Features.Commands.EntryComment.DeleteFav;

public class DeleteEntryCommentFavCommand: IRequest<bool>
{
    public Guid EntryCommentId { get; set; }

    public Guid UserId { get; set; }

    public DeleteEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }
}