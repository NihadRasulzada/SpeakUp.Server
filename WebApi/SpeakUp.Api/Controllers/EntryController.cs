using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpeakUp.Application.Features.Queries.GetEntries;
using SpeakUp.Application.Features.Queries.GetEntryComments;
using SpeakUp.Application.Features.Queries.GetEntryDetail;
using SpeakUp.Application.Features.Queries.GetMainPageEntries;
using SpeakUp.Application.Features.Queries.GetUserEntries;
using SpeakUp.Common.Models.Queries;
using SpeakUp.Common.Models.RequestModels;

namespace SpeakUp.Api.Controllers;

public class EntryController(IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
    {
        var entries = await mediator.Send(query);

        return Ok(entries);
    }



    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await mediator.Send(new GetEntryDetailQuery(id, UserId));

        return Ok(result);
    }


    [HttpGet]
    [Route("Comments/{id}")]
    public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
    {
        var result = await mediator.Send(new GetEntryCommentsQuery(id, UserId, page, pageSize));

        return Ok(result);
    }

    [HttpGet]
    [Route("UserEntries")]
    [Authorize]
    public async Task<IActionResult> GetUserEntries(string userName, Guid userId, int page, int pageSize)
    {
        if (userId == Guid.Empty && string.IsNullOrEmpty(userName))
            if (UserId != null)
                userId = UserId.Value;

        var result = await mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

        return Ok(result);
    }


    [HttpGet]
    [Route("MainPageEntries")]
    public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
    {
        var entries = await mediator.Send(new GetMainPageEntriesQuery(UserId, page, pageSize));

        return Ok(entries);
    }

    [HttpPost]
    [Route("CreateEntry")]
    [Authorize]
    public async Task<IActionResult> CreateEntry([FromBody] CreateEntryCommand command)
    {
        command.CreatedById ??= UserId;

        var result = await mediator.Send(command);

        return Ok(result);
    }

    [HttpPost]
    [Route("CreateEntryComment")]
    [Authorize]
    public async Task<IActionResult> CreateEntryComment([FromBody] CreateEntryCommentCommand command)
    {
        command.CreatedById ??= UserId;

        var result = await mediator.Send(command);

        return Ok(result);
    }


    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
    {
        var result = await mediator.Send(query);

        return Ok(result);
    }
}