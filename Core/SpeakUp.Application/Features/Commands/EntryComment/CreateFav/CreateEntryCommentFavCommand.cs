namespace SpeakUp.Application.Features.Commands.EntryComment.CreateFav;

public class CreateEntryCommentFavCommand : IRequest<bool>
{
    public CreateEntryCommentFavCommand(Guid entryCommentId, Guid userId)
    {
        EntryCommentId = entryCommentId;
        UserId = userId;
    }

    public Guid EntryCommentId { get; set; }

    public Guid UserId { get; set; }
}