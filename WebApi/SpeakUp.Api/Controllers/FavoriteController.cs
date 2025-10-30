using MediatR;
using Microsoft.AspNetCore.Mvc;
using SpeakUp.Application.Features.Commands.Entry.CreateFav;
using SpeakUp.Application.Features.Commands.Entry.DeleteFav;
using SpeakUp.Application.Features.Commands.EntryComment.CreateFav;
using SpeakUp.Application.Features.Commands.EntryComment.DeleteFav;

namespace SpeakUp.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoriteController(IMediator mediator) : BaseController
{
    [HttpPost]
    [Route("entry/{entryId}")]
    public async Task<IActionResult> CreateEntryFav(Guid entryId)
    {
        var result = await mediator.Send(new CreateEntryFavCommand(entryId, UserId));

        return Ok(result);
    }

    [HttpPost]
    [Route("entrycomment/{entrycommentId}")]
    public async Task<IActionResult> CreateEntryCommentFav(Guid entrycommentId)
    {
        var result = UserId != null && await mediator.Send(new CreateEntryCommentFavCommand(entrycommentId, UserId.Value));

        return Ok(result);
    }


    [HttpPost]
    [Route("deleteentryfav/{entryId}")]
    public async Task<IActionResult> DeleteEntryFav(Guid entryId)
    {
        var result = UserId != null && await mediator.Send(new DeleteEntryFavCommand(entryId, UserId.Value));

        return Ok(result);
    }

    [HttpPost]
    [Route("deleteentrycommentfav/{entrycommentId}")]
    public async Task<IActionResult> DeleteEntryCommentFav(Guid entrycommentId)
    {
        var result = UserId != null && await mediator.Send(new DeleteEntryCommentFavCommand(entrycommentId, UserId.Value));

        return Ok(result);
    }
}