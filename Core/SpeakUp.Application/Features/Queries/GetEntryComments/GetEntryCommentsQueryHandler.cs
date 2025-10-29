using Microsoft.EntityFrameworkCore;
using SpeakUp.Application.Interfaces.Repositories;
using SpeakUp.Common.Infratructure.Extensions;
using SpeakUp.Common.Models;
using SpeakUp.Common.Models.Page;
using SpeakUp.Common.Models.Queries;

namespace SpeakUp.Application.Features.Queries.GetEntryComments;

internal class
    GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository entryCommentRepository;

    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        this.entryCommentRepository = entryCommentRepository;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = entryCommentRepository.AsQueryable();

        query = query.Include(i => i.EntryCommentFavorites)
            .Include(i => i.CreatedBy)
            .Include(i => i.EntryCommentVotes)
            .Where(i => i.EntryId == request.EntryId);

        var list = query.Select(i => new GetEntryCommentsViewModel
        {
            Id = i.Id,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
            FavoritedCount = i.EntryCommentFavorites.Count,
            CreatedDate = i.CreateDate,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
                    ? i.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
                    : VoteType.None
        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}