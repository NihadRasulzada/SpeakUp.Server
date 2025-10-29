using SpeakUp.Common.Models.Page;
using SpeakUp.Common.Models.Queries;

namespace SpeakUp.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }


    public Guid EntryId { get; set; }

    public Guid? UserId { get; set; }
}